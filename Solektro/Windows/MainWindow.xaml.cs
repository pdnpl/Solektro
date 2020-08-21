using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Microsoft.Win32;
using Solektro.API;
using Solektro.API.Helpers;
using Solektro.Core.Models;
using Solektro.Helpers;

namespace Solektro.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ViewModel VM { get; } = new ViewModel();
        private Calculations Calc { get; } = new Calculations();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = VM;
        }

        #region UI events

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            SaveSession();
        }

        private void ExportToHtml_Click(object sender, RoutedEventArgs e)
        {
            var saveFile = new SaveFileDialog
            {
                CheckPathExists = true,
                FileName = "Oferta Solekro.html",
                Filter = "HTML (*.html)|*.html|All files (*.*)|*.*",
                FilterIndex = 0,
                OverwritePrompt = true,
                Title = "Export to HTML",
            };

            if (saveFile.ShowDialog() == true)
            {
                var offers = new Offers();
                offers.ExportToHtml(VM.Offer, saveFile.FileName);
                Process.Start(@"cmd.exe ", @"/c " + WinPathHelper.GetShortPath(saveFile.FileName));
            }
        }

        private void Window_BindingUpdated(object sender, DataTransferEventArgs e)
        {
            Calc.Recalculate(VM.Offer);
            e.Handled = true;
        }

        private void KitComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cb = (ComboBox)sender;
            var kit = cb.SelectedItem as Kit;

            if (VM?.Offer == null || kit == null)
            {
                e.Handled = true;
                return;
            }

            var defPanel = VM?.SolarPanels?.FirstOrDefault(x => x.Id == kit.DefaultPanelId);
            if (defPanel != null)
            {
                SolarPanelGroupCB.SelectedValue = VM.SolarPanelGroups.First(x => x.Key == defPanel.Manufacturer).Key;
                VM.Offer.Panel.Item = defPanel;
            }

            var defInverter = VM?.Inverters?.FirstOrDefault(x => x.Id == kit.DefaultInverterId);
            if (defInverter != null)
            {
                InverterGroupsCB.SelectedValue = VM.InverterGroups.First(x => x.Key == defInverter.Manufacturer).Key;
                VM.Offer.Inverter.Item = defInverter;
            }

            var defInstallationsType = VM?.InstallationTypes?.FirstOrDefault(x => x.Id == kit.DefaultInstallationTypeId);
            if (defInstallationsType != null)
            {
                InstallationTypeGroupsCB.SelectedValue = VM.InstallationTypeGroups.First(x => x.Key == defInstallationsType.Type).Key;
                VM.Offer.InstallationsType.Item = defInstallationsType;
            }

            RecalculatePanelQuantity();
        }

        private void PowerComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RecalculatePanelQuantity();
        }

        private void SolarPanelComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FindInverter();
        }

        private void PanelQtyTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            FindInverter();
        }

        #endregion

        #region Intrernal helpers

        private void LoadData()
        {
            // Offer
            VM.Offer = new Offer
            {
                Date = DateTime.Now,
                Consultant = new User { Id = 1, Name = "Najlepszy z najlepszych", Phone = "123-123-123", Email = "aaa@aaa.pl" },
            };

            // Data
            var data = new Data();
            VM.Kits = data.GetKits();
            VM.SolarPanels = data.GetSolarPanels().OrderBy(x => x.Manufacturer).ThenBy(x => x.Model);
            VM.SolarPanelGroups = VM.SolarPanels.GroupBy(x => x.Manufacturer);
            VM.Inverters = data.GetInverters().OrderBy(x => x.Manufacturer).ThenBy(x => x.Model);
            VM.InverterGroups = VM.Inverters.GroupBy(x => x.Manufacturer);

            VM.OptimizerGroups = data.GetOptimizers().GroupBy(x => x.Manufacturer);
            VM.AcDistributionBoards = data.GetAcDistributionBoards();
            VM.AcMaterials = data.GetAcWires();
            VM.DcDistributionBoard = data.GetDcDistributionBoard();
            VM.DcMaterials = data.GetDcWires();

            VM.InstallationTypes = data.GetInstallationsTypes().OrderBy(x => x.Type).ThenBy(x => x.Type);
            VM.InstallationTypeGroups = VM.InstallationTypes.GroupBy(x => x.Type);

            VM.InstallationWorks = data.GetInstallationWorks();
            VM.GroundWorks = data.GetGroundWorks();
            VM.Monitorings = data.GetMonitorings();
            VM.Documentations = data.GetDocumentations();
            VM.Warranties = data.GetWarranties();
            VM.Inspections = data.GetInspections();
            VM.Insurances = data.GetInsurances();
            VM.KitPowers = data.GetPowers();
            VM.Vats = data.GetVats();
            VM.Margins = data.GetMargins();

            // Session
            var sm = new SessionManager();
            var session = sm.ReadSession();
            if (session != null)
            {
                VM.Offer.Kit = VM?.Kits?.FirstOrDefault(x => x.Id == session.Kit.Id);
                VM.Offer.KitPower = VM?.KitPowers?.FirstOrDefault(x => x.Value == session.KitPower.Value);
                VM.Offer.Margin = VM?.Margins?.FirstOrDefault(x => x.Value == session.Margin.Value);
                VM.Offer.Total.VatRate = VM?.Vats?.FirstOrDefault(x => x.Value == session.VatRate.Value);
            }
        }

        private void SaveSession()
        {
            var session = new Session()
            {
                Kit = VM?.Offer?.Kit,
                KitPower = VM?.Offer?.KitPower,
                Margin = VM?.Offer?.Margin,
                VatRate = VM?.Offer?.Total?.VatRate
            };

            var sm = new SessionManager();
            sm.SaveSession(session);
        }

        private void RecalculatePanelQuantity()
        {
            if (VM.Offer == null || VM.Offer.Panel == null)
                return;

            if (VM.Offer.KitPower == null || VM.Offer.Panel.Item?.Power == null || VM.Offer.Panel.Item.Power.Value == 0)
                VM.Offer.Panel.Quantity = 0;
            else
                VM.Offer.Panel.Quantity = (decimal)Math.Round(VM.Offer.KitPower.Value / VM.Offer.Panel.Item.Power.Value, 0);

            FindInverter();
        }

        private void FindInverter()
        {
            if (VM.Offer == null || VM.SelectedInvertersGroup == null || VM.Offer.PowerCalc == null)
                return;

            VM.Offer.Inverter.Item = InverterHelper.FindInverter(VM.SelectedInvertersGroup, VM.Offer.PowerCalc);

            if (VM.Offer.Inverter != null)
                VM.Offer.Inverter.Quantity = 1;

        }

        #endregion
    }
}



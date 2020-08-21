using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Solektro.Core.Models;

namespace Solektro.Windows
{
    public class ViewModel : INotifyPropertyChanged
    {
        public IEnumerable<Kit> Kits { get => _kits; set { _kits = value; NotifyPropertyChanged(); } }
        private IEnumerable<Kit> _kits;

        public IEnumerable<Power> KitPowers { get => _kitPowers; set { _kitPowers = value; NotifyPropertyChanged(); } }
        private IEnumerable<Power> _kitPowers;

        public IEnumerable<Margin> Margins { get => _margins; set { _margins = value; NotifyPropertyChanged(); } }
        private IEnumerable<Margin> _margins;

        public IEnumerable<IGrouping<string, PvItem>> SolarPanelGroups { get => _solarPanelGroups; set { _solarPanelGroups = value; NotifyPropertyChanged(); } }
        private IEnumerable<IGrouping<string, PvItem>> _solarPanelGroups;

        public IGrouping<string, PvItem> SelectedSolarPanelsGroup { get => _selectedSolarPanelsGroup; set { _selectedSolarPanelsGroup = value; NotifyPropertyChanged(); } }
        private IGrouping<string, PvItem> _selectedSolarPanelsGroup;

        public IEnumerable<PvItem> SolarPanels { get => _solarPanels; set { _solarPanels = value; NotifyPropertyChanged(); } }
        private IEnumerable<PvItem> _solarPanels;

        public IEnumerable<IGrouping<string, PvItem>> InverterGroups { get => _inverterGroups; set { _inverterGroups = value; NotifyPropertyChanged(); } }
        private IEnumerable<IGrouping<string, PvItem>> _inverterGroups;

        public IGrouping<string, PvItem> SelectedInvertersGroup { get => _selectedInvertersGroup; set { _selectedInvertersGroup = value; NotifyPropertyChanged(); } }
        private IGrouping<string, PvItem> _selectedInvertersGroup;

        public IEnumerable<PvItem> Inverters { get => _inverters; set { _inverters = value; NotifyPropertyChanged(); } }
        private IEnumerable<PvItem> _inverters;

        public IEnumerable<IGrouping<string, PvItem>> OptimizerGroups { get => _optimizerGroups; set { _optimizerGroups = value; NotifyPropertyChanged(); } }
        private IEnumerable<IGrouping<string, PvItem>> _optimizerGroups;

        public IGrouping<string, PvItem> SelectedOptimizersGroup { get => _selectedOptimizersGroup; set { _selectedOptimizersGroup = value; NotifyPropertyChanged(); } }
        private IGrouping<string, PvItem> _selectedOptimizersGroup;

        public IEnumerable<PvItem> Optimizers { get => _optimizers; set { _optimizers = value; NotifyPropertyChanged(); } }
        private IEnumerable<PvItem> _optimizers;

        public IEnumerable<OtherItem> AcDistributionBoards { get => _acDistributionBoards; set { _acDistributionBoards = value; NotifyPropertyChanged(); } }
        private IEnumerable<OtherItem> _acDistributionBoards;

        public IEnumerable<OtherItem> AcMaterials { get => _AcMaterials; set { _AcMaterials = value; NotifyPropertyChanged(); } }
        private IEnumerable<OtherItem> _AcMaterials;

        public IEnumerable<OtherItem> DcDistributionBoard { get => _dcDistributionBoard; set { _dcDistributionBoard = value; NotifyPropertyChanged(); } }
        private IEnumerable<OtherItem> _dcDistributionBoard;

        public IEnumerable<OtherItem> DcMaterials { get => _dcMaterials; set { _dcMaterials = value; NotifyPropertyChanged(); } }
        private IEnumerable<OtherItem> _dcMaterials;

        public IEnumerable<IGrouping<string, AssemblyItem>> InstallationTypeGroups { get => _installationTypeGroups; set { _installationTypeGroups = value; NotifyPropertyChanged(); } }
        private IEnumerable<IGrouping<string, AssemblyItem>> _installationTypeGroups;

        public IGrouping<string, AssemblyItem> SelectedInstallationTypesGroup { get => _selectedInstallationTypesGroup; set { _selectedInstallationTypesGroup = value; NotifyPropertyChanged(); } }
        private IGrouping<string, AssemblyItem> _selectedInstallationTypesGroup;

        public IEnumerable<AssemblyItem> InstallationTypes { get => _installationsTypes; set { _installationsTypes = value; NotifyPropertyChanged(); } }
        private IEnumerable<AssemblyItem> _installationsTypes;

        public IEnumerable<OtherItem> GroundInstallations { get => _groundInstallations; set { _groundInstallations = value; NotifyPropertyChanged(); } }
        private IEnumerable<OtherItem> _groundInstallations;

        public IEnumerable<OtherItem> InstallationWorks { get => _installationWorks; set { _installationWorks = value; NotifyPropertyChanged(); } }
        private IEnumerable<OtherItem> _installationWorks;

        public IEnumerable<OtherItem> GroundWorks { get => _groundWorks; set { _groundWorks = value; NotifyPropertyChanged(); } }
        private IEnumerable<OtherItem> _groundWorks;

        public IEnumerable<OtherItem> Monitorings { get => _monitorings; set { _monitorings = value; NotifyPropertyChanged(); } }
        private IEnumerable<OtherItem> _monitorings;

        public IEnumerable<OtherItem> Documentations { get => _documentations; set { _documentations = value; NotifyPropertyChanged(); } }
        private IEnumerable<OtherItem> _documentations;

        public IEnumerable<OtherItem> Warranties { get => _warranties; set { _warranties = value; NotifyPropertyChanged(); } }
        private IEnumerable<OtherItem> _warranties;

        public IEnumerable<OtherItem> Inspections { get => _inspections; set { _inspections = value; NotifyPropertyChanged(); } }
        private IEnumerable<OtherItem> _inspections;

        public IEnumerable<OtherItem> Insurances { get => _insurances; set { _insurances = value; NotifyPropertyChanged(); } }
        private IEnumerable<OtherItem> _insurances;
        public IEnumerable<VatRate> Vats { get => _vats; set { _vats = value; NotifyPropertyChanged(); } }
        private IEnumerable<VatRate> _vats;

        public Offer Offer { get => _offer; set { _offer = value; NotifyPropertyChanged(); } }
        private Offer _offer;



        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        internal void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (propertyName == null)
                return;

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}

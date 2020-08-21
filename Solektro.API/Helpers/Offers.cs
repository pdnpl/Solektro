using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using Solektro.Core.Models;

namespace Solektro.API.Helpers
{
    public class Offers
    {
        private const string TemplateFilename = @".\Templates\Offer.html";

        public void ExportToHtml(Offer offer, string htmlFilePath)
        {
            if (offer == null)
                throw new ArgumentNullException(nameof(offer));

            #region Dictionary
            var dic = new Dictionary<string, string>
            {
                { "{offer-Power}", offer.PowerCalc.Text },
                { "{offer-Date}",  offer.Date.ToShortDateString()},

                { "{offer-SolarPanelModel}", offer.Panel.Item.Model },
                { "{offer-SolarPanelQuantity}", $"{offer.Panel.Quantity} {offer.Panel.Item.Unit}" },
                { "{offer-SolarPanelNetUnitPrice}", offer.Panel.NetUnitPrice.ToString("C2") },
                { "{offer-SolarPanelNetAmount}", offer.Panel.NetAmount.ToString("C2") },

                { "{offer-InverterModel}", offer.Inverter.Item.Model },
                { "{offer-InverterQuantity}", $"{offer.Inverter.Quantity} {offer.Inverter.Item.Unit}" },
                { "{offer-InverterNetUnitPrice}", offer.Inverter.NetUnitPrice.ToString("C2") },
                { "{offer-InverterNetAmount}", offer.Inverter.NetAmount.ToString("C2") },

                { "{offer-OptimizerModel}", offer.Optimizer.Item.Model },
                { "{offer-OptimizerQuantity}", $"{offer.Optimizer.Quantity} {offer.Optimizer.Item.Unit}" },
                { "{offer-OptimizerNetUnitPrice}", offer.Optimizer.NetUnitPrice.ToString("C2") },
                { "{offer-OptimizerNetAmount}", offer.Optimizer.NetAmount.ToString("C2") },

                { "{offer-AcDistributionBoard}", offer.AcDistributionBoard.Item.Description },
                { "{offer-AcDistBoardQuantity}", $"{offer.AcDistributionBoard.Quantity} { offer.AcDistributionBoard.Item.Unit}" },
                { "{offer-AcDistBoardNetUnitPrice}", offer.AcDistributionBoard.NetUnitPrice.ToString("C2") },
                { "{offer-AcDistBoardNetAmount}", offer.AcDistributionBoard.NetAmount.ToString("C2") },

                { "{offer-AcMaterial}", offer.AcMaterial.Item.Description },
                { "{offer-AcMatQuantity}", $"{offer.AcMaterial.Quantity} {offer.AcMaterial.Item.Unit}" },
                { "{offer-AcMatNetUnitPrice}", offer.AcMaterial.NetUnitPrice.ToString("C2") },
                { "{offer-AcMatNetAmount}", offer.AcMaterial.NetAmount.ToString("C2") },

                { "{offer-DcDistributionBoard}", offer.DcDistributionBoard.Item.Description },
                { "{offer-DcDistBoardQuantity}", $"{offer.DcDistributionBoard.Quantity} {offer.DcDistributionBoard.Item.Unit}" },
                { "{offer-DcDistBoardNetUnitPrice}", offer.DcDistributionBoard.NetUnitPrice.ToString("C2") },
                { "{offer-DcDistBoardNetAmount}", offer.DcDistributionBoard.NetAmount.ToString("C2") },

                { "{offer-DcMaterial}", offer.DcMaterial.Item.Description },
                { "{offer-DcMatQuantity}", $"{offer.DcMaterial.Quantity} {offer.DcMaterial.Item.Unit}" },
                { "{offer-DcMatNetUnitPrice}", offer.DcMaterial.NetUnitPrice.ToString("C2") },
                { "{offer-DcMatNetAmount}", offer.DcMaterial.NetAmount.ToString("C2") },

                { "{offer-InstalType}", offer.InstallationsType.Item.Description },
                { "{offer-InstalTypeQuantity}", $"{offer.InstallationsType.Quantity} {offer.InstallationsType.Item.Unit}" },
                { "{offer-InstalTypeNetUnitPrice}", offer.InstallationsType.NetUnitPrice.ToString("C2") },
                { "{offer-InstalTypeNetAmount}", offer.InstallationsType.NetAmount.ToString("C2") },

                { "{offer-InstallationWork}", offer.InstallationWork.Item.Description },
                { "{offer-InstalWorkQuantity}", $"{offer.InstallationWork.Quantity} { offer.InstallationWork.Item.Unit}" },
                { "{offer-InstalWorkNetUnitPrice}", offer.InstallationWork.NetUnitPrice.ToString("C2") },
                { "{offer-InstalWorkNetAmount}", offer.InstallationWork.NetAmount.ToString("C2") },

                { "{offer-GroundWork}", offer.GroundWork.Item.Description },
                { "{offer-GroundWQuantity}", $"{offer.GroundWork.Quantity} {offer.GroundWork.Item.Unit}" },
                { "{offer-GroundWoNetUnitPrice}", offer.GroundWork.NetUnitPrice.ToString("C2") },
                { "{offer-GroundWNetAmount}", offer.GroundWork.NetAmount.ToString("C2") },

                { "{offer-Monitoring}", offer.Monitoring.Item.Description },
                { "{offer-MonitQuantity}", $"{offer.Monitoring.Quantity} {offer.Monitoring.Item.Unit}" },
                { "{offer-MonitNetUnitPrice}", offer.Monitoring.NetUnitPrice.ToString("C2") },
                { "{offer-MonitNetAmount}", offer.Monitoring.NetAmount.ToString("C2") },

                { "{offer-Documentation}", offer.Documentation.Item.Description },
                { "{offer-DocQuantity}", $"{offer.Documentation.Quantity} {offer.Documentation.Item.Unit}" },
                { "{offer-DocNetUnitPrice}", offer.Documentation.NetUnitPrice.ToString("C2") },
                { "{offer-DocNetAmount}", offer.Documentation.NetAmount.ToString("C2") },

                { "{offer-TotalNetAmount}", offer.Total.NetAmount.ToString("C2") },
                { "{offer-VatRate}", offer.Total.VatRate },
                { "{offer-TotalVatAmount}", offer.Total.VatAmount.ToString("C2") },
                { "{offer-TotalGrossAmount}", offer.Total.GrossAmount.ToString("C2") },

                { "{offer-Warranty}", offer.Warranty.Item.Description },
                { "{offer-WarrantyQuantity}", $"{offer.Warranty.Quantity} {offer.Warranty.Item.Unit}" },
                { "{offer-WarrantyNetUnitPrice}", offer.Warranty.NetUnitPrice.ToString("C2") },
                { "{offer-WarrantyNetAmount}", offer.Warranty.NetAmount.ToString("C2") },

                { "{offer-Inspection}", offer.Inspections.Item.Description },
                { "{offer-InspectionQuantity}", $"{offer.Inspections.Quantity} {offer.Inspections.Item.Unit}" },
                { "{offer-InspectionNetUnitPrice}", offer.Inspections.NetUnitPrice.ToString("C2") },
                { "{offer-InspectionNetAmount}", offer.Inspections.NetAmount.ToString("C2") },

                { "{offer-Insurance}", offer.Insurance.Item.Description },
                { "{offer-InsuranceQuantity}", $"{offer.Insurance.Quantity} {offer.Insurance.Item.Unit}" },
                { "{offer-InsuranceNetUnitPrice}", offer.Insurance.NetUnitPrice.ToString("C2") },
                { "{offer-InsuranceNetAmount}", offer.Insurance.NetAmount.ToString("C2") },
            };

            #endregion

            if (!File.Exists(TemplateFilename))
            {
                throw new FileNotFoundException("File not found.", TemplateFilename);
            }

            var html = File.ReadAllText(TemplateFilename);

            foreach (var item in dic)
            {
                html = html.Replace(item.Key, item.Value);
            }

            File.WriteAllText(htmlFilePath, html);
        }
    }
}

using System;
using Solektro.Core.Models;

namespace Solektro.API.Helpers
{
    public class Calculations
    {
        public void Recalculate(Offer offer)
        {
            if (offer == null)
                return;

            PowerCalc(offer);
            UpdateDefaultQuantity(offer);
            UpdateWorkQuantity(offer);
            UpdateMargins(offer);
            CalculateTotal(offer);
            CalculateKwAmount(offer);
        }

        private static void PowerCalc(Offer offer)
        {
            offer.PowerCalc.Value = (offer.Panel?.Item?.Power?.Value ?? 0) * (double)(offer.Panel?.Quantity ?? 0);
        }

        private void UpdateDefaultQuantity(Offer offer)
        {
            //We want to assign a detault quantity only for specific items
            //For example we don't want to assign a detault quantity either to solar panel or inverter
            offer.AcDistributionBoard.Quantity = offer.AcDistributionBoard.Item?.DefaultQuantity ?? 0;
            offer.AcMaterial.Quantity = offer.AcMaterial.Item?.DefaultQuantity ?? 0;
            offer.DcDistributionBoard.Quantity = offer.DcDistributionBoard.Item?.DefaultQuantity ?? 0;
            offer.DcMaterial.Quantity = offer.DcMaterial.Item?.DefaultQuantity ?? 0;
            offer.Documentation.Quantity = offer.Documentation.Item?.DefaultQuantity ?? 0;
            offer.Inspections.Quantity = offer.Inspections.Item?.DefaultQuantity ?? 0;
            offer.Insurance.Quantity = offer.Insurance.Item?.DefaultQuantity ?? 0;
            offer.Monitoring.Quantity = offer.Monitoring.Item?.DefaultQuantity ?? 0;
            offer.Warranty.Quantity = offer.Warranty.Item?.DefaultQuantity ?? 0;
        }

        private void UpdateWorkQuantity(Offer offer)
        {
            var qty = (decimal)offer.PowerCalc.Value / 1000;

            offer.InstallationsType.Quantity = qty;
            offer.InstallationWork.Quantity = qty;
        }

        private void UpdateMargins(Offer offer)
        {
            offer.Panel.Margin = offer.Margin;
            offer.Inverter.Margin = offer.Margin;
            offer.Optimizer.Margin = offer.Margin;
            offer.AcDistributionBoard.Margin = offer.Margin;
            offer.AcMaterial.Margin = offer.Margin;
            offer.DcDistributionBoard.Margin = offer.Margin;
            offer.DcMaterial.Margin = offer.Margin;
            offer.InstallationsType.Margin = offer.Margin;
            offer.InstallationWork.Margin = offer.Margin;
            offer.GroundWork.Margin = offer.Margin;
            offer.Monitoring.Margin = offer.Margin;
            offer.Documentation.Margin = offer.Margin;
            offer.Warranty.Margin = offer.Margin;
            offer.Insurance.Margin = offer.Margin;
        }

        private void CalculateTotal(Offer offer)
        {
            var t = offer.Total ?? throw new NullReferenceException(nameof(offer.Total));

            var offerBasicAmount = (offer.Panel?.BasicAmount ?? 0)
                                 + (offer.Inverter?.BasicAmount ?? 0)
                                 + (offer.Optimizer?.BasicAmount ?? 0)
                                 + (offer.AcDistributionBoard?.BasicAmount ?? 0)
                                 + (offer.AcMaterial?.BasicAmount ?? 0)
                                 + (offer.DcDistributionBoard?.BasicAmount ?? 0)
                                 + (offer.DcMaterial?.BasicAmount ?? 0)
                                 + (offer.InstallationsType?.BasicAmount ?? 0)
                                 + (offer.InstallationWork?.BasicAmount ?? 0)
                                 + (offer.GroundWork?.BasicAmount ?? 0)
                                 + (offer.Monitoring?.BasicAmount ?? 0)
                                 + (offer.Documentation?.BasicAmount ?? 0);


            var offerNetAmount = (offer.Panel?.NetAmount ?? 0)
                               + (offer.Inverter?.NetAmount ?? 0)
                               + (offer.Optimizer?.NetAmount ?? 0)
                               + (offer.AcDistributionBoard?.NetAmount ?? 0)
                               + (offer.AcMaterial?.NetAmount ?? 0)
                               + (offer.DcDistributionBoard?.NetAmount ?? 0)
                               + (offer.DcMaterial?.NetAmount ?? 0)
                               + (offer.InstallationsType?.NetAmount ?? 0)
                               + (offer.InstallationWork?.NetAmount ?? 0)
                               + (offer.GroundWork?.NetAmount ?? 0)
                               + (offer.Monitoring?.NetAmount ?? 0)
                               + (offer.Documentation?.NetAmount ?? 0);


            var vatFactor = 1 + t.VatRate.Value;
            var roundedGross = RoundTo(offerNetAmount * vatFactor);
            t.NetAmount = roundedGross / vatFactor;
            t.VatAmount = t.NetAmount * t.VatRate.Value;
            t.GrossAmount = t.NetAmount + t.VatAmount;
            t.MarginAmount = t.NetAmount - offerBasicAmount;
        }

        private static void CalculateKwAmount(Offer offer)
        {
            if (offer.KitPower?.Value > 0)
                offer.Total.KwAmount = offer.Total.NetAmount / (decimal)(offer.KitPower.Value);
            else
                offer.Total.KwAmount = 0;
        }

        private decimal RoundTo(decimal value)
        {
            const decimal roundingPoint = 100;
            return Math.Round(value / roundingPoint, 0, MidpointRounding.AwayFromZero) * roundingPoint;
        }
    }
}

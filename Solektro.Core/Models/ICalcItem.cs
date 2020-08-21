namespace Solektro.Core.Models
{
    public interface ICalcItem
    {
        public decimal Quantity { get; set; }

        public string Unit { get; set; }

        public decimal BasicUnitPrice { get; set; }

        public decimal BasicAmount { get; }

        public Margin Margin { get; set; }

        public decimal NetUnitPrice { get; }

        public decimal NetAmount { get; }
    }
}

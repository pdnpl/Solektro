using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Solektro.Core.Models
{
    public class Line<T> : INotifyPropertyChanged where T : BaseItem
    {
        public LineType Type { get; }

        public T Item { get => _item; set { _item = value; NotifyPropertyChanged(); } }
        private T _item;

        public decimal Quantity { get => _quantity; set { _quantity = value; NotifyPropertyChanged(); NotifyPropertyChanged(nameof(BasicAmount)); NotifyPropertyChanged(nameof(NetAmount)); } }
        private decimal _quantity;

        public decimal BasicAmount { get => Quantity * Item?.BasicUnitPrice ?? 0; }

        public Margin Margin { get => _margin; set { _margin = value; NotifyPropertyChanged(); NotifyPropertyChanged(nameof(NetUnitPrice)); NotifyPropertyChanged(nameof(NetAmount)); } }
        private Margin _margin;

        public decimal NetUnitPrice { get => (Item?.BasicUnitPrice ?? 0) * (1 + Margin?.Value ?? 0); }

        public decimal NetAmount { get => Quantity * NetUnitPrice; }

        public Line(LineType lineType)
        {
            Type = lineType;
        }


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

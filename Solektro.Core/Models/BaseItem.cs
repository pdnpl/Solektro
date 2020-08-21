using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Solektro.Core.Models
{
    public abstract class BaseItem : INotifyPropertyChanged
    {
        public decimal DefaultQuantity { get => _defaultQuantity; set { _defaultQuantity = value; NotifyPropertyChanged(); } }
        private decimal _defaultQuantity;

        public string Unit { get => _unit; set { _unit = value; NotifyPropertyChanged(); } }
        private string _unit;

        public decimal BasicUnitPrice { get => _basicUnitPrice; set { _basicUnitPrice = value; NotifyPropertyChanged(); } }
        private decimal _basicUnitPrice;


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

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Solektro.Core.Models
{
    public class Total : INotifyPropertyChanged
    {
        public VatRate VatRate { get => _vatRate; set { _vatRate = value; NotifyPropertyChanged(); } }
        private VatRate _vatRate = new VatRate();

        public decimal NetAmount { get => _netAmount; set { _netAmount = value; NotifyPropertyChanged(); } }
        private decimal _netAmount;

        public decimal VatAmount { get => _vatAmount; set { _vatAmount = value; NotifyPropertyChanged(); } }
        private decimal _vatAmount;

        public decimal GrossAmount { get => _grossAmount; set { _grossAmount = value; NotifyPropertyChanged(); } }
        private decimal _grossAmount;

        public decimal MarginAmount { get => _marginAmount; set { _marginAmount = value; NotifyPropertyChanged(); } }
        private decimal _marginAmount;

        public decimal KwAmount { get => _kwAmount; set { _kwAmount = value; NotifyPropertyChanged(); } }
        private decimal _kwAmount;


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

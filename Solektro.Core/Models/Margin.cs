using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Solektro.Core.Models
{
    public class Margin: INotifyPropertyChanged
    {

        public decimal Value { get => _value; set { _value = value; NotifyPropertyChanged(); } }
        private decimal _value;

        public Margin ()
        {
        }

        public Margin (decimal value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value.ToString("P0");
        }

        public static implicit operator string(Margin val)
        {
            return val.ToString();
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

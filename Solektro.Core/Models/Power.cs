using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Solektro.Core.Models
{
    public class Power : INotifyPropertyChanged, IComparable<Power>, IComparable
    {
        public double Value { get => _value; set { _value = value; NotifyPropertyChanged(); NotifyPropertyChanged(nameof(Text)); } }
        private double _value;

        public string Text { get => ToString(); }

        public Power()
        {
        }

        public Power(double value)
        {
            Value = value;
        }


        public override string ToString()
        {
            const double kW = 1000;
            const double MW = kW * 1000;
            const double GW = MW * 1000;

            if (Value >= GW)
                return (Value / GW).ToString("N2") + " GW";

            else if (Value >= MW && Value < GW)
                return (Value / MW).ToString("N2") + " MW";

            else if (Value >= kW && Value < MW)
                return (Value / kW).ToString("N2") + " kW";

            else
                return Value.ToString("N0") + " W";
        }

        public static implicit operator string(Power val)
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

        #region IComparable

        public int CompareTo([AllowNull] Power other)
        {
            return Value.CompareTo(other?.Value);
        }

        public int CompareTo(object obj)
        {
            return CompareTo(obj as Power);
        }

        #endregion
    }
}

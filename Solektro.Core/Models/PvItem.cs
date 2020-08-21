using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Solektro.Core.Models
{
    public class PvItem : BaseItem
    {
        public double Id { get => _id; set { _id = value; NotifyPropertyChanged(); } }
        private double _id;

        public string Manufacturer { get => _manufacturer; set { _manufacturer = value; NotifyPropertyChanged(); } }
        private string _manufacturer;

        public string Model { get => _model; set { _model = value; NotifyPropertyChanged(); } }
        private string _model;

        public Power Power { get => _power; set { _power = value; NotifyPropertyChanged(); } }
        private Power _power;
    }
}

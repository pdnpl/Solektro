using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Solektro.Core.Models
{
    public class OtherItem : BaseItem
    {
        public double Id { get => _id; set { _id = value; NotifyPropertyChanged(); } }
        private double _id;

        public string Description { get => _description; set { _description = value; NotifyPropertyChanged(); } }
        private string _description;

    }
}

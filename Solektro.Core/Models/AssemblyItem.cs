namespace Solektro.Core.Models
{
    public class AssemblyItem : BaseItem
    {
        public double Id { get => _id; set { _id = value; NotifyPropertyChanged(); } }
        private double _id;

        public string Type { get => _type; set { _type = value; NotifyPropertyChanged(); } }
        private string _type;

        public string Description { get => _description; set { _description = value; NotifyPropertyChanged(); } }
        private string _description;

    }
}

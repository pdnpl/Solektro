using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Solektro.Core.Models
{
    public class User : INotifyPropertyChanged
    {
        public int Id { get => _id; set { _id = value; NotifyPropertyChanged(); } }
        private int _id;

        public string Name { get => _name; set { _name = value; NotifyPropertyChanged(); } }
        private string _name;

        public string Phone { get => _phone; set { _phone = value; NotifyPropertyChanged(); } }
        private string _phone;

        public string Email { get => _email; set { _email = value; NotifyPropertyChanged(); } }
        private string _email;
        


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

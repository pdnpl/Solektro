using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Solektro.Core.Models
{
    public class Offer : INotifyPropertyChanged
    {
        public DateTime Date { get => _dateTime; set { _dateTime = value; NotifyPropertyChanged(); } }
        private DateTime _dateTime;

        public User Consultant { get => _consultant; set { _consultant = value; NotifyPropertyChanged(); } }
        private User _consultant;

        public Kit Kit { get => _kit; set { _kit = value; NotifyPropertyChanged(); } }
        private Kit _kit;

        public Power KitPower { get => _kitPower; set { _kitPower = value; NotifyPropertyChanged(); } }
        private Power _kitPower;

        public Margin Margin { get => _margin; set { _margin = value; NotifyPropertyChanged(); } }
        private Margin _margin;

        public Line<PvItem> Panel { get; set; } = new Line<PvItem>(LineType.SolarPanel);

        public Line<PvItem> Inverter { get; set; } = new Line<PvItem>(LineType.Inverter);

        public Line<PvItem> Optimizer { get; set; } = new Line<PvItem>(LineType.Optimizer);

        public Line<OtherItem> AcDistributionBoard { get; set; } = new Line<OtherItem>(LineType.AcDistributionBoard);

        public Line<OtherItem> AcMaterial { get; set; } = new Line<OtherItem>(LineType.AcMaterial);

        public Line<OtherItem> DcDistributionBoard { get; set; } = new Line<OtherItem>(LineType.DcDistributionBoard);

        public Line<OtherItem> DcMaterial { get; set; } = new Line<OtherItem>(LineType.DcMaterial);

        public Line<AssemblyItem> InstallationsType { get; set; } = new Line<AssemblyItem>(LineType.InstallationsType);

        public Line<OtherItem> InstallationWork { get; set; } = new Line<OtherItem>(LineType.InstallationWork);

        public Line<OtherItem> GroundWork { get; set; } = new Line<OtherItem>(LineType.GroundWork);

        public Line<OtherItem> Monitoring { get; set; } = new Line<OtherItem>(LineType.Monitoring);

        public Line<OtherItem> Documentation { get; set; } = new Line<OtherItem>(LineType.Documentation);

        public Line<OtherItem> Warranty { get; set; } = new Line<OtherItem>(LineType.Warranty);

        public Line<OtherItem> Inspections { get; set; } = new Line<OtherItem>(LineType.Inspections);

        public Line<OtherItem> Insurance { get; set; } = new Line<OtherItem>(LineType.Insurance);

        public Power PowerCalc { get; set; } = new Power();

        public Total Total { get => _total; set { _total = value; NotifyPropertyChanged(); } }
        private Total _total = new Total();


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

using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Solektro.Core.Models
{
    public class Kit 
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public int DefaultPanelId { get; set; }

        public int DefaultInverterId { get; set; }

        public int DefaultInstallationTypeId { get; set; }

    }
}

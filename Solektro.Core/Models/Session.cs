using System.ComponentModel;
using System.Runtime.CompilerServices;
using Solektro.Core.Models;

namespace Solektro.Core.Models
{
    public class Session
    {
        public Kit Kit { get; set; }

        public Power KitPower { get; set; }

        public Margin Margin { get; set; }

        public VatRate VatRate { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Solektro.Core.Models;
using System.Linq;

namespace Solektro.API.Helpers
{
    public static class InverterHelper
    {
        private const double Margin = 0.15;

        public static PvItem FindInverter(IEnumerable<PvItem> inverters, Power power)
        {
            if ((inverters == null) || (power == null))
                throw new ArgumentNullException();

            var sortedList = inverters.OrderBy(x => x.Power);

            foreach (var inverter in sortedList)
            {
                if (inverter.Power.Value * (1 + Margin) >= power.Value)
                    return inverter;
            }

            return null;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Solektro.API.Helpers;
using Solektro.Core.Models;

namespace Solektro.API
{
    public class Data
    {
        private const string basePath = @"D:\Dev\Solektro\Data";

        //OpenOffer
        //SaveOffer


        #region Data from JSON

        public IEnumerable<Kit> GetKits()
        {
            return GetFromFile<Kit>(Path.Combine(basePath, "Kits.json"));
        }

        public IEnumerable<PvItem> GetSolarPanels()
        {
            return GetFromFile<PvItem>(Path.Combine(basePath, "SolarPanels.json"));
        }

        public IEnumerable<PvItem> GetInverters()
        {
            return GetFromFile<PvItem>(Path.Combine(basePath, "Inverters.json"));
        }

        public IEnumerable<PvItem> GetOptimizers()
        {
            return GetFromFile<PvItem>(Path.Combine(basePath, "Optimizers.json"));
        }

        public IEnumerable<OtherItem> GetAcDistributionBoards()
        {
            return GetFromFile<OtherItem>(Path.Combine(basePath, "AcDistributionBoards.json"));
        }

        public IEnumerable<OtherItem> GetAcWires()
        {
            return GetFromFile<OtherItem>(Path.Combine(basePath, "AcWires.json"));
        }

        public IEnumerable<OtherItem> GetDcDistributionBoard()
        {
            return GetFromFile<OtherItem>(Path.Combine(basePath, "DcDistributionBoard.json"));
        }

        public IEnumerable<OtherItem> GetDcWires()
        {
            return GetFromFile<OtherItem>(Path.Combine(basePath, "DcWires.json"));
        }

        public IEnumerable<AssemblyItem> GetInstallationsTypes()
        {
            return GetFromFile<AssemblyItem>(Path.Combine(basePath, "InstallationsTypes.json"));
        }

        public IEnumerable<OtherItem> GetInstallationWorks()
        {
            return GetFromFile<OtherItem>(Path.Combine(basePath, "InstallationWorks.json"));
        }

        public IEnumerable<OtherItem> GetGroundWorks()
        {
            return GetFromFile<OtherItem>(Path.Combine(basePath, "GroundWorks.json"));
        }

        public IEnumerable<OtherItem> GetMonitorings()
        {
            return GetFromFile<OtherItem>(Path.Combine(basePath, "Monitorings.json"));
        }

        public IEnumerable<OtherItem> GetDocumentations()
        {
            return GetFromFile<OtherItem>(Path.Combine(basePath, "Documentations.json"));
        }

        public IEnumerable<OtherItem> GetWarranties()
        {
            return GetFromFile<OtherItem>(Path.Combine(basePath, "Warranties.json"));
        }

        public IEnumerable<OtherItem> GetInspections()
        {
            return GetFromFile<OtherItem>(Path.Combine(basePath, "Inspections.json"));
        }

        public IEnumerable<OtherItem> GetInsurances()
        {
            return GetFromFile<OtherItem>(Path.Combine(basePath, "Insurances.json"));
        }

        #endregion

        #region Other data

        public IEnumerable<Power> GetPowers()
        {
            return new List<Power>
            {
                new Power(1000),
                new Power(2000),
                new Power(3000),
                new Power(5000),
                new Power(10000),
                new Power(15000),
                new Power(20000),
                new Power(25000),
                new Power(30000),
                new Power(40000),
                new Power(50000)
            };
        }

        public IEnumerable<VatRate> GetVats()
        {
            return new List<VatRate>
            {
                new VatRate(0.08m),
                new VatRate(0.23m)
            };
        }

        public IEnumerable<Margin> GetMargins()
        {
            var list = new List<Margin>();

            for (int i = 1; i <= 100; i++)
            {
                list.Add(new Margin(i / 100m));
            }

            return list;
        }

        #endregion

        #region Private

        private static JsonSerializerOptions GetJSO()
        {
            var jso = new JsonSerializerOptions()
            {
                WriteIndented = true
            };

            jso.Converters.Add(new PowerJsonConverter());

            return jso;
        }


        private IEnumerable<T> GetFromFile<T>(string filePath)
        {
            var json = File.ReadAllText(filePath);
            var model = JsonSerializer.Deserialize<IEnumerable<T>>(json, GetJSO());
            return model;
        }

        #endregion

        #region ExampleData

        public void ExportExampleData()
        {
            SerialiseExampleData(GetKits().ToList());
            SerialiseExampleData(GetSolarPanels().ToList());
            SerialiseExampleData(GetInverters().ToList());
        }

        private void SerialiseExampleData<T>(T obj) where T : IList
        {
            var filePath = Path.Combine(@"D:\Dev\Solektro-ExampleData", obj[0].GetType().Name + ".json");
            var json = JsonSerializer.Serialize(obj, GetJSO());
            File.WriteAllText(filePath, json);
        }

        #endregion
    }
}

using System.IO;
using System.Text.Json;
using Solektro.Core.Models;

namespace Solektro.API
{
    public class SessionManager
    {
        private const string fileName = "Session.json";

        private readonly JsonSerializerOptions jso = new JsonSerializerOptions() { WriteIndented = true };

        public Session ReadSession()
        {
            if (!File.Exists(fileName))
            {
                return null;
            }

            var json = File.ReadAllText(fileName);
            var model = JsonSerializer.Deserialize<Session>(json, jso);
            return model;
        }

        public void SaveSession(Session session)
        {
            var json = JsonSerializer.Serialize(session, jso);
            File.WriteAllText(fileName, json);
        }
    }
}

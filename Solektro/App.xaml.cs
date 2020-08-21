using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Markup;
using Solektro.Windows;

namespace Solektro
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var culture = new CultureInfo("pl-PL");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;

            var mainWindow = new MainWindow
            {
                Language = XmlLanguage.GetLanguage("pl")
            };

            mainWindow.Show();
        }
    }
}

using System.Reflection;
using System.Windows;

namespace qa_eval_finale_memento
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            // Set the default menuDropAlignment to right instead of left
            if (SystemParameters.MenuDropAlignment)
            {
                var t = typeof(SystemParameters);
                var field = t.GetField("_menuDropAlignment", BindingFlags.NonPublic | BindingFlags.Static);
                field?.SetValue(null, false);
            }

            MainView mainView = new();
            mainView.Show();

            base.OnStartup(e);
        }
    }
}
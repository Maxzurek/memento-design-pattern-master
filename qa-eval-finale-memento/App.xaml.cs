using Microsoft.Extensions.DependencyInjection;
using qa_eval_finale_memento.ViewModels;
using System;
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
            IServiceProvider serviceProvider = CreateServiceProvider();

            MainView window = new MainView();
            window.Show();

            base.OnStartup(e);
        }

        private IServiceProvider CreateServiceProvider()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<IOriginator, MainViewModel>();

            services.AddScoped<MainViewModel>();


            return services.BuildServiceProvider();
        }
    }
}

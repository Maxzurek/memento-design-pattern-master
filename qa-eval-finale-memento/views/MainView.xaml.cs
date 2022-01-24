using qa_eval_finale_memento.ViewModels;
using System.Windows;

namespace qa_eval_finale_memento
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
        }

        private void textBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                MainViewModel mainViewModel = (MainViewModel)DataContext;
                mainViewModel.handleViewEnterKeyDown();
            }
        }
    }
}

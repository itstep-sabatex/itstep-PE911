using Cafe.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CafeBar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            login.LogginController = new LoginController<CafeBarDbContext>(Cafe.Models.AccessLevel.Barmen);

        }
        private void login_LoginResult(bool arg1, int arg2)
        {
            if (!arg1) Close();
            login.Visibility = Visibility.Collapsed;
            //_currentUserId = arg2;
            //SetActiveFrame(CafeFrame.Main);
        }
    }
}

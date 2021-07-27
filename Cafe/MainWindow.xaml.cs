using Cafe.Data;
using Microsoft.EntityFrameworkCore;
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

namespace Cafe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (var dbContext = new CafeDbContext())
            {
                //var badWaiter = dbContext.Waiter.Single(s => s.Id == 1);
                //badWaiter.Name = "Semen Semenov";
                //dbContext.SaveChanges();

                var waiters = dbContext.UserAccesLevels
                    .Where(w => w.AccessLevel == Models.AccessLevel.Waiter)
                    .Include(i=>i.User)
                    .Select(w => new {Id=w.Id,Name=w.User.Name })
                    .ToArray();

                dg.ItemsSource = waiters;

            }

        }
    }
}

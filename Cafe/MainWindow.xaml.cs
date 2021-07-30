using Cafe.Data;
using Cafe.Models;
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
        int _currentUserId { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            login.LogginController = new LoginController<CafeDbContext>(Models.AccessLevel.Waiter);
        }
        
        private void SetActiveFrame(CafeFrame cafeFrame)
        {
            login.Visibility = Visibility.Collapsed;
            dg.Visibility = Visibility.Collapsed;
            switch (cafeFrame)
            {
                case CafeFrame.Login:
                    login.Visibility = Visibility.Visible;
                    break;
                case CafeFrame.Main:
                    dg.Visibility = Visibility.Visible;
                    using (var dbContext = new CafeDbContext())
                    {
                        //var badWaiter = dbContext.Waiter.Single(s => s.Id == 1);
                        //badWaiter.Name = "Semen Semenov";
                        //dbContext.SaveChanges();
                        //CREATE FUNCTION GetMyProc(@id int)
                        //RETURN TABLE
                        //AS
                        //RETURN
                        //(
                        //     SELECT * FROM UserAccessLevels
                        //     WHERE [Id] = @id
                        //)
                        //var waiters = dbContext.UserAccesLevels.FromSqlRaw("EXECUTE GetMyProc {0}",10) 
                        //      .Where(w => w.AccessLevel == Models.AccessLevel.Waiter)
                        //      .Include(i => i.User)
                        //      .Select(w => new { Id = w.Id, Name = w.User.Name })
                        //      .ToArray();
                        var users = dbContext.Users.Include(i=>i.UserAccesLevels).ToArray();

                        //var su = System.Text.Json.JsonSerializer.Serialize(users);


                        dg.ItemsSource = users;


                        var nu = new User
                        {
                            Name = "Zuzana Chaputova",
                            Password = "12345",
                            UserAccesLevels = new List<UserAccesLevel>
                            {
                                new UserAccesLevel{AccessLevel = AccessLevel.Barmen},
                                new UserAccesLevel{AccessLevel = AccessLevel.Waiter},
                                new UserAccesLevel{ AccessLevel = AccessLevel.Admin}
                            }
                        };
                        //dbContext.Users.Add(nu);
                        //dbContext.SaveChanges();


                    }
                    break;

            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetActiveFrame(CafeFrame.Login); 
        }

        private void login_LoginResult(bool arg1, int arg2)
        {
            if (!arg1) Close();
            _currentUserId = arg2;
            SetActiveFrame(CafeFrame.Main);
        }
    }
}

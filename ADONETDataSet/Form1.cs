using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADONETDataSet
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'restoranDS.Waiter' table. You can move, or remove it, as needed.
            this.waiterTableAdapter.Fill(this.restoranDS.Waiter);

            using (var connection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\DataBases\restoran.mdf; Integrated Security = True; Connect Timeout = 30"))
            {
                var waitersDapper = connection.Query<Models.Waiter>("select * from waiter");
                var waitersDapper1 = connection.QueryFirst<Models.Waiter>("select * from waiter");
                var dwaiter = new Models.Waiter() { Name = "Petre Fasto" };
                connection.Execute("insert into waiter (Name) values (@name)",new {name = dwaiter.Name,Id=10 });
                
                var rd = connection.ExecuteReader("select * from waiter where id=@id",new {id=1 });
                var sc = connection.ExecuteScalar("select count(Id) from waiter");


                var cmd = connection.CreateCommand();
                cmd.CommandText = "select * from waiter";
                cmd.ExecuteReader();




                //var cmd = connection.CreateCommand();
                cmd.CommandText = "insert into waiter (Name) values (@name)";
                var par = cmd.CreateParameter();
                par.ParameterName = "name";
                par.Value = dwaiter.Name;
                cmd.Parameters.Add(par);
                cmd.ExecuteNonQuery();
                


                DataContext db = new DataContext(connection);
                Table<Models.Waiter> tableWaiters = db.GetTable<Models.Waiter>();

                tableWaiters.Take(10);
                var waiterLinqToSQL = tableWaiters.ToArray();

                var nwaiter = new Models.Waiter() { Name = "Petre Fasto" };
                tableWaiters.InsertOnSubmit(nwaiter);
                db.SubmitChanges();

            }



            //DataContext db = new DataContext(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\DataBases\restoran.mdf; Integrated Security = True; Connect Timeout = 30");
            //Table<Models.Waiter> waiters = db.GetTable<Models.Waiter>();
            

 
            //Models.Waiter waiterEdited = db.GetTable<Models.Waiter>().SingleOrDefault(s => s.Id == 1);
            //waiterEdited.Name = "Петренко Іван Іванович";
            //db.SubmitChanges();


            //var rs = from u in db.GetTable<Models.Waiter>()
            //         where u.Name.Contains("w")
            //         select u;

            
            //var r = waiters.GroupBy(g=>g.Name);

            //foreach (var waiter in r)
            //{
            //    foreach (var g in waiter.ToArray())
            //    {
            //        Console.WriteLine($"{waiter.Key} {g.Id} {g.Name}");
            //    }
              
            //}

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.waiterTableAdapter.Update(this.restoranDS.Waiter);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var waiter = waiterBindingSource.Current as DataRow;
            if (waiter != null)
            {
                int id = (int)waiter["Id"];
                DataContext db = new DataContext(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\DataBases\restoran.mdf; Integrated Security = True; Connect Timeout = 30");
                Table<Models.Waiter> waiters = db.GetTable<Models.Waiter>();
                var o = waiters.SingleOrDefault(s => s.Id == (int)waiter["Id"]);
                waiters.DeleteOnSubmit(o);
                db.SubmitChanges();

            }
        }
    }
}

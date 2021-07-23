using System;
using System.Data;
using System.IO;
using System.Linq;

namespace ADONETDemoConnection
{
    class Program
    {
        static int? getCountryId(IDbCommand dbCommand,string country)
        {
            dbCommand.CommandText = "select Id from Country where Name=@Country";
            var pName = dbCommand.CreateParameter();
            pName.ParameterName = "Country";
            pName.Value = country;
            dbCommand.Parameters.Add(pName);
            return (int?)dbCommand.ExecuteScalar();
        }


        static void Main(string[] args)
        {
            using (IDbConnection dbConnection = new System.Data.SqlClient.SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\DataBases\restoran.mdf;Integrated Security=True;Connect Timeout=30"))
            {
                dbConnection.Open();

                var str = File.ReadAllText(@"C:\DataBases\DemoData.json");
                var demoData = System.Text.Json.JsonSerializer.Deserialize<PeopleDataDemo[]>(str);
                var rnd = new Random();
                int index = rnd.Next(1, 10000);
                var r = demoData.Single(s=>s.id==index);
               
                var tr = dbConnection.BeginTransaction();
                var dbCommand = dbConnection.CreateCommand();
                dbCommand.Transaction = tr;
                var countryId = getCountryId(dbCommand,r.Country);
                if (countryId == null)
                {
                    dbCommand = dbConnection.CreateCommand();
                    dbCommand.Transaction = tr;
                    dbCommand.CommandText = "insert into Country (Name) values (@name)";
                    var p = dbCommand.CreateParameter();
                    p.ParameterName = "name";
                    p.Value = r.Country;
                    dbCommand.Parameters.Add(p);
                    dbCommand.ExecuteNonQuery();

                    dbCommand = dbConnection.CreateCommand();
                    dbCommand.Transaction = tr;

                    countryId = getCountryId(dbCommand, r.Country);
                }


                dbCommand = dbConnection.CreateCommand();
                dbCommand.Transaction = tr;
                dbCommand.CommandText = "insert into Waiter (name,CountryId) values (@name,@countryId)";
                var pName = dbCommand.CreateParameter();
                pName.ParameterName = "name";
                pName.Value = $"{r.firstname} {r.lastname}";
                dbCommand.Parameters.Add(pName);
                pName = dbCommand.CreateParameter();
                pName.ParameterName = "countryId";
                pName.Value = countryId.Value;
                dbCommand.Parameters.Add(pName);
                dbCommand.ExecuteNonQuery();
                tr.Rollback();



                dbCommand = dbConnection.CreateCommand();
                dbCommand.CommandText = "select Id,Name from Waiter";
                var dBReader = dbCommand.ExecuteReader();
                while (dBReader.Read())
                {
                    Console.WriteLine($"{dBReader.GetInt32(0)} {dBReader.GetString(1)}");
                }
                dBReader.Close();

                dbCommand = dbConnection.CreateCommand();
                dbCommand.CommandText = "select count(Id) from Waiter";
                var count = dbCommand.ExecuteScalar();
                Console.WriteLine($"Кількість офіціантів  = {count}");

                Console.WriteLine("End !");
                dbConnection.Close();
            }

        }
    }
}

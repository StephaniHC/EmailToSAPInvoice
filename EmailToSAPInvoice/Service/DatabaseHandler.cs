using EmailToSAPInvoice.Models;
using EmailToSAPInvoice.Service.Table;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace EmailToSAPInvoice.Service
{
    public class DatabaseHandler
    {
        public SQLiteConnection database;
        private const string DATABASE_NAME = "BD_EMAIL.db";

        public DatabaseHandler()
        {
            string databasePath;
            var directory = "Ruta.json";
            if (File.Exists(directory))
            {
                string json = File.ReadAllText(directory);
                Route ruta = JsonSerializer.Deserialize<Route>(json);
                databasePath = Path.Combine(ruta.Base, DATABASE_NAME);
            }
            else
            {
                databasePath = Path.Combine(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).FullName).FullName).FullName, DATABASE_NAME);
                //databasePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), DATABASE_NAME);
            }
            database = new SQLiteConnection(databasePath);
            database.CreateTable<Datas>();
        }
        public List<Datas> GetAllDatasEmail()
        {
            return database.Query<Datas>("SELECT Date, Subject, Attached, Status FROM Datas").ToList();
        }

        public Datas InsertData(string date, string subject, string attached)
        {
            var newDatas = new Datas()
            {
                Date = date,
                Subject = subject,
                Attached = attached,
                Status = "Pendiente"
            };
            database.Insert(newDatas);
            return newDatas;
        }

        public List<Datas> GetPendingEmails()
        {
            return database.Query<Datas>("SELECT  Attached, Status FROM Datas WHERE Status='Pendiente'").ToList();
        }
    }
}

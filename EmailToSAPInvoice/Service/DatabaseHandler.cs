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
            database.CreateTable<Rperfil>();
            database.CreateTable<Rcuenta>();
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
                Status = Datas.StatusPending,
                Observation = ""
            };
            database.Insert(newDatas);
            return newDatas;
        }

        public List<Datas> GetPendingEmails()
        {
            return database.Query<Datas>("SELECT Id, Attached, Status FROM Datas WHERE Status='Pendiente'").ToList();
        }
         
        public void UpdateStatus(object id, string status, object observation)
        {
            database.Execute("UPDATE Datas SET Status = ?, Observation = ? WHERE Id = ?", status, observation, id);
        } 
        public List<Rperfil> GetAllPerfil()
        {
            return database.Query<Rperfil>("SELECT U_CodPerfil, U_NombrePerfil, U_Trabaja FROM Rperfil").ToList();
        }
        public List<Rcuenta> GetAllCuenta()
        {
            return database.Query<Rcuenta>("SELECT U_IdDocumento,U_CodPerfil,U_TipDoc,U_EXENTOpercent,U_IdTipoDoc,U_TipoCalc,U_IVApercent,U_IVAcuenta,U_ITpercent,U_ITcuenta,U_IUEpercent,U_IUEcuenta,U_RCIVApercent,U_RCIVAcuenta,U_CTAexento,U_TASA FROM Rcuenta").ToList();
        }
        public Rperfil InsertPerfil(string U_NombrePerfil, string U_Trabaja )
        {
            var newRperfil = new Rperfil()
            {
                U_NombrePerfil = U_NombrePerfil,
                U_Trabaja = U_Trabaja 
            };
            database.Insert(newRperfil);
            return newRperfil;
        }

        public Rcuenta InsertCuenta(string U_CodPerfil, string U_TipDoc, decimal U_EXENTOpercent, string U_IdTipoDoc, string U_TipoCalc, decimal U_IVApercent, 
                                    string U_IVAcuenta, decimal U_ITpercent, string U_ITcuenta, decimal U_IUEpercent, string U_IUEcuenta, 
                                    decimal U_RCIVApercent, string U_RCIVAcuenta, string U_CTAexento, decimal U_TASA)
        {
            var newRcuenta = new Rcuenta()
            {
                U_CodPerfil = U_CodPerfil,
                U_TipDoc = U_TipDoc,
                U_EXENTOpercent = U_EXENTOpercent,
                U_IdTipoDoc = U_IdTipoDoc,
                U_TipoCalc = U_TipoCalc,
                U_IVApercent = U_IVApercent,
                U_IVAcuenta = U_IVAcuenta,
                U_ITpercent = U_ITpercent,
                U_ITcuenta = U_ITcuenta,
                U_IUEpercent = U_IUEpercent,
                U_IUEcuenta = U_IUEcuenta,
                U_RCIVApercent = U_RCIVApercent,
                U_RCIVAcuenta = U_RCIVAcuenta,
                U_CTAexento = U_CTAexento,
                U_TASA = U_TASA
            };
            database.Insert(newRcuenta);
            return newRcuenta;
        }
    }
}

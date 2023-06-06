using Laokontroll.Models;
using Laokontroll.Views;
using SQLite;
using System;
using System.IO;
using Xamarin.Forms;

namespace Laokontroll
{
    public partial class App : Application
    {
        public const string databaseName = "database.db";
        public static WarehouseDatabase database;

        public static WarehouseDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new WarehouseDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), databaseName));
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage( new Pealeht());
        }

       
    }
}

using Laokontroll.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;

namespace Laokontroll
{
    public class WarehouseDatabase
    {
        SQLiteConnection database;

        /*public WarehouseDatabase(string databasePath)
        {
            database = new SQLiteConnection(databasePath);
            database.CreateTable<Laos>();
            database.CreateTable<Models.Object>();
        }*/
        public WarehouseDatabase(string databasePath)
        {
            string fullPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), databasePath);
            database = new SQLiteConnection(fullPath);
            database.CreateTable<Laos>();
            database.CreateTable<Models.Object>();
        }

        public IEnumerable<Models.Object> GetObjects()
        {
            return database.Table<Models.Object>().ToList();
        }

        public int SaveObject(Models.Object obj)
        {
            if (obj.ObjektId != 0)
            {
                database.Update(obj);
                return obj.ObjektId;
            }
            else
            {
                return database.Insert(obj);
            }
        }

        public int DeleteObject(Models.Object obj)
        {
            return database.Delete(obj);
        }

        public List<Laos> GetWarehouses()
        {
            return database.Table<Laos>().ToList();
        }

        public int SaveWarehouse(Laos laos)
        {
            if (laos.LaosId != 0)
            {
                database.Update(laos.LaosId); // Исправлено
                return laos.LaosId;
            }
            else
            {
                return database.Insert(laos);
            }
        }

        public int DeleteWarehouse(Laos laos)
        {
            return database.Delete(laos); // Исправлено

        }


    }
}

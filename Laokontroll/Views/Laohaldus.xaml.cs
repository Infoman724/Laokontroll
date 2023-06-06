using Laokontroll.Models;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Laokontroll.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Laohaldus : ContentPage
    {
        private WarehouseDatabase database;

        public Laohaldus(WarehouseDatabase database)
        {
            this.database = database;

            ListView warehouseListView = new ListView
            {
                ItemsSource = GetWarehouseList(),
                ItemTemplate = new DataTemplate(typeof(TextCell))
            };
            warehouseListView.ItemTemplate.SetBinding(TextCell.TextProperty, "Nimetus");
            warehouseListView.ItemSelected += OnWarehouseSelected;

            Content = warehouseListView;
        }

        private void OnWarehouseSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Laos selectedWarehouse = (Laos)e.SelectedItem;

            bool delete = DisplayAlert("Подтвердить удаление", "Вы уверены, что хотите удалить склад?", "Да", "Нет").Result;
            if (delete)
            {
                App.Database.SaveWarehouse(selectedWarehouse);
                DisplayAlert("Успех", "Склад удален", "ОК").Wait();

                ListView warehouseListView = (ListView)sender;
                warehouseListView.ItemsSource = GetWarehouseList();
            }
        }

        private List<Models.Laos> GetWarehouseList()
        {
            return App.Database.GetWarehouses();
        }
    }
}

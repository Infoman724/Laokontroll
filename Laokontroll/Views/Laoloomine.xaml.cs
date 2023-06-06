using Laokontroll.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Laokontroll.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Laoloomine : ContentPage
    {
        private WarehouseDatabase database;
        private Entry warehouseNameEntry;

        public Laoloomine(WarehouseDatabase database)
        {
            this.database = database;

            warehouseNameEntry = new Entry
            {
                Placeholder = "Имя склада"
            };

            Button createButton = new Button
            {
                Text = "Создать"
            };
            createButton.Clicked += OnCreateClicked;

            Content = new StackLayout
            {
                Children = { warehouseNameEntry, createButton }
            };
        }

        private async void OnCreateClicked(object sender, EventArgs e)
        {
            string warehouseName = warehouseNameEntry.Text;
            Laos laos = new Laos { Nimetus = warehouseName };

            App.Database.SaveWarehouse(laos);
            await DisplayAlert("Успех", "Склад создан", "ОК");

            await Navigation.PopAsync();
        }
    }

}
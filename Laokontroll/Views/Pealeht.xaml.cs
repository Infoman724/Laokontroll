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
    public partial class Pealeht : ContentPage
    {
        private WarehouseDatabase database;

        public Pealeht()
        {
            database = new WarehouseDatabase("database.db");

            Button createWarehouseButton = new Button
            {
                Text = "Создать склад"
            };
            createWarehouseButton.Clicked += OnCreateWarehouseClicked;

            Button manageWarehouseButton = new Button
            {
                Text = "Управлять складом"
            };
            manageWarehouseButton.Clicked += OnManageWarehouseClicked;

            Content = new StackLayout
            {
                Children = { createWarehouseButton, manageWarehouseButton }
            };
        }

        private void OnCreateWarehouseClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Laoloomine(database));
        }

        private void OnManageWarehouseClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Laohaldus(database));
        }
    }
}
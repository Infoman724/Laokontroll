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
    public partial class AB : ContentPage
    {
        private WarehouseDatabase database;
        private Laos warehouse;
        private Entry objectNameEntry;

        public AB(WarehouseDatabase database, Laos warehouse)
        {
            this.database = database;
            this.warehouse = warehouse;

            objectNameEntry = new Entry
            {
                Placeholder = "Название объекта"
            };

            Button addButton = new Button
            {
                Text = "Добавить"
            };
            addButton.Clicked += OnAddClicked;

            Content = new StackLayout
            {
                Children = { objectNameEntry, addButton }
            };
        }

        private async void OnAddClicked(object sender, EventArgs e)
        {
            string objectName = objectNameEntry.Text;
            Models.Object obj = new Models.Object { Nimetus = objectName };

            App.Database.SaveObject(obj);
            await DisplayAlert("Успех", "Объект добавлен на склад", "ОК");

            await Navigation.PopAsync();
        }
    }

}
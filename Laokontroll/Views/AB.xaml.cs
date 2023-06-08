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
        private Entry objectAsukohtEntry;

        public AB(WarehouseDatabase database, Laos warehouse)
        {
            this.database = database;
            this.warehouse = warehouse;

            
            objectNameEntry = new Entry
            {
                Placeholder = "Objekti nimi:",
                TextColor = Color.White,
                PlaceholderColor = Color.White,
            };

            objectAsukohtEntry = new Entry
            {
                Placeholder = "Objekti asukoht",
                TextColor = Color.White,
                PlaceholderColor = Color.White,
            };
            BackgroundImageSource = "Fon.jpeg";

            ImageButton addButton = new ImageButton
            {
                Source = "Lisa.png",

                BackgroundColor = Color.Transparent,
                WidthRequest = 300, 
                HeightRequest = 50,                      
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center 
            };
            addButton.Clicked += OnAddClicked;

            Content = new StackLayout
            {
                Children = { objectNameEntry, objectAsukohtEntry, addButton }
            };
        }

        private async void OnAddClicked(object sender, EventArgs e)
        {
            string objectName = objectNameEntry.Text;
            string objectAsukoht = objectAsukohtEntry.Text;

            Models.Object obj = new Models.Object
            {
                Nimetus = objectName,
                Asukoht = objectAsukoht,
                LaosId = warehouse.LaosId 
            };

            App.Database.SaveObject(obj);
            await DisplayAlert("Õnnestus", "Objekt on lisatud", "OK");

            await Navigation.PopAsync();
        }


    }
}

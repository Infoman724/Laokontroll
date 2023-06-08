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
            BackgroundImageSource = "Fon.jpeg";
            warehouseNameEntry = new Entry
            {
                Placeholder = "Lao nimi",
                TextColor = Color.White,
                PlaceholderColor = Color.White,
            };

           
            ImageButton createButton = new ImageButton
            {
                Source = "LooLadu2.png",

                BackgroundColor = Color.Transparent,
                WidthRequest = 300, 
                HeightRequest = 50, 
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center 
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
            await DisplayAlert("Õnnestus", "Ladu on loodud", "OK");

            await Navigation.PopAsync();
        }
    }

}
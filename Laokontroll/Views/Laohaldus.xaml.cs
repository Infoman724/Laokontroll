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
        private ListView warehouseListView;
        private Laos selectedWarehouse;

        public Laohaldus(WarehouseDatabase database)
        {
            this.database = database;
            this.selectedWarehouse = selectedWarehouse;
            var warehouseListView = new ListView
            {
                ItemsSource = GetWarehouseList(),
                ItemTemplate = new DataTemplate(() =>
                {
                    var textCell = new TextCell();
                    textCell.SetBinding(TextCell.TextProperty, "Nimetus");
                    textCell.TextColor = Color.White; // Установите цвет текста на белый
                    return textCell;
                })
            };
            
            warehouseListView.ItemSelected += OnWarehouseSelected;
            BackgroundImageSource = "Fon.jpeg";

            ImageButton deleteButton = new ImageButton
            {
                Source = "kustuta.png",
                IsEnabled = false,
                BackgroundColor = Color.Transparent,
                WidthRequest = 300, 
                HeightRequest = 50, 
                HorizontalOptions = LayoutOptions.Center, 
                VerticalOptions = LayoutOptions.Center  
            };
            deleteButton.Clicked += OnDeleteButtonClicked;

            
            ImageButton viewButton = new ImageButton
            {
                Source = "Minne.png",
                IsEnabled = false,
                BackgroundColor = Color.Transparent,
                WidthRequest = 300, 
                HeightRequest = 50, 
                HorizontalOptions = LayoutOptions.Center, 
                VerticalOptions = LayoutOptions.Center 
            };
            viewButton.Clicked += OnViewButtonClicked;

            Content = new StackLayout
            {
                Children = { warehouseListView, deleteButton, viewButton }
            };
        }

        private void OnWarehouseSelected(object sender, SelectedItemChangedEventArgs e)
        {
            selectedWarehouse = (Laos)e.SelectedItem;
            ImageButton deleteButton = (ImageButton)((StackLayout)Content).Children[1];
            deleteButton.IsEnabled = true;

            ImageButton viewButton = (ImageButton)((StackLayout)Content).Children[2];
            viewButton.IsEnabled = true;
        }

        private async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            if (selectedWarehouse == null)
                return;

            bool delete = await DisplayAlert("Kinnita kustutamine", "Kas soovite kindlasti lao kustutada?", "Jah", "Ei");
            if (delete)
            {
                database.DeleteWarehouse(selectedWarehouse);
                await DisplayAlert("Õnnestus", "Ladu on kustutatud", "OK");

                warehouseListView.ItemsSource = GetWarehouseList();
                selectedWarehouse = null;

                Button deleteButton = (Button)sender;
                deleteButton.IsEnabled = false;

                Button viewButton = (Button)((StackLayout)Content).Children[2];
                viewButton.IsEnabled = false;
            }
        }

        private async void OnViewButtonClicked(object sender, EventArgs e)
        {
            if (selectedWarehouse == null)
                return;

            await Navigation.PushAsync(new Lao(database, selectedWarehouse));
        }


        private List<Laos> GetWarehouseList()
        {
            return database.GetWarehouses();
        }
    }
}

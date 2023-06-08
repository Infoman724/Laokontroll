using Laokontroll.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Laokontroll.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ObjectDetails : ContentPage
    {
        private Models.Object selectedObject;

        public ObjectDetails(Models.Object selectedObject)
        {
            this.selectedObject = selectedObject;
            BackgroundImageSource = "Fon.jpeg";
            Label nameLabel = new Label
            {
                Text = "Objekti nimi:",
                TextColor = Color.White,
                
            };
            Entry nameEntry = new Entry
            {
                Text = selectedObject.Nimetus,
                TextColor = Color.White,
                PlaceholderColor = Color.White,
            };
            nameEntry.TextChanged += (sender, e) => selectedObject.Nimetus = e.NewTextValue;

            Label locationLabel = new Label
            {
                Text = "Objekti asukoht:",
                TextColor = Color.White,
            };
            Entry locationEntry = new Entry
            {
                Text = selectedObject.Asukoht,
                TextColor = Color.White,
                PlaceholderColor = Color.White,
            };
            locationEntry.TextChanged += (sender, e) => selectedObject.Asukoht = e.NewTextValue;

            
            ImageButton saveButton = new ImageButton
            {
                Source = "Salvesta.png",

                BackgroundColor = Color.Transparent,
                WidthRequest = 300, 
                HeightRequest = 50, 
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center 
            };
            saveButton.Clicked += OnSaveClicked;

            
            ImageButton deleteButton = new ImageButton
            {
                Source = "Kustuta2.png",

                BackgroundColor = Color.Transparent,
                WidthRequest = 300, 
                HeightRequest = 50, 
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center 
            };
            deleteButton.Clicked += OnDeleteClicked;

            Content = new StackLayout
            {
                Children = { nameLabel, nameEntry, locationLabel, locationEntry, saveButton, deleteButton }
            };
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            App.Database.SaveObject(selectedObject);
            await DisplayAlert("Õnnestus", "Salvestusruum on salvestatud", "OK");
        }

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            bool delete = await DisplayAlert("Kinnita kustutamine", "Kas soovite kindlasti objekti kustutada?", "Jah", "Ei");
            if (delete)
            {
                App.Database.DeleteObject(selectedObject);
                await DisplayAlert("Õnnestus", "Objekt on kustutatud", "OK");
            }
        }
    }
}

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

            Image logoImage = new Image
            {
                Source = ImageSource.FromFile("korobkaLogo.png"),
                Aspect = Aspect.AspectFit,
                WidthRequest = 500, // установка ширины картинки
                HeightRequest = 300, // установка высоты картинки
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            BackgroundImageSource = "Fon.jpeg";
            ImageButton createWarehouseButton = new ImageButton
            {
                Source = "LooLadu2.png",

                BackgroundColor = Color.Transparent,
                WidthRequest = 300, 
                HeightRequest = 50, 
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center 
            };
            createWarehouseButton.Clicked += OnCreateWarehouseClicked;

            ImageButton manageWarehouseButton = new ImageButton
            {
                Source = "halda2.png",

                BackgroundColor = Color.Transparent,
                WidthRequest = 300, 
                HeightRequest = 50, 
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center 
            };
            manageWarehouseButton.Clicked += OnManageWarehouseClicked;

            Content = new StackLayout
            {
                Children = { logoImage, createWarehouseButton, manageWarehouseButton }
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
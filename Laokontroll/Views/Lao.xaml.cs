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
    public partial class Lao : ContentPage
    {
        private WarehouseDatabase database;
        private Laos warehouse;

        public Lao(WarehouseDatabase database, Laos warehouse)
        {
            this.database = database;
            this.warehouse = warehouse;

            BackgroundImageSource = "Fon.jpeg";
            ImageButton viewObjectsButton = new ImageButton
            {
                Source = "Vaadake.png",
                
                BackgroundColor = Color.Transparent,
                WidthRequest = 300, 
                HeightRequest = 70, 
                HorizontalOptions = LayoutOptions.Center, 
                VerticalOptions = LayoutOptions.Center 
            };
            viewObjectsButton.Clicked += OnViewObjectsClicked;

            
            ImageButton addObjectButton = new ImageButton
            {
                Source = "Lisa.png",
                
                BackgroundColor = Color.Transparent,
                WidthRequest = 300, 
                HeightRequest = 50, 
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center 
            };                                           
            addObjectButton.Clicked += OnAddObjectClicked;

            Content = new StackLayout
            {
                Children = { viewObjectsButton, addObjectButton }
            };

        }

        private async void OnViewObjectsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ObjectList(database, warehouse)); // Переход к списку объектов на складе
        }

        private async void OnAddObjectClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AB(database, warehouse)); // Переход к добавлению нового объекта
        }
    }

}
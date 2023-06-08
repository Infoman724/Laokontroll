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

            Button viewObjectsButton = new Button
            {
                Text = "Просмотреть все объекты на складе"
            };
            viewObjectsButton.Clicked += OnViewObjectsClicked;

            Button addObjectButton = new Button
            {
                Text = "Добавить объект на склад"
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
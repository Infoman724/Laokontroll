using Laokontroll.Models;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Laokontroll.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ObjectList : ContentPage
    {
        private WarehouseDatabase database;
        private Laos warehouse;
        private ListView objectListView;

        public ObjectList(WarehouseDatabase database, Laos warehouse)
        {
            this.database = database;
            this.warehouse = warehouse;
            BackgroundImageSource = "Fon.jpeg";
            objectListView = new ListView
            {
                ItemTemplate = new DataTemplate(() =>
                {
                    var textCell = new TextCell();
                    textCell.SetBinding(TextCell.TextProperty, "Nimetus");
                    textCell.TextColor = Color.White; // Установите цвет текста на белый
                    return textCell;
                })
            };
            objectListView.ItemSelected += OnObjectSelected;

            Content = objectListView;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            objectListView.ItemsSource = GetObjectList();
        }

        private List<Laokontroll.Models.Object> GetObjectList()
        {
            return App.Database.GetObjects().Where(obj => obj.LaosId == warehouse.LaosId).ToList();
        }

        private async void OnObjectSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Models.Object selectedObject = (Models.Object)e.SelectedItem;
            await Navigation.PushAsync(new ObjectDetails(selectedObject));
        }
    }
}

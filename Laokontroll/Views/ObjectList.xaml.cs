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
    public partial class ObjectList : ContentPage
    {
        private WarehouseDatabase database;
        private Laos warehouse;
        private ListView objectListView;

        public ObjectList(WarehouseDatabase database, Laos warehouse)
        {
            this.database = database;
            this.warehouse = warehouse;

            objectListView = new ListView
            {
                ItemTemplate = new DataTemplate(typeof(TextCell))
            };
            objectListView.ItemTemplate.SetBinding(TextCell.TextProperty, "Nimetus");
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

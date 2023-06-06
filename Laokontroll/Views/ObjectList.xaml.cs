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

        public ObjectList(WarehouseDatabase database, Laos warehouse)
        {
            this.database = database;
            this.warehouse = warehouse;

            ListView objectListView = new ListView
            {
                ItemsSource = GetObjectList(),
                ItemTemplate = new DataTemplate(typeof(TextCell))
            };
            objectListView.ItemTemplate.SetBinding(TextCell.TextProperty, "Nimetus");
            objectListView.ItemSelected += OnObjectSelected;

            Content = objectListView;
        }

        private List<Laokontroll.Models.Object> GetObjectList()
        {
            return App.Database.GetObjects().ToList();
        }



        private async void OnObjectSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Models.Object selectedObject = (Models.Object)e.SelectedItem;

            bool delete = await DisplayAlert("Подтвердить удаление", "Вы уверены, что хотите удалить объект?", "Да", "Нет");
            if (delete)
            {
                App.Database.DeleteObject(selectedObject);
                await DisplayAlert("Успех", "Объект удален", "ОК");

                ListView objectListView = (ListView)sender;
                objectListView.ItemsSource = GetObjectList();
            }
        }
    }
}
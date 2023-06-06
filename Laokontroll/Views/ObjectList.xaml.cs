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

            await Navigation.PushAsync(new ObjectDetails(selectedObject));
        }

        private async Task DisplayObjectDetails(Models.Object selectedObject, ListView objectListView)
        {
            string objectInfo = $"Имя: {selectedObject.Nimetus}\nМестоположение: {selectedObject.Asukoht}";
            bool edit = await DisplayAlert("Детали объекта", objectInfo, "Редактировать", "Отмена");

            if (edit)
            {
                await EditObject(selectedObject, objectListView);
            }
        }

        private async Task EditObject(Models.Object selectedObject, ListView objectListView)
        {
            string objectName = await DisplayPromptAsync("Редактирование объекта", "Введите новое имя", initialValue: selectedObject.Nimetus);
            string objectAsukoht = await DisplayPromptAsync("Редактирование объекта", "Введите новое местоположение", initialValue: selectedObject.Asukoht);

            selectedObject.Nimetus = objectName;
            selectedObject.Asukoht = objectAsukoht;

            App.Database.SaveObject(selectedObject);
            await DisplayAlert("Успех", "Данные объекта обновлены", "ОК");

            objectListView.ItemsSource = GetObjectList();
        }
    }
}

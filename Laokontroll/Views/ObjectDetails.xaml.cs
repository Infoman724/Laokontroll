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

            Label nameLabel = new Label
            {
                Text = "Имя объекта:"
            };
            Entry nameEntry = new Entry
            {
                Text = selectedObject.Nimetus
            };
            nameEntry.TextChanged += (sender, e) => selectedObject.Nimetus = e.NewTextValue;

            Label locationLabel = new Label
            {
                Text = "Местоположение:"
            };
            Entry locationEntry = new Entry
            {
                Text = selectedObject.Asukoht
            };
            locationEntry.TextChanged += (sender, e) => selectedObject.Asukoht = e.NewTextValue;

            Button saveButton = new Button
            {
                Text = "Сохранить"
            };
            saveButton.Clicked += OnSaveClicked;

            Button deleteButton = new Button
            {
                Text = "Удалить"
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
            await DisplayAlert("Успех", "Данные объекта сохранены", "ОК");
        }

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            bool delete = await DisplayAlert("Подтвердить удаление", "Вы уверены, что хотите удалить объект?", "Да", "Нет");
            if (delete)
            {
                App.Database.DeleteObject(selectedObject);
                await DisplayAlert("Успех", "Объект удален", "ОК");
            }
        }
    }
}

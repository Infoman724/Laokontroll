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
            warehouseListView = new ListView
            {
                ItemsSource = GetWarehouseList(),
                ItemTemplate = new DataTemplate(typeof(TextCell))
            };
            warehouseListView.ItemTemplate.SetBinding(TextCell.TextProperty, "Nimetus");
            warehouseListView.ItemSelected += OnWarehouseSelected;

            Button deleteButton = new Button
            {
                Text = "Удалить склад",
                IsEnabled = false
            };
            deleteButton.Clicked += OnDeleteButtonClicked;

            Button viewButton = new Button
            {
                Text = "Перейти на страницу склада",
                IsEnabled = false
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
            Button deleteButton = (Button)((StackLayout)Content).Children[1];
            deleteButton.IsEnabled = true;

            Button viewButton = (Button)((StackLayout)Content).Children[2];
            viewButton.IsEnabled = true;
        }

        private async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            if (selectedWarehouse == null)
                return;

            bool delete = await DisplayAlert("Подтвердить удаление", "Вы уверены, что хотите удалить склад?", "Да", "Нет");
            if (delete)
            {
                database.DeleteWarehouse(selectedWarehouse);
                await DisplayAlert("Успех", "Склад удален", "ОК");

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

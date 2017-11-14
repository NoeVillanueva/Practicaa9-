﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.WindowsAzure.MobileServices;
using System.Collections.ObjectModel;

namespace Practica9A
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DeletePage : ContentPage
    {
        public ObservableCollection<_13090417> Items { get; set; }
        public MobileServiceUser usuario;

        public DeletePage(MobileServiceUser user)
        {
            usuario = user;
            InitializeComponent();
        }


        async void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;
            await Navigation.PushAsync(new RecuperarPage(e.SelectedItem as _13090417));
        }


        private async void Button_Mostrar_Eliminados_Clicked(object sender, EventArgs e)
        {
            if (usuario != null)
            { 
            IEnumerable<_13090417> elementos = await DataPage.Tabla.IncludeDeleted().Where(_13090417 => _13090417.Deleted == true).ToEnumerableAsync();
            Items = new ObservableCollection<_13090417>(elementos);
            BindingContext = this;
            Lista.ItemsSource = Items;

            }
            else
            {
                await DisplayAlert("Error", "No hay datos de usuario", "✓");
            }
        }
    }
}
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
    public partial class DataPage : ContentPage
    {
        public ObservableCollection<_13090417> Items { get; set; }
        public static MobileServiceClient cliente;
        public static IMobileServiceTable<_13090417> Tabla;
        public static MobileServiceUser usuario;

        public DataPage()
        {
            InitializeComponent();

            cliente = new MobileServiceClient(AzureConnection.AzureURL);
            Tabla = cliente.GetTable<_13090417>();
            //LeerTabla();


        }

        private void Insertar_Clicked(object sender, EventArgs e)
        {

            Navigation.PushAsync(new InsertPage());

        }

        private void Button_Mostrar_Datos_Eliminados_Clicked(object sender, EventArgs e)
        {
            //MobileServiceUser user = sender as MobileServiceUser;
            Navigation.PushAsync(new DeletePage(usuario));
        }


        private async void LeerTabla()
        {
            IEnumerable<_13090417> elementos = await Tabla.ToEnumerableAsync();
            Items = new ObservableCollection<_13090417>(elementos);
            BindingContext = this;
            Lista.ItemsSource = Items;
        }

        async void Handle_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;
            await Navigation.PushAsync(new SelectPage(e.SelectedItem as _13090417));
        }



        private async void Login_Clicked(object sender, EventArgs e) 
        {
            usuario = await App.Authenticador.Authenticate();
            if (App.Authenticador != null)
            {
                if (usuario != null)
                {
                    await DisplayAlert("Usuario Autenticado", usuario.UserId, "✓");
                    LeerTabla();

                    if (usuario == null)
                    {
                        
                        
  
                    }

                
                }
            }

        }


        protected override void OnAppearing()
        {
            base.OnAppearing();

            if(usuario != null)
            {
                LeerTabla();
            }
        }

    }
}
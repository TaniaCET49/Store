using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Store.UIForms
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            lblFollowers.Text = (int.Parse(lblFollowers.Text) + 1).ToString();
        }

        private async void MenuItem_OnClicked(object sender, EventArgs e)
        {
            ToolbarItem tbi = (ToolbarItem)sender;

            bool result = await Application.Current.MainPage.DisplayAlert("Selecionado!",tbi.Text,"OK","CANCEL");

            if(tbi.Text.Equals("Editar") && result)
            {
                await Application.Current.MainPage.Navigation.PushAsync(new DemoPage());
            }
        }
    }
}

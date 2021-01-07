namespace Store.UIForms.ViewModels
{
    using Store.Common.Models;
    using Store.Common.Services;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using Xamarin.Forms;

    public class ProductsViewModel : INotifyPropertyChanged
    {

        #region Atributos

        private readonly ApiService apiService;

        private ObservableCollection<Product> _products;

        private bool _isRefreshing;

        #endregion



        #region Eventos

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion


        #region Propriedades

        public bool IsRefreshing
        {
            get => this._isRefreshing;

            set
            {
                if (this._isRefreshing != value)
                {
                    this._isRefreshing = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsRefreshing"));
                }
            }
        }

        public ObservableCollection<Product> Products
        {
            get => this._products;

            set
            {
                if (this._products != value)
                {
                    this._products = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Products"));
                }
            }
        }


        #endregion


        public ProductsViewModel()
        {
            this.apiService = new ApiService();
            this.LoadProducts();
        }



        private async void LoadProducts()
        {
            this.IsRefreshing = true;

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.GetListAsync<Product>(
                url,
                "/api",
                "/Products",
                "bearer",
                MainViewModel.GetInstance().Token.Token);

            this.IsRefreshing = false;

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");

                return;
            }

            var myProducts = (List<Product>)response.Result;
            this.Products = new ObservableCollection<Product>(myProducts);
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EcommerceProject
{
    public partial class HomePage : TabbedPage
    {
        private readonly ProductService _productService;

        public List<Product> ProductsList { get; set; }

        public HomePage()
        {
            InitializeComponent();
            _productService = new ProductService();
            LoadProducts();
            BindingContext = this;
        }

        // Fetch products from the backend
        private async Task LoadProducts()
        {
            try
            {
                ProductsList = await _productService.GetAllProducts();
                OnPropertyChanged(nameof(ProductsList));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to load products: {ex.Message}", "OK");
            }
        }

        // Logout functionality
        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            bool confirmLogout = await DisplayAlert("Logout", "Are you sure you want to logout?", "Yes", "No");
            if (confirmLogout)
            {
                Application.Current.MainPage = new LoginPage(); // Redirect to LoginPage
            }
        }

        // Navigate to Product Details Page
        private async void OnDetailsClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var product = button?.BindingContext as Product;

            if (product != null)
            {
                await Navigation.PushAsync(new ProductDetailsPage(product)); // Pass the selected product to the details page
            }
        }

        // Navigate to Cart Page
        private async void OnCartClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CartPage());
        }
    }
}

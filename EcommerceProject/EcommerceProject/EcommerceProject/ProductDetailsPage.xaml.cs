using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EcommerceProject
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductDetailsPage : ContentPage
    {

        public Product SelectedProduct { get; set; }

        public ProductDetailsPage(Product product)
        {
            InitializeComponent();
            SelectedProduct = product;
            BindingContext = SelectedProduct;
        }

        private async void OnAddToCartClicked(object sender, EventArgs e)
        {
            try
            {
                // Retrieve the cart from properties
                var cart = new List<CartItem>();
                if (Application.Current.Properties.ContainsKey("Cart"))
                {
                    var cartJson = Application.Current.Properties["Cart"] as string;
                    cart = JsonConvert.DeserializeObject<List<CartItem>>(cartJson) ?? new List<CartItem>();
                }

                // Check if the product already exists in the cart
                var existingItem = cart.FirstOrDefault(item => item.Id == SelectedProduct.Id);
                if (existingItem != null)
                {
                    existingItem.Quantity++;
                }
                else
                {
                    cart.Add(new CartItem
                    {
                        Id = SelectedProduct.Id,
                        Title = SelectedProduct.Title,
                        Price = (decimal)SelectedProduct.Price,
                        Image = SelectedProduct.Image,
                        Quantity = 1
                    });
                }

                // Save the updated cart back to properties
                Application.Current.Properties["Cart"] = JsonConvert.SerializeObject(cart);
                await Application.Current.SavePropertiesAsync();

                // Debug: Verify if the cart is updated
                var savedCartJson = Application.Current.Properties["Cart"] as string;
                System.Diagnostics.Debug.WriteLine($"Updated Cart: {savedCartJson}");

                await DisplayAlert("Success", $"{SelectedProduct.Title} has been added to your cart!", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }

    }
}
using EcommerceProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace EcommerceProject
{
    public partial class CartPage : ContentPage
    {
        public List
<CartItem> CartItems
        { get; set; }
        public decimal TotalPrice => CartItems.Sum(item => item.Price * item.Quantity);

        public CartPage()
        {
            InitializeComponent();
            LoadCart();
        }

        private void LoadCart()
        {
            CartItems = CartServicee.GetCartItems(); // Fetch cart items
            BindingContext = this;
            // Fetch cart items
            OnPropertyChanged(nameof(CartItems));
            OnPropertyChanged(nameof(TotalPrice));
        }

        // Remove item from cart
        private void OnRemoveClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var cartItem = button?.CommandParameter as CartItem;

            if (cartItem != null)
            {
                CartServicee.RemoveFromCart(cartItem);
                LoadCart();
                OnPropertyChanged(nameof(CartItems));
                OnPropertyChanged(nameof(TotalPrice));
            }
        }

        private void OnDecrementQuantityClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var cartItem = button?.CommandParameter as CartItem;

            if (cartItem != null && cartItem.Quantity > 1) // Ensure quantity is greater than 1
            {
                cartItem.Quantity--;
                CartServicee.UpdateCartItem(cartItem, cartItem.Quantity); // Pass the updated quantity
                LoadCart();
            }
        }


        // Increment quantity
        private void OnIncrementQuantityClicked(object sender, EventArgs e)
        {

            var button = sender as Button;
            var cartItem = button?.CommandParameter as CartItem;

            cartItem.Quantity++;
            CartServicee.UpdateCartItem(cartItem, cartItem.Quantity); // Pass the updated quantity
            LoadCart();

        }

        // Handle checkout
        private async void OnCheckoutClicked(object sender, EventArgs e)
        {
            if (!CartItems.Any())
            {
                await DisplayAlert("Cart Empty", "Your cart is empty. Add products to proceed!", "OK");
                return;
            }

            // Retrieve user ID from local storage
            if (!Application.Current.Properties.TryGetValue("userId", out var userIdObj) || !(userIdObj is string userId))
            {
                await DisplayAlert("Error", "User is not logged in. Please log in to proceed.", "OK");
                return;
            }

       


            var command = new Command
            {
                id = null,
                userId = userId, // save the userId in the localstorage
                
               
                products = CartItems.Select(item => new Command.Product
                {
                    id = item.Id,
                    quantity = item.Quantity,
                    productName = item.Title
                }).ToList(),
                amount = (double)TotalPrice,
                status = "pending",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            LoadCart();

            var commandService = new CommandService();
            bool success = await commandService.CreateCommand(command);
            LoadCart();
            if (success)
            {
                LoadCart();
                await DisplayAlert("Success", "Checkout completed successfully!", "OK");
                CartServicee.ClearCart();
                LoadCart();
            }
            else
            {
                await DisplayAlert("Error", "Checkout failed. Please try again.", "OK");
            }
        }


    }
}







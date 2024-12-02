using Newtonsoft.Json;
using System.Collections.Generic;
using Xamarin.Forms;

namespace EcommerceProject.Services
{
    public static class CartServicee
    {
        private const string CartKey = "Cart";

        // Retrieve cart items
        public static List<CartItem> GetCartItems()
        {
            if (Application.Current.Properties.ContainsKey(CartKey))
            {
                var cartJson = Application.Current.Properties[CartKey] as string;
                return JsonConvert.DeserializeObject<List<CartItem>>(cartJson) ?? new List<CartItem>();
            }

            return new List<CartItem>();
        }

        // Add item to cart
        public static void AddToCart(CartItem item)
        {
            var cart = GetCartItems();
            var existingItem = cart.Find(i => i.Id == item.Id);

            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                cart.Add(item);
            }

            SaveCart(cart);
        }

        // Remove item from cart
        public static void RemoveFromCart(CartItem item)
        {
            var cart = GetCartItems();
            cart.RemoveAll(i => i.Id == item.Id);
            SaveCart(cart);
        }

        // Update item quantity in cart
        public static void UpdateCartItem(CartItem item, int quantity)
        {
            var cart = GetCartItems();
            var existingItem = cart.Find(i => i.Id == item.Id);

            if (existingItem != null)
            {
                existingItem.Quantity = quantity; // Update quantity
            }

            SaveCart(cart);
        }

        // Clear the cart
        public static void ClearCart()
        {
            SaveCart(new List<CartItem>());
        }

        // Save the cart
        private static void SaveCart(List<CartItem> cart)
        {
            Application.Current.Properties[CartKey] = JsonConvert.SerializeObject(cart);
            Application.Current.SavePropertiesAsync();
        }
    }
}

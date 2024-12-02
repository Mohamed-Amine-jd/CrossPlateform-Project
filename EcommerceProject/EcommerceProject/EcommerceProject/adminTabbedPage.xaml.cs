using System;
using Xamarin.Forms;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using EcommerceProject.Services;

namespace EcommerceProject
{
    public partial class adminTabbedPage : TabbedPage
    {
        private ProductService productService;
        private CommandService commandService;
        private readonly HttpClient _httpClient;
   

        public adminTabbedPage()
        {
            InitializeComponent();
            productService = new ProductService();
            commandService = new CommandService();
            LoadProducts();
            _httpClient = new HttpClient { BaseAddress = new Uri("http://172.20.10.4:8080/api/user/") }; // Set the correct backend URL
             LoadUsers();
          

        }

     

        // Logout functionality
        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            bool confirmLogout = await DisplayAlert("Logout", "Are you sure you want to logout?", "Yes", "No");
            if (confirmLogout)
            {
                Application.Current.MainPage = new LoginPage(); // Navigate to login page
            }
        }

        // Add product functionality
        private async void AddProductButton_Clicked(object sender, EventArgs e)
        {
            // Collect data from the form fields
            string title = TitleEntry.Text;
            string description = DescriptionEntry.Text;
            string image = ImageEntry.Text;
            double price = double.TryParse(PriceEntry.Text, out double parsedPrice) ? parsedPrice : 0;
            int qty = int.TryParse(QtyEntry.Text, out int parsedQty) ? parsedQty : 0;
            string category = CategoryPicker.SelectedItem?.ToString();

            // Validate the input fields
            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(description) || string.IsNullOrWhiteSpace(image) || price <= 0 || qty <= 0 || string.IsNullOrWhiteSpace(category))
            {
                await DisplayAlert("Validation Error", "All fields are required and valid.", "OK");
                return;
            }

            try
            {
                // Call the API to add the product
                bool result = await productService.AddProduct(title, description, image, price, qty, category);

                if (result)
                {
                    await DisplayAlert("Success", "Product added successfully!", "OK");
                    await LoadProducts();
                }
                else
                {
                    await DisplayAlert("Error", "Failed to add product. Please try again.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        // Load products functionality - calling this on page load to get product list
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Get the list of products from the API
            await LoadProducts();
            await LoadCommands();
        }

        // Fetch all products
        private async Task LoadProducts()
        {
            try
            {
                var products = await productService.GetAllProducts();
                // Display the list of products in a CollectionView or ListView
                ProductCollectionView.ItemsSource = products;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }



        private async void DeleteProductButton_Clicked(object sender, EventArgs e)
        {
            // Get the product ID from the sender's binding context (the product object)
            var product = (sender as Button)?.BindingContext as Product;

            if (product != null)
            {
                try
                {
                    // Call the DeleteProduct method to delete the product
                    bool result = await productService.DeleteProduct(product.Id);

                    if (result)
                    {
                        await DisplayAlert("Success", "Product deleted successfully!", "OK");

                        // Refresh the product list
                        LoadProducts();
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", ex.Message, "OK");
                }
            }
        }

        private async void UpdateProductButton_Clicked(object sender, EventArgs e)
        {
            var product = (sender as Button)?.BindingContext as Product;

            if (product != null)
            {
                // Open an alert with pre-filled data to edit
                string newTitle = await DisplayPromptAsync("Update Product", "Enter new title:", initialValue: product.Title);
                string newDescription = await DisplayPromptAsync("Update Product", "Enter new description:", initialValue: product.Description);
                string newImage = await DisplayPromptAsync("Update Product", "Enter new image URL:", initialValue: product.Image);
                string newPrice = await DisplayPromptAsync("Update Product", "Enter new price:", initialValue: product.Price.ToString());
                string newQty = await DisplayPromptAsync("Update Product", "Enter new quantity:", initialValue: product.Qty.ToString());

                if (string.IsNullOrWhiteSpace(newTitle) || string.IsNullOrWhiteSpace(newDescription) || string.IsNullOrWhiteSpace(newImage) ||
                    !double.TryParse(newPrice, out double price) || !int.TryParse(newQty, out int qty))
                {
                    await DisplayAlert("Validation Error", "Please enter valid data.", "OK");
                    return;
                }

                try
                {
                    // Send the updated product details to the API
                    bool result = await productService.UpdateProduct(product.Id, newTitle, newDescription, newImage, price, qty, product.Category);

                    if (result)
                    {
                        await DisplayAlert("Success", "Product updated successfully!", "OK");
                        LoadProducts();  // Refresh the list of products
                    }
                    else
                    {
                        await DisplayAlert("Error", "Failed to update product. Please try again.", "OK");
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", ex.Message, "OK");
                }
            }
        }




        private async Task LoadCommands()
        {
            try
            {
                var commands = await commandService.GetAllCommands();
                CommandsCollectionView.ItemsSource = commands; // Ensure commands is a List<Command>
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to load commands: {ex.Message}", "OK");
            }
        }





        // Fetch users from the backend
        private async Task LoadUsers()
        {
            try
            {
                var response = await _httpClient.GetAsync("all");

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    var users = JsonConvert.DeserializeObject<List<User>>(responseData);
                    UsersCollectionView.ItemsSource = users;  // Bind users to the CollectionView
                }
                else
                {
                    await DisplayAlert("Error", "Failed to load users.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Error fetching users: " + ex.Message, "OK");
            }
        }


     





        // Handle delete button click for users
        private async void DeleteUserButton_Clicked(object sender, EventArgs e)
        {
            var user = (sender as Button)?.BindingContext as User;  // Get the selected user

            if (user != null)
            {
                bool confirmDelete = await DisplayAlert("Delete User", $"Are you sure you want to delete user {user.Name}?", "Yes", "No");
                if (confirmDelete)
                {
                    try
                    {
                        var response = await _httpClient.DeleteAsync($"delete/{user.Id}");

                        if (response.IsSuccessStatusCode)
                        {
                            await DisplayAlert("Success", "User deleted successfully.", "OK");
                            LoadUsers();  // Reload users list after deletion
                        }
                        else
                        {
                            await DisplayAlert("Error", "Failed to delete user.", "OK");
                        }
                    }
                    catch (Exception ex)
                    {
                        await DisplayAlert("Error", "Error deleting user: " + ex.Message, "OK");
                    }
                }
            }
        }
    }

    // User class (this will match the JSON response from your backend)
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
       
    }


}
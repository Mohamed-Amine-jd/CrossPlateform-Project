using System;
using Xamarin.Forms;
using EcommerceProject;

namespace EcommerceProject
{
    public partial class RegisterPage : ContentPage
    {
        private readonly ApiService _apiService;

        public RegisterPage()
        {
            InitializeComponent();
            _apiService = new ApiService(); // Initialize the ApiService to make requests
        }

        private async void RegisterButton_Clicked(object sender, EventArgs e)
        {
            string name = NameEntry.Text;
            string email = EmailEntry.Text;
            string password = PasswordEntry.Text;
            string phone = PhoneEntry.Text;

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                await DisplayAlert("Validation Error", "All fields are required.", "OK");
                return;
            }

            try
            {
                bool result = await _apiService.RegisterUser(name, email, password, phone);

                if (result)
                {
                    await DisplayAlert("Success", "Registration successful!", "OK");
                    await Navigation.PushAsync(new LoginPage());
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Registration Failed", ex.Message, "OK");
            }
        }

        // Method to handle Login navigation when the label is tapped
        private async void OnLoginTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage()); // Navigate to LoginPage
        }

    }
}

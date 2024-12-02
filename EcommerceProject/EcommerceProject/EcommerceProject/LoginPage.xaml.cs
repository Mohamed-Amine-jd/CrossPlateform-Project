using Xamarin.Forms;
using System;

namespace EcommerceProject
{
    public partial class LoginPage : ContentPage
    {
        private readonly ApiService _apiService;

        public LoginPage()
        {
            InitializeComponent();
            _apiService = new ApiService();
        }

        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            string email = EmailEntry.Text;
            string password = PasswordEntry.Text;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                await DisplayAlert("Validation Error", "Both fields are required.", "OK");
                return;
            }

            try
            {
                var (success, isAdmin, userId) = await _apiService.LoginUser(email, password);

                if (success)
                {
                    // Save user ID to local storage
                    Application.Current.Properties["userId"] = userId;
                 

                    await Application.Current.SavePropertiesAsync();

                    if (isAdmin)
                    {
                        await DisplayAlert("Success", "Welcome, Admin!", "OK");
                        await Navigation.PushAsync(new adminTabbedPage());
                    }
                    else
                    {
                        await DisplayAlert("Success", "Login successful!", "OK");
                        await Navigation.PushAsync(new HomePage());
                    }
                }
                else
                {
                    await DisplayAlert("Login Failed", "Invalid email or password.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Login Failed", ex.Message, "OK");
            }
        }

        private async void OnRegisterTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }
    }
}

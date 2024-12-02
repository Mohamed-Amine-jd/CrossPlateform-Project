using Xamarin.Forms;

namespace EcommerceProject
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
            // Logic d'initialisation de l'application
        }

        protected override void OnSleep()
        {
            // Logic lorsque l'application entre en arrière-plan
        }

        protected override void OnResume()
        {
            // Logic lorsque l'application reprend
        }
    }
}

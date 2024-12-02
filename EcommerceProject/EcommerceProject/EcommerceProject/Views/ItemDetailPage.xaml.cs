using EcommerceProject.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace EcommerceProject.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}
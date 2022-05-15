using PartsClient.Data;
using PartsClient.ViewModels;

namespace PartsClient.Pages;

public partial class PartsPage : ContentPage
{
    public PartsPage()
    {
        InitializeComponent();
        BindingContext = new PartsViewModel();
    }
}
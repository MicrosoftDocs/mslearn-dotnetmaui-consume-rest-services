using PartsClient.Pages;

namespace PartsClient;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute("addpart", typeof(AddPartPage));
    }
}

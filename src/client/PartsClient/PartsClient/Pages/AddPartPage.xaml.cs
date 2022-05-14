using PartsClient.Data;

namespace PartsClient.Pages;

public partial class AddPartPage : ContentPage
{
	public AddPartPage()
	{
		InitializeComponent();
	}

    private async void OnAddPart(object sender, EventArgs e)
    {
        if (IsBusy)
            return;

        IsBusy = true;

        try
        {
            Part part = new Part
            {
                PartName = PartNameField.Text,
                PartType = PartTypeField.Text,
                Suppliers = new List<String> { PartSupplierField.Text }
            };

            var insertedPart = await PartsPage.partsViewModel.AddPart(part);

            await DisplayAlert("Saved",
                    "Changes saved",
                    "OK");

            BindingContext = insertedPart;
        }
        catch (Exception ex)
        {
            await this.DisplayAlert("Error",
                    ex.Message,
                    "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }

    private void OnClearPart(object sender, EventArgs e)
    {
        BindingContext = null;
        PartIDField.Text = "";
        PartNameField.Text = "";
        PartTypeField.Text = "";
        PartSupplierField.Text = "";
    }
}
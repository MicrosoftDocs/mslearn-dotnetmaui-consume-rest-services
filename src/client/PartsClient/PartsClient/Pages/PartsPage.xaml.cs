using PartsClient.Data;
using PartsClient.ViewModels;

namespace PartsClient.Pages;

public partial class PartsPage : ContentPage
{
    internal static PartsViewModel partsViewModel = new PartsViewModel();
    readonly PartsManager manager = new PartsManager();

    public PartsPage()
    {
        InitializeComponent();
        //Refresh();
        //BindingContext = partsViewModel;
        //PartsView.ScrollTo(0);
    }

    private async Task Refresh()
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;
            await partsViewModel.Populate();
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

    private async void OnSaveChanges(object sender, EventArgs e)
    {
        var item = (SwipeItem)sender;
        var part = item.CommandParameter as Part;

        if (part is null || IsBusy)
            return;

        IsBusy = true;

        try
        {
            await partsViewModel.UpdatePart(part);
            await this.DisplayAlert("Saved",
                    "Changes saved",
                    "OK");
        }
        catch (Exception ex)
        {
            await this.DisplayAlert("Error",
                    ex.Message,
                    "OK");
        }
        finally
        {
            PartsView.ScrollTo(0);
            IsBusy = false;
        }
    }

    private async void OnDeletePart(object sender, EventArgs e)
    {
        var item = (SwipeItem)sender;
        var part = item.CommandParameter as Part;

        if (part is null || IsBusy)
            return;

        if (await this.DisplayAlert("Delete Part?",
            $"Are you sure you want to delete the part '{part.PartName}'?",
            "Yes",
            "Cancel") == true)
        {
            try
            {
                IsBusy = true;
                await partsViewModel.DeletePart(part);
                PartsView.ScrollTo(0);
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
    }
}
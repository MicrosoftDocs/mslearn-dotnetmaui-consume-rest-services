using PartsClient.Data;
using PartsClient.ViewModels;

namespace PartsClient.Pages;

[QueryProperty("PartToDisplay", "part")]
public partial class AddPartPage : ContentPage
{
	AddPartViewModel viewModel;
	public AddPartPage()
	{
		InitializeComponent();

		viewModel = new AddPartViewModel();
		BindingContext = viewModel;
    }

	Part _partToDisplay;
    public Part PartToDisplay {
		get => _partToDisplay;
		set
        {
			if (_partToDisplay == value)
				return;

			_partToDisplay = value;

			viewModel.PartID = _partToDisplay.PartID;
			viewModel.PartName = _partToDisplay.PartName;
			viewModel.Suppliers = _partToDisplay.SupplierString;
			viewModel.PartType = _partToDisplay.PartType;
        }
	}
}

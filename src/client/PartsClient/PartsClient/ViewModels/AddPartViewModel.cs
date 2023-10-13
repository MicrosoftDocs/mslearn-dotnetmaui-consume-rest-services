using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using PartsClient.Data;

namespace PartsClient.ViewModels;

public partial class AddPartViewModel : ObservableObject
{ 
    [ObservableProperty]
    string _partID;

    [ObservableProperty]
    string _partName;

    [ObservableProperty]
    string _suppliers;

    [ObservableProperty]
    string _partType;
    
    public AddPartViewModel()
    {            
    }

    [RelayCommand]
    async Task SaveData()
    {
        if (string.IsNullOrWhiteSpace(PartID))
            await InsertPart();
        else
            await UpdatePart();
    }


    [RelayCommand]
    async Task InsertPart()
    {
        await PartsManager.Add(PartName, Suppliers, PartType);

        WeakReferenceMessenger.Default.Send(new RefreshMessage(true));

        await Shell.Current.GoToAsync("..");
    }


    [RelayCommand]
    async Task UpdatePart()
    {
        Part partToSave = new()
        {
            PartID = PartID,
            PartName = PartName,
            PartType = PartType,
            Suppliers = Suppliers.Split(",").ToList()
        };

        await PartsManager.Update(partToSave);

        WeakReferenceMessenger.Default.Send(new RefreshMessage(true));

        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    async Task DeletePart()
    {
        if (string.IsNullOrWhiteSpace(PartID))
            return;

        await PartsManager.Delete(PartID);

        WeakReferenceMessenger.Default.Send(new RefreshMessage(true));

        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    async Task DoneEditing()
    {
        await Shell.Current.GoToAsync("..");
    }
}

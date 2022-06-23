using PartsClient.Data;
using PartsClient.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PartsClient.ViewModels
{
    public class AddPartViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand DoneEditingCommand { get; private set; }

        public ICommand SaveCommand { get; private set; }

        public ICommand DeleteCommand { get; private set; }

        string _partId;
        public string PartID
        {
            get => _partId;
            set
            {
                if (_partId == value)
                    return;

                _partId = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PartID)));
            }
        }

        string _partName;
        public string PartName
        {
            get => _partName;
            set
            {
                if (_partName == value)
                    return;

                _partName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PartName)));
            }
        }

        string _suppliers;
        public string Suppliers
        {
            get => _suppliers;
            set
            {
                if (_suppliers == value)
                    return;

                _suppliers = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Suppliers)));
            }
        }

        string _partType;
        public string PartType
        {
            get => _partType;
            set
            {
                if (_partType == value)
                    return;

                _partType = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PartType)));
            }
        }

        //private PartsManager partsManager = new PartsManager();
        
        public AddPartViewModel()
        {            
            DoneEditingCommand = new Command(async () => await DoneEditing());
            SaveCommand = new Command(async () => await SaveData());
            DeleteCommand = new Command(async () => await DeletePart());
        }        

        private async Task SaveData()
        {
            if (string.IsNullOrWhiteSpace(PartID))
                await InsertPart();
            else
                await UpdatePart();
        }

        private async Task InsertPart()
        {
            await PartsManager.Add(PartName, Suppliers, PartType);

            MessagingCenter.Send(this, "refresh");

            await Shell.Current.GoToAsync("..");
        }

        private async Task UpdatePart()
        {
            Part partToSave = new()
            {
                PartID = PartID,
                PartName = PartName,
                PartType = PartType,
                Suppliers = Suppliers.Split(",").ToList()
            };

            await PartsManager.Update(partToSave);

            MessagingCenter.Send(this, "refresh");

            await Shell.Current.GoToAsync("..");
        }

        private async Task DeletePart()
        {
            if (string.IsNullOrWhiteSpace(PartID))
                return;

            await PartsManager.Delete(PartID);

            MessagingCenter.Send(this, "refresh");

            await Shell.Current.GoToAsync("..");
        }

        private async Task DoneEditing()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}

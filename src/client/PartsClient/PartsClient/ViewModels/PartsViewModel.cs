using Microsoft.Maui.Layouts;
using PartsClient.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PartsClient.ViewModels
{
    public class PartsViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Part> _parts;

        private bool _isRefreshing = false;
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set
            {
                if (_isRefreshing == value)
                    return;

                _isRefreshing = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsRefreshing)));
            }
        }

        private bool _isBusy = false;
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                if (_isBusy == value)
                    return;

                _isBusy = value;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsBusy)));
            }
        }

        private Part _selectedPart;
        public Part SelectedPart
        {
            get => _selectedPart;
            set
            {
                if (_selectedPart == value)
                    return;

                _selectedPart = value;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedPart)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public PartsViewModel()
        {            
            _parts = new ObservableCollection<Part>();
            LoadDataCommand = new Command(async () => await LoadData());
            PartSelectedCommand = new Command(async () => await PartSelected());
            AddNewPartCommand = new Command(async () => await Shell.Current.GoToAsync("addpart"));

            MessagingCenter.Subscribe<AddPartViewModel>(this, "refresh", async (sender) => await LoadData());

            Task.Run(LoadData);
        }

        private async Task PartSelected()
        {
            if (SelectedPart == null)
                return;

            var navigationParameter = new Dictionary<string, object>()
            {
                { "part", SelectedPart }
            };

            await Shell.Current.GoToAsync("addpart", navigationParameter);

            MainThread.BeginInvokeOnMainThread(() => SelectedPart = null);            
        }

        public ObservableCollection<Part> Parts
        {
            get => _parts;
            set => _parts = value;
        }

        public ICommand LoadDataCommand { get; private set; }

        public ICommand PartSelectedCommand { get; private set; }

        public ICommand AddNewPartCommand { get; private set; }

        public async Task LoadData()
        {
            if (IsBusy)
                return;

            try
            {
                IsRefreshing = true;
                IsBusy = true;

                var partsCollection = await PartsManager.GetAll();

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    Parts.Clear();
                    
                    foreach (Part part in partsCollection)
                    {                        
                        Parts.Add(part);                        
                    }
                });
            }
            finally
            {    
                IsRefreshing = false;
                IsBusy = false;
            }
        }

    }
}

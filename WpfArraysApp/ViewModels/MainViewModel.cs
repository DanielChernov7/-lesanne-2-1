using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using WpfArraysApp.Infrastructure;
using WpfArraysApp.Models;

namespace WpfArraysApp.ViewModels;

public class MainViewModel : NotifyPropertyChangedBase
{
    private readonly ICarEditDialogService _dialogService;
    private readonly ICollectionView _carsView;
    private Car? _selectedCar;
    private string _searchText = string.Empty;
    private SearchFieldOption? _selectedSearchField;
    private StatusFilterOption? _selectedStatusFilter;
    private int _nextId = 1;

    public MainViewModel(ICarEditDialogService dialogService)
    {
        _dialogService = dialogService;

        Cars = new ObservableCollection<Car>(CreateSeedCars());
        _nextId = Cars.Count + 1;

        SearchFieldOptions = new ObservableCollection<SearchFieldOption>
        {
            new(Constants.SearchFieldBrand, SearchField.Brand),
            new(Constants.SearchFieldModel, SearchField.Model),
            new(Constants.SearchFieldYear, SearchField.Year)
        };
        SelectedSearchField = SearchFieldOptions[0];

        StatusFilterOptions = new ObservableCollection<StatusFilterOption>
        {
            new(Constants.AllFilterOption, null),
            new(Status.Available.ToString(), Status.Available),
            new(Status.Reserved.ToString(), Status.Reserved),
            new(Status.Sold.ToString(), Status.Sold)
        };
        SelectedStatusFilter = StatusFilterOptions[0];

        _carsView = CollectionViewSource.GetDefaultView(Cars);
        _carsView.Filter = FilterCar;

        AddCommand = new RelayCommand(_ => AddCar());
        EditCommand = new RelayCommand(_ => EditCar(), _ => SelectedCar != null);
        DeleteCommand = new RelayCommand(_ => DeleteCar(), _ => SelectedCar != null);
        ClearFiltersCommand = new RelayCommand(_ => ClearFilters());
    }

    public ObservableCollection<Car> Cars { get; }

    public ICollectionView CarsView => _carsView;

    public ObservableCollection<SearchFieldOption> SearchFieldOptions { get; }

    public ObservableCollection<StatusFilterOption> StatusFilterOptions { get; }

    public Car? SelectedCar
    {
        get => _selectedCar;
        set
        {
            if (_selectedCar == value) return;
            _selectedCar = value;
            OnPropertyChanged();
            EditCommand.RaiseCanExecuteChanged();
            DeleteCommand.RaiseCanExecuteChanged();
        }
    }

    public string SearchText
    {
        get => _searchText;
        set
        {
            if (_searchText == value) return;
            _searchText = value;
            OnPropertyChanged();
            RefreshFilter();
        }
    }

    public SearchFieldOption? SelectedSearchField
    {
        get => _selectedSearchField;
        set
        {
            if (_selectedSearchField == value) return;
            _selectedSearchField = value;
            OnPropertyChanged();
            RefreshFilter();
        }
    }

    public StatusFilterOption? SelectedStatusFilter
    {
        get => _selectedStatusFilter;
        set
        {
            if (_selectedStatusFilter == value) return;
            _selectedStatusFilter = value;
            OnPropertyChanged();
            RefreshFilter();
        }
    }

    public RelayCommand AddCommand { get; }
    public RelayCommand EditCommand { get; }
    public RelayCommand DeleteCommand { get; }
    public RelayCommand ClearFiltersCommand { get; }

    private bool FilterCar(object item)
    {
        if (item is not Car car)
        {
            return false;
        }

        bool matchesSearch = true;
        if (!string.IsNullOrWhiteSpace(SearchText))
        {
            var field = SelectedSearchField?.Field ?? SearchField.Brand;
            switch (field)
            {
                case SearchField.Brand:
                    matchesSearch = car.Brand.Contains(SearchText, StringComparison.OrdinalIgnoreCase);
                    break;
                case SearchField.Model:
                    matchesSearch = car.Model.Contains(SearchText, StringComparison.OrdinalIgnoreCase);
                    break;
                case SearchField.Year:
                    var yearFilter = int.Parse(SearchText);
                    matchesSearch = car.Year == yearFilter;
                    break;
            }
        }

        bool matchesStatus = true;
        if (SelectedStatusFilter?.Value != null)
        {
            matchesStatus = car.Status == SelectedStatusFilter.Value.Value;
        }

        return matchesSearch && matchesStatus;
    }

    private void RefreshFilter()
    {
        _carsView.Refresh();
    }

    private void AddCar()
    {
        var newCar = _dialogService.ShowAddDialog(_nextId);
        if (newCar == null)
        {
            return;
        }

        Cars.Add(newCar);
        _nextId++;
    }

    private void EditCar()
    {
        if (SelectedCar == null)
        {
            return;
        }

        _dialogService.ShowEditDialog(SelectedCar);
    }

    private void DeleteCar()
    {
        if (SelectedCar == null)
        {
            return;
        }

        var result = MessageBox.Show(Constants.ConfirmDeleteMessage, Constants.ConfirmDeleteTitle,
            MessageBoxButton.YesNo, MessageBoxImage.Warning);
        if (result != MessageBoxResult.Yes)
        {
            return;
        }

        Cars.Remove(SelectedCar);
        SelectedCar = null;
    }

    private void ClearFilters()
    {
        SearchText = string.Empty;
        SelectedSearchField = SearchFieldOptions[0];
        SelectedStatusFilter = StatusFilterOptions[0];
    }

    private static IEnumerable<Car> CreateSeedCars()
    {
        return new List<Car>
        {
            new() { Id = 1, Brand = "Toyota", Model = "Corolla", Year = 2019, Price = 16500, FuelType = FuelType.Gasoline, Status = Status.Available },
            new() { Id = 2, Brand = "Volkswagen", Model = "Golf", Year = 2018, Price = 14900, FuelType = FuelType.Diesel, Status = Status.Reserved },
            new() { Id = 3, Brand = "Tesla", Model = "Model 3", Year = 2022, Price = 38900, FuelType = FuelType.Electric, Status = Status.Available },
            new() { Id = 4, Brand = "Honda", Model = "Civic", Year = 2020, Price = 17900, FuelType = FuelType.Gasoline, Status = Status.Sold },
            new() { Id = 5, Brand = "Ford", Model = "Focus", Year = 2017, Price = 12100, FuelType = FuelType.Hybrid, Status = Status.Available },
            new() { Id = 6, Brand = "Nissan", Model = "Micra", Year = 2016, Price = 9500, FuelType = FuelType.Gasoline, Status = Status.Available }
        };
    }
}

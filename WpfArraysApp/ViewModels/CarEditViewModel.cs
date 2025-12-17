using System.Collections.ObjectModel;
using System.ComponentModel;
using WpfArraysApp.Infrastructure;
using WpfArraysApp.Models;

namespace WpfArraysApp.ViewModels;

public class CarEditViewModel : NotifyPropertyChangedBase, IDataErrorInfo
{
    private int _id;
    private string _brand = string.Empty;
    private string _model = string.Empty;
    private int _year;
    private decimal _price;
    private FuelType _fuelType;
    private Status _status;

    public CarEditViewModel()
    {
        FuelTypeOptions = new ObservableCollection<FuelType>(Enum.GetValues<FuelType>());
        StatusOptions = new ObservableCollection<Status>(Enum.GetValues<Status>());

        SaveCommand = new RelayCommand(_ => RequestClose?.Invoke(this, true), _ => IsValid);
        CancelCommand = new RelayCommand(_ => RequestClose?.Invoke(this, false));
    }

    public CarEditViewModel(Car car) : this()
    {
        Id = car.Id;
        Brand = car.Brand;
        Model = car.Model;
        Year = car.Year;
        Price = car.Price;
        FuelType = car.FuelType;
        Status = car.Status;
    }

    public event EventHandler<bool?>? RequestClose;

    public ObservableCollection<FuelType> FuelTypeOptions { get; }
    public ObservableCollection<Status> StatusOptions { get; }

    public RelayCommand SaveCommand { get; }
    public RelayCommand CancelCommand { get; }

    public int Id
    {
        get => _id;
        set
        {
            if (_id == value) return;
            _id = value;
            OnPropertyChanged();
        }
    }

    public string Brand
    {
        get => _brand;
        set
        {
            if (_brand == value) return;
            _brand = value;
            OnPropertyChanged();
            SaveCommand.RaiseCanExecuteChanged();
        }
    }

    public string Model
    {
        get => _model;
        set
        {
            if (_model == value) return;
            _model = value;
            OnPropertyChanged();
            SaveCommand.RaiseCanExecuteChanged();
        }
    }

    public int Year
    {
        get => _year;
        set
        {
            if (_year == value) return;
            _year = value;
            OnPropertyChanged();
            SaveCommand.RaiseCanExecuteChanged();
        }
    }

    public decimal Price
    {
        get => _price;
        set
        {
            if (_price == value) return;
            _price = value;
            OnPropertyChanged();
            SaveCommand.RaiseCanExecuteChanged();
        }
    }

    public FuelType FuelType
    {
        get => _fuelType;
        set
        {
            if (_fuelType == value) return;
            _fuelType = value;
            OnPropertyChanged();
        }
    }

    public Status Status
    {
        get => _status;
        set
        {
            if (_status == value) return;
            _status = value;
            OnPropertyChanged();
        }
    }

    public string Error => string.Empty;

    public string this[string columnName]
    {
        get
        {
            return columnName switch
            {
                nameof(Brand) => string.IsNullOrWhiteSpace(Brand) ? "Brand is required." : string.Empty,
                nameof(Model) => string.IsNullOrWhiteSpace(Model) ? "Model is required." : string.Empty,
                nameof(Year) => Year < Constants.MinYear || Year > Constants.MaxYear
                    ? $"Year must be between {Constants.MinYear} and {Constants.MaxYear}."
                    : string.Empty,
                nameof(Price) => Price < 0 ? "Price must be zero or greater." : string.Empty,
                _ => string.Empty
            };
        }
    }

    public Car ToCar()
    {
        return new Car
        {
            Id = Id,
            Brand = Brand,
            Model = Model,
            Year = Year,
            Price = Price,
            FuelType = FuelType,
            Status = Status
        };
    }

    public void ApplyTo(Car car)
    {
        car.Brand = Brand;
        car.Model = Model;
        car.Year = Year;
        car.Price = Price;
        car.FuelType = FuelType;
        car.Status = Status;
    }

    private bool IsValid
    {
        get
        {
            return string.IsNullOrEmpty(this[nameof(Brand)])
                && string.IsNullOrEmpty(this[nameof(Model)])
                && string.IsNullOrEmpty(this[nameof(Year)])
                && string.IsNullOrEmpty(this[nameof(Price)]);
        }
    }
}

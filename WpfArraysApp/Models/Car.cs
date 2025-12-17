using WpfArraysApp.Infrastructure;

namespace WpfArraysApp.Models;

public enum FuelType
{
    Gasoline,
    Diesel,
    Hybrid,
    Electric
}

public enum Status
{
    Available,
    Reserved,
    Sold
}

public class Car : NotifyPropertyChangedBase
{
    private int _id;
    private string _brand = string.Empty;
    private string _model = string.Empty;
    private int _year;
    private decimal _price;
    private FuelType _fuelType;
    private Status _status;

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
            OnPropertyChanged(nameof(PriceDisplay));
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

    public string PriceDisplay => Price.ToString("F2");
}

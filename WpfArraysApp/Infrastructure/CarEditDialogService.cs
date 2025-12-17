using System.Windows;
using WpfArraysApp.Models;
using WpfArraysApp.ViewModels;
using WpfArraysApp.Views;

namespace WpfArraysApp.Infrastructure;

public interface ICarEditDialogService
{
    Car? ShowAddDialog(int nextId);
    bool ShowEditDialog(Car car);
}

public class CarEditDialogService : ICarEditDialogService
{
    public Car? ShowAddDialog(int nextId)
    {
        var viewModel = new CarEditViewModel
        {
            Id = nextId
        };

        var window = CreateWindow(viewModel);
        var result = window.ShowDialog();
        return result == true ? viewModel.ToCar() : null;
    }

    public bool ShowEditDialog(Car car)
    {
        var viewModel = new CarEditViewModel(car);
        var window = CreateWindow(viewModel);
        var result = window.ShowDialog();
        if (result == true)
        {
            viewModel.ApplyTo(car);
            return true;
        }

        return false;
    }

    private static CarEditWindow CreateWindow(CarEditViewModel viewModel)
    {
        var window = new CarEditWindow
        {
            DataContext = viewModel,
            Owner = Application.Current?.MainWindow
        };

        viewModel.RequestClose += (_, dialogResult) =>
        {
            window.DialogResult = dialogResult;
            window.Close();
        };

        return window;
    }
}

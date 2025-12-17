using System.Windows;
using WpfArraysApp.Infrastructure;
using WpfArraysApp.ViewModels;

namespace WpfArraysApp.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainViewModel(new CarEditDialogService());
    }
}

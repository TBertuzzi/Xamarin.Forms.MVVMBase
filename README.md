# Xamarin.Forms.MVVMBase

Simple MVVM framework for Xamarin.Forms projects

###### This is the component, works on iOS, Android and UWP.

**NuGet**

|Name|Info|
| ------------------- | :------------------: |
|Xamarin.Forms.MVVMBase|[![NuGet](https://img.shields.io/badge/nuget-1.0.0-blue.svg)](https://www.nuget.org/packages/Xamarin.Forms.MVVMBase/)|

**Platform Support**

Xamarin.Forms.MVVMBase is a .NET Standard 2.0 library.Its only dependency is the Xamarin.Forms

## Initialize

App.cs :

```csharp

 public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            BuildDependencies();
            InitNavigation();
        }

        public void BuildDependencies()
        {
            ViewModelLocator.Current.RegisterForNavigation<MainPage, MainViewModel>();
        }

        async void InitNavigation()
        {
            var navigationService = ViewModelLocator.Current.Resolve<INavigationService>();
            await navigationService.InitializeAsync<MainViewModel>(null, true);
        }
    }
```

Determines which Initial ViewModel of your app :

```csharp
InitializeAsync<MainViewModel>(null, true)
```

To register the View Matching ViewModel, use:

```csharp
ViewModelLocator.Current.RegisterForNavigation <View, ViewModel> ();
```

## Base ViewModel

BaseViewModel is based on all ViewModels. BaseViewModel Implements LoadAsync in addition to Navigation and Dialog Service


```csharp
 public class MainViewModel : BaseViewModel
    {
       //Set Title
        public MainViewModel() : base("Main View")
        {
        }

        //Override Load
        public override async Task LoadAsync(object navigationData)
        {
           
        }
    }
```

BaseViewModel already behind the implementations of Title and isBusy by default to use in all views

## Navigation


# Xamarin.Forms.MVVMBase

Simple MVVM framework for Xamarin.Forms projects

###### This is the component, works on iOS, Android.

**NuGet**

| Name                   |                                                           Info                                                           |
| ---------------------- | :----------------------------------------------------------------------------------------------------------------------: |
| Xamarin.Forms.MVVMBase | [![NuGet](https://buildstats.info/nuget/Xamarin.Forms.MVVMBase)](https://www.nuget.org/packages/Xamarin.Forms.MVVMBase/) |

**Platform Support**

Xamarin.Forms.MVVMBase is a .NET Standard 2.0 library.Its dependencies are Xamarin.Forms and Microsoft Extensions for iOC

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
           Container.Current.RegisterForNavigation<MainPage, MainViewModel>();

            //Configure Container
            Container.Current.Setup();
        }

        async void InitNavigation()
        {
               var navigationService = Container.Current.Resolve<INavigationService>();

            //Basic Startup
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
Container.Current.RegisterForNavigation <View, ViewModel> ();
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
        public override async Task LoadAsync(NavigationParameters navigationData)
        {

        }

        //Override OnNavigate
        public override async Task OnNavigate(NavigationParameters navigationData)
        {
            if (navigationData.NavigationState == NavigationState.Backward)
            {
                //you can use the navigation to identify whether you have returned from a viewmodel
            }

            if (navigationData.NavigationState == NavigationState.Forward)
            {
                //you can use the navigation to identify whether you have navigated to a viewmodel
            }
        }
    }
```

BaseViewModel already behind the implementations of Title and isBusy by default to use in all views

## Navigation

Use BaseViewModel NavigationService to navigate between them. You can also send parameters that will be received in LoadAsync.

```csharp
await NavigationService.NavigateToAsync<DetalhesViewModel>();
await NavigationService.NavigateToAsync<DetalhesViewModel>(parameter);
```

NavigationParameters can be used to send parameters to a View, both when navigating to a new one and returning to the previous one.

```csharp
  var parametros = new NavigationParameters();
  parametros.Add("key", value);
```

navigationData also returns if it is a new navigation or return from some view. use NavigationState :

```csharp
 public enum NavigationState
    {
        Init, //Start App
        Forward, //Navigation to next View
        Backward // Return Navigation
    }
```

In addition to object navigation you can also browse via queryString as if using a url :

```csharp
 NavigationParameters(string queryString)

```

## Dialog

Use BaseViewModel DialogService to display custom alerts or an actionsheet.

```csharp
 //Alert
 await DialogService.AlertAsync("Title", "Message", "Cancel Button Label");
 //Dialog
 await DialogService.AlertAsync("Title", "Message", "Accept Button Label", "Cancel Button Label");
 //ActionSheet
 await DialogService.ActionSheetAsync("Title", "Message", "Destruction Button Label", buttons);
```

## BasePage

You can use BasePage instead of the standard ContentPage. BasePage automatically changes the title to that of BaseViewModel and also implements DeepLink

```csharp
 /<?xml version="1.0" encoding="utf-8"?>
<base:BasePage   xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:d="http://xamarin.com/schemas/2014/forms/design"
             xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
             xmlns:base="clr-namespace:Xamarin.Forms.MVVMBase.Page;assembly=Xamarin.Forms.MVVMBase"
             mc:Ignorable="d">

</base:BasePage>

 public void OnAppLinkRequestReceived(Uri uri)

```

## Dependency Injection

Xamarin.Forms.MVVMBase uses DryIoc. You can use your container in the app.cs with :

```csharp
  Container.Current.Register<Interface, Implementation> (LifeTime.Singleton);
```

Use the lifetime for your IoC

```csharp
public enum LifeTime
    {
        Scoped,
        Singleton,
        Transient
    }
```

## For full Sample [click here](https://github.com/TBertuzzi/Xamarin.Forms.MVVMBase/tree/master/MVVMBase.Sample)

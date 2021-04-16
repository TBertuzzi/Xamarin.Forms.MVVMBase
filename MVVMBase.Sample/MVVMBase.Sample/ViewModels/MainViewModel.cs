using System;
using System.Threading.Tasks;
using Xamarin.Forms.MVVMBase.Services.Navigation;
using Xamarin.Forms.MVVMBase.ViewModels;

namespace MVVMBase.Sample.ViewModels
{
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
    }
}

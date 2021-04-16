using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.MVVMBase.Services.Dialog;
using Xamarin.Forms.MVVMBase.Services.Navigation;

namespace Xamarin.Forms.MVVMBase.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
            {
                return false;
            }

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }



        protected readonly INavigationService NavigationService;
        protected readonly IDialogService DialogService;

        string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value; OnPropertyChanged(); }
        }

        bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { _isBusy = value; OnPropertyChanged(); }
        }

        public BaseViewModel(string title)
        {
            Title = title;
            NavigationService = ViewModelLocator.Current.Resolve<INavigationService>();
            DialogService = ViewModelLocator.Current.Resolve<IDialogService>();
        }

        public virtual Task LoadAsync(NavigationParameters navigationData) => Task.FromResult(false);

        public virtual Task OnNavigate(NavigationParameters navigationData) => Task.FromResult(false);

        public virtual Task ResumeASync() => Task.FromResult(false);

        public virtual void AppLinkRequestReceive(Uri uri) => Task.FromResult(false);

    }
}

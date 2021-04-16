using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms.MVVMBase.Services.Dialog;
using Xamarin.Forms.MVVMBase.Services.Navigation;
using Xamarin.Forms.MVVMBase.ViewModels;

namespace Xamarin.Forms.MVVMBase
{
    public class Container
    {

        public IServiceCollection Services { get; set; }
        private static IServiceProvider _ServiceProvider { get; set; }

        internal Dictionary<Type, Type> Mappings;

        static Lazy<Container> LazyContainer = new Lazy<Container>(() => new Container());
        public static Container Current => LazyContainer.Value;

        public Container()
        {
            Services = new ServiceCollection();

            Services.AddSingleton<INavigationService, NavigationService>();
            Services.AddSingleton<IDialogService, DialogService>();

            Mappings = new Dictionary<Type, Type>();

        }

        public T Resolve<T>() => _ServiceProvider.GetService<T>();
        public object Resolve(Type type) => _ServiceProvider.GetService(type);


        public void RegisterForNavigation<TView, TViewModel>()
            where TView : Xamarin.Forms.Page
            where TViewModel : BaseViewModel
        {
            Mappings.Add(typeof(TViewModel), typeof(TView));

            Services.AddTransient<TViewModel>();
        }

        public void Setup()
        {
            _ServiceProvider = null;
            _ServiceProvider = Services.BuildServiceProvider();
        }

    }

}

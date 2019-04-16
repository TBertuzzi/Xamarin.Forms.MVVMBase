using System;
using System.Collections.Generic;
using DryIoc;
using Xamarin.Forms.MVVMBase.Services.Dialog;
using Xamarin.Forms.MVVMBase.Services.Navigation;


namespace Xamarin.Forms.MVVMBase.ViewModels
{
    public class ViewModelLocator
    {
       
        public Container ContainerBuilder;

        internal Dictionary<Type, Type> Mappings;
    
        static Lazy<ViewModelLocator> LazyViewModel = new Lazy<ViewModelLocator>(() => new ViewModelLocator());
        public static ViewModelLocator Current => LazyViewModel.Value;

        public ViewModelLocator()
        {
            ContainerBuilder = new Container();

            ContainerBuilder.Register<INavigationService,NavigationService>();
            ContainerBuilder.Register<IDialogService,DialogService>();
          
            Mappings = new Dictionary<Type, Type>();
        }

        public T Resolve<T>() => ContainerBuilder.Resolve<T>();
        public object Resolve(Type type) => ContainerBuilder.Resolve(type);

        public void Register<TInterface, TImplementation>() where TImplementation : TInterface =>
            ContainerBuilder.Register<TInterface, TImplementation>();
        public void Register<T>() where T : class => ContainerBuilder.Register<T>();

        public void RegisterForNavigation<TView, TViewModel>()
            where TView : Xamarin.Forms.Page
            where TViewModel : BaseViewModel
        {
            Mappings.Add(typeof(TViewModel), typeof(TView));

            ContainerBuilder.Register<TViewModel>();
        }
    }
}

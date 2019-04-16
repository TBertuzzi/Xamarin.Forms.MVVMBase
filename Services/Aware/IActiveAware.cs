using System;
namespace Xamarin.Forms.MVVMBase.Services.Aware
{
    public interface IActiveAware
    {
        bool IsActive { get; set; }
        event EventHandler IsActiveChanged;
    }
}

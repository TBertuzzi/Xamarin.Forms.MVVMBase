using System;
using Xamarin.Forms.MVVMBase.Page;

namespace Xamarin.Forms.MVVMBase
{
    public class BaseApplication : Application
    {
        public new static BaseApplication Current => (BaseApplication)Application.Current;

        protected override async void OnResume()
        {
            if (MainPage is BasePage page)
            {
                await page.OnResume();
                return;
            }
        }

        protected override void OnAppLinkRequestReceived(Uri uri)
        {
            if (MainPage is BasePage page)
            {
                page.OnAppLinkRequestReceived(uri);
            }

            base.OnAppLinkRequestReceived(uri);
        }

    }
}

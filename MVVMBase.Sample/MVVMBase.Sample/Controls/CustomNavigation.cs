using System;
using Xamarin.Forms;

namespace MVVMBase.Sample.Controls
{
    public class CustomNavigation : NavigationPage
    {
        public CustomNavigation(Page root) : base(root)
        {
            Init();
            Title = root.Title;
            IconImageSource = root.IconImageSource;
        }

        public CustomNavigation()
        {
            Init();
        }

        void Init()
        {
            //Initialization codes
        }
    }
}

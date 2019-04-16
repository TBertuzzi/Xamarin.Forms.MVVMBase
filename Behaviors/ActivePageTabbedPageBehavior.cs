using System;
using System.Linq;
using Xamarin.Forms.MVVMBase.Services.Aware;

namespace Xamarin.Forms.MVVMBase.Behaviors
{
    public class ActivePageTabbedPageBehavior : Behavior<TabbedPage>
    {
        protected override void OnAttachedTo(TabbedPage tabbedPage)
        {
            base.OnAttachedTo(tabbedPage);
            tabbedPage.CurrentPageChanged += OnTabbedPageCurrentPageChanged;
        }

        protected override void OnDetachingFrom(TabbedPage tabbedPage)
        {
            base.OnDetachingFrom(tabbedPage);
            tabbedPage.CurrentPageChanged -= OnTabbedPageCurrentPageChanged;
        }

        private void OnTabbedPageCurrentPageChanged(object sender, EventArgs e)
        {
            var tabbedPage = (TabbedPage)sender;

            // Deactivate previously selected page
            IActiveAware prevActiveAwarePage = tabbedPage.Children.OfType<IActiveAware>()
                .FirstOrDefault(c => c.IsActive && tabbedPage.CurrentPage != c);
            if (prevActiveAwarePage != null)
            {
                prevActiveAwarePage.IsActive = false;
            }

            // Activate selected page
            if (tabbedPage.CurrentPage is IActiveAware activeAwarePage)
            {
                activeAwarePage.IsActive = true;
            }
        }
    }
}

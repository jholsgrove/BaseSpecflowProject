namespace BaseSolution.Pages
{
    using System;

    using BaseSolution.Constants;
    using BaseSolution.Pages;

    internal partial class SUTMainPage : PageBase
    {
        public SUTMainPage(PageContext context)
            : base(context)
        {
        }

        public void ClickOnLogo()
        {
            ClickById(IdAttribute.Logo);
        }
    }
}
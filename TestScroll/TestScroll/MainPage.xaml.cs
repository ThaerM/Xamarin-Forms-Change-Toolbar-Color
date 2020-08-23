using System;
using Xamarin.Forms;

namespace TestScroll
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            if (Application.Current.MainPage is NavigationPage result)
            {
                result.BarBackgroundColor = Color.FromRgba(0, 0, 0, 0);
            }
        }

        private void OnClickChnage(object sender, EventArgs e)
        {
            if (Application.Current.MainPage is NavigationPage result)
            {
                result.BarBackgroundColor = Color.Red;
            }
        }

        private void ScrollView_Scrolled(object sender, ScrolledEventArgs e)
        {
            if (e.ScrollY > 100)
            {
                if (Application.Current.MainPage is NavigationPage result)
                {
                    result.BarBackgroundColor = Color.FromRgba(0, 0, 0, (e.ScrollY / 100));
                    result.BarTextColor = Color.White;
                }
            }
            else
            {
                if (Application.Current.MainPage is NavigationPage result)
                {
                    result.BarBackgroundColor = Color.Transparent;
                    result.BarTextColor = Color.Black;
                }
            }
        }
    }
}

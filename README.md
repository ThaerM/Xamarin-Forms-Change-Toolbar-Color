# Xamarin Forms Change Toolbar Color 
Change toolbar color from Transparent to any color 

Reference from [CrossGeeks](https://github.com/CrossGeeks/Forms-TransparentNavigationBar/tree/master/TransparentNavBarXForms)

 <img src="art/untitled.gif"  height="480"> 

1) Create CustomNavigationPage on Xamarin
- CustomNavigationPage.Xaml file
``` 
<?xml version="1.0" encoding="utf-8" ?>
<NavigationPage
    x:Class="TestScroll.CustomNavigationPage"
    xmlns="http://xamarin.com/schemas/2014/forms"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    xmlns:iOS="clr-namespace:Xamarin.Forms.PlatformConfiguration.iOSSpecific;assembly=Xamarin.Forms.Core"
    iOS:NavigationPage.IsNavigationBarTranslucent="True"
    BarTextColor="Black">
    <NavigationPage.BarBackgroundColor>
        <OnPlatform x:TypeArguments="Color">
            <On Platform="Android, iOS" Value="Transparent" />
        </OnPlatform>
    </NavigationPage.BarBackgroundColor>
</NavigationPage>
```
- CustomNavigationPage.cs file
``` 
  public partial class CustomNavigationPage : NavigationPage
    {
        public CustomNavigationPage() : base()
        {
            InitializeComponent();
        }

        public CustomNavigationPage(Page root) : base(root)
        {
            InitializeComponent();
        }

        public bool IgnoreLayoutChange { get; set; } = false;

        protected override void OnSizeAllocated(double width, double height)
        {
            if (!IgnoreLayoutChange)
                base.OnSizeAllocated(width, height);
        }
    }
```

2) On Android:-
CustomNavigationRenderer.cs
```
[assembly: ExportRenderer(typeof(CustomNavigationPage), typeof(CustomNavigationRenderer))]
namespace TestScroll.Droid
{
    public class CustomNavigationRenderer : NavigationPageRenderer
    {
        public CustomNavigationRenderer(Context context) : base(context)
        {

        }
        IPageController PageController => Element as IPageController;
        CustomNavigationPage CustomNavigationPage => Element as CustomNavigationPage;

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            CustomNavigationPage.IgnoreLayoutChange = true;
            base.OnLayout(changed, l, t, r, b);
            CustomNavigationPage.IgnoreLayoutChange = false;

            int containerHeight = b - t;

            PageController.ContainerArea = new Rectangle(0, 0, Context.FromPixels(r - l), Context.FromPixels(containerHeight));

            for (var i = 0; i < ChildCount; i++)
            {
                AView child = GetChildAt(i);

                if (child is Android.Support.V7.Widget.Toolbar)
                {
                    continue;
                }

                child.Layout(0, 0, r, b);
            }
        }
    }
}
```

3) On App.cs:-
```

            MainPage = new CustomNavigationPage(new MainPage());
```

4) Sample scroll
- MainPage.cs

```
    private void ScrollView_Scrolled(object sender, ScrolledEventArgs e)
        {
            if (e.ScrollY > 100)
            {
                if (Application.Current.MainPage is NavigationPage result)
                {
                    result.BarBackgroundColor = Color.FromRgba(0, 0, 0, (e.ScrollY - 100) / 100);
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
```

- MainPage.xaml
```
  <ScrollView Scrolled="ScrollView_Scrolled">
        <StackLayout
            HorizontalOptions="CenterAndExpand"
            Spacing="10"
            VerticalOptions="FillAndExpand">
            <Label Text="Welcome to Xamarin.Forms!" />
            <Label Text="Welcome to Xamarin.Forms!" />
            ...
            <Label Text="Welcome to Xamarin.Forms!" />
        </StackLayout>
    </ScrollView>
```



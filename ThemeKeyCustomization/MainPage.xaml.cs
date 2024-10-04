using Syncfusion.Maui.Themes;
using ThemeKeyCustomization.Resources.Styles;

namespace ThemeKeyCustomization
{
    public partial class MainPage : ContentPage
    {
        DarkTheme darkTheme = new DarkTheme();
        LightTheme lightTheme = new LightTheme();

        ICollection<ResourceDictionary>? mergedDictionaries;

        SyncfusionThemeResourceDictionary theme = new();
        public MainPage()
        {
            InitializeComponent();
            if (Application.Current != null)
            {
                mergedDictionaries = Application.Current.Resources.MergedDictionaries;
                if(mergedDictionaries!=null)
                    theme = mergedDictionaries.OfType<SyncfusionThemeResourceDictionary>().FirstOrDefault();
            }
        }

        protected override void OnAppearing()
        {
            if (Application.Current != null)
            {
                this.ApplyTheme(Application.Current.RequestedTheme);
                Application.Current.RequestedThemeChanged += Current_RequestedThemeChanged;
            }
            base.OnAppearing();
        }

        private void Current_RequestedThemeChanged(object? sender, AppThemeChangedEventArgs e)
        {
            this.ApplyTheme(e.RequestedTheme);
        }
        public void ApplyTheme(AppTheme appTheme)
        {
            if (Application.Current != null)
            {
                if (mergedDictionaries != null)
                {
                    if (theme != null)
                    {
                        if (appTheme is AppTheme.Light)
                        {
                            theme.VisualTheme = SfVisuals.MaterialLight;
                            if (darkTheme != null)
                            {
                                mergedDictionaries.Remove(darkTheme);
                            }
                            mergedDictionaries.Add(lightTheme);
                        }
                        else
                        {
                            theme.VisualTheme = SfVisuals.MaterialDark;
                            if (lightTheme != null)
                            {
                                mergedDictionaries.Remove(lightTheme);
                            }
                            mergedDictionaries.Add(darkTheme);
                        }
                    }
                }
            }
        }

        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            if (theme.VisualTheme is SfVisuals.MaterialDark)
            {
                this.ApplyTheme(AppTheme.Light);
            }
            else
            {
                this.ApplyTheme(AppTheme.Dark);
            }
        }
    }

}

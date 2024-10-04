This article demonstrates how to assign values to [Syncfusion theme keys](https://help.syncfusion.com/maui/themes/keys) for different themes in a [.NET MAUI Syncfusion controls](https://www.syncfusion.com/maui-controls). Syncfusion provides theme keys that allow developers to customize the appearance of controls, and this article focuses on how to define and apply custom values to these keys for both light and dark themes.

**App.xaml Configuration**

Ensure that your App.xaml includes the [SyncfusionThemeResourceDictionary](https://help.syncfusion.com/cr/maui/Syncfusion.Maui.Themes.SyncfusionThemeResourceDictionary.html):

```
<Application ...             
             xmlns:theme="clr-namespace:Syncfusion.Maui.Themes;assembly=Syncfusion.Maui.Core">
    <Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <theme:SyncfusionThemeResourceDictionary />
                ...
            </ResourceDictionary.MergedDictionaries>
        </ResourceDictionary>
    </Application.Resources>
</Application>
```

**Define Custom Resource Dictionaries for Light and Dark Themes**

Create separate resource dictionaries for light and dark themes and assign different values to Syncfusion theme keys for each. Here we have customized the `SfChatIncomingMessageAuthorTextColor` key for [.NET MAUI Chat](https://www.syncfusion.com/maui-controls/maui-chat) control.

Dark Theme:

```
<ResourceDictionary …
             x:Class="ThemeSample.Resources.Styles.DarkTheme" >   

    <Color x:Key="SfChatIncomingMessageAuthorTextColor">HotPink</Color> 
</ResourceDictionary>
```

Light Theme:

```
<ResourceDictionary …
             x:Class="ThemeSample.Resources.Styles.LightTheme">  

    <Color x:Key="SfChatIncomingMessageAuthorTextColor">Green</Color>
</ResourceDictionary>
```

**XAML**

Initialize the Maui control. 

```
<ContentPage.Content>
    <Grid RowDefinitions="50,*">
        <HorizontalStackLayout Grid.Row="0" Spacing="5" HorizontalOptions="End">
            <Label Text="Switch Theme" VerticalTextAlignment="Center"/>
            <Switch Toggled="Switch_Toggled" />
        </HorizontalStackLayout>
        <sfChat:SfChat x:Name="sfChat" Grid.Row="1"
           Messages="{Binding Messages}"
           CurrentUser="{Binding CurrentUser}"/>
    </Grid>
</ContentPage.Content>
```

**C- Sharp**

You can switch between the light and dark themes dynamically based on the system's theme or user preferences. For instance, by using the RequestedThemeChanged event, you can update the [SyncfusionThemeResourceDictionary](https://help.syncfusion.com/cr/maui/Syncfusion.Maui.Themes.SyncfusionThemeResourceDictionary.html) at runtime to reflect the correct theme.

```
public partial class MainPage : ContentPage
{
    DarkTheme darkTheme = new DarkTheme();
    LightTheme lightTheme = new LightTheme();
    ICollection<ResourceDictionary> mergedDictionaries;
    SyncfusionThemeResourceDictionary theme ;
    public MainPage()
    {
        InitializeComponent();
        mergedDictionaries = Application.Current.Resources.MergedDictionaries;
        theme = mergedDictionaries.OfType<SyncfusionThemeResourceDictionary>().FirstOrDefault();
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
                        if (darkTheme != null )
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
```

**Output**

![Themekey_Customization.gif](https://support.syncfusion.com/kb/agent/attachment/article/17420/inline?token=eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjI5NTQ5Iiwib3JnaWQiOiIzIiwiaXNzIjoic3VwcG9ydC5zeW5jZnVzaW9uLmNvbSJ9.91rgjdW59oW4jmwAtt3jkxWhH-uGtmOVqPQ5qHyJF6U)

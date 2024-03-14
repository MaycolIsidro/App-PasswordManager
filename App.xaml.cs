using PasswordManager.Views;

namespace PasswordManager
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPinPage());
            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(Entry), (handler, view) =>
            {
#if __ANDROID__
                handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
#endif
            });
            Microsoft.Maui.Handlers.SearchBarHandler.Mapper.AppendToMapping(nameof(SearchBar), (handler, view) =>
            {
#if __ANDROID__          
                Android.Widget.LinearLayout linearLayout = handler.PlatformView.GetChildAt(0) as Android.Widget.LinearLayout;
                linearLayout = linearLayout.GetChildAt(2) as Android.Widget.LinearLayout;
                linearLayout = linearLayout.GetChildAt(1) as Android.Widget.LinearLayout;
                linearLayout.Background = null;
#endif
            });
        }
    }
}

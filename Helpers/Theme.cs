using System;
namespace RecibosNominaCGA.Helpers
{
	public class Theme
	{
        public static void SetTheme()
        {

    
            switch (Settings.Theme)
            {
                //Default
                case 0:
                    App.Current!.UserAppTheme = AppTheme.Unspecified;
                    break;
                //Light
                case 1:
                    App.Current!.UserAppTheme = AppTheme.Light;
                    break;
                //Dark
                case 2:
                    App.Current!.UserAppTheme = AppTheme.Dark;
                    break;
            }
        }
    }
}


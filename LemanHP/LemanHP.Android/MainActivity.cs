﻿using Android.App;
using Android.Content.PM;
using Android.OS;
using Plugin.Permissions;

namespace LemanHP.Droid
{
    [Activity(Label = "@string/app_name",
        Theme = "@style/MyTheme",
        MainLauncher =false, 
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
           // ConfigurationManager.Initialise(PCLAppConfig.FileSystemStream.PortableStream.Current);

            LoadApplication(new App());
        }

      

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
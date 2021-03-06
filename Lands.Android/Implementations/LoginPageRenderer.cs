﻿[assembly: Xamarin.Forms.ExportRenderer(
    typeof(Lands.Views.LoginFacebookPage),
    typeof(Lands.Droid.Implementations.LoginPageRenderer))]

namespace Lands.Droid.Implementations
{
    using System;
    using System.Threading.Tasks;
    using Android.App;
    using Android.Content;
    using Models;
    using Services;
    using Xamarin.Auth;
    using Xamarin.Forms.Platform.Android;

    public class LoginPageRenderer : PageRenderer
    {
        public LoginPageRenderer(Context context) : base (context)
        {
            var activity = context as Activity;

            var auth = new OAuth2Authenticator(
                clientId: "513586705788621",
                scope: "email",
                authorizeUrl: new Uri("https://www.facebook.com/dialog/oauth/"),
                redirectUrl: new Uri("https://www.facebook.com/connect/login_success.html"));

            auth.Completed += async (sender, eventArgs) =>
            {
                if (eventArgs.IsAuthenticated)
                {
                    var accessToken = eventArgs.Account.Properties["access_token"].ToString();
                    var profile = await GetFacebookProfileAsync(accessToken);
                    await App.NavigateToProfile(profile);
                }
                else
                {
                    App.HideLoginView();
                }
            };

            activity.StartActivity(auth.GetUI(activity));
        }

        private async Task<FacebookResponse> GetFacebookProfileAsync(string accessToken)
        {
            var apiService = new ApiService();
            return await apiService.GetFacebook(accessToken);
        }
    }
}

using Android.App;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Xamarin.Forms;
using XamarinForms_SyncTask.Android;

[assembly: Dependency(typeof(ADAuthenticator))]

namespace XamarinForms_SyncTask.Android
{
    public class ADAuthenticator : IADAuthenticator
    {
        public IPlatformParameters GetPlatformParameters()
        {
            var activity = MainActivity.Instance;
            return new PlatformParameters(activity);
        }
    }
}

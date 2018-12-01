using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace XamarinForms_SyncTask
{
    public interface IADAuthenticator
    {
        IPlatformParameters GetPlatformParameters();
    }
}

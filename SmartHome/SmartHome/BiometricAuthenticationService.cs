using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using System.Threading.Tasks;

namespace SmartHome.Services
{
    public class BiometricAuthenticationService
    {
        public async Task<bool> AuthenticateAsync()
        {
            var result = await CrossFingerprint.Current.AuthenticateAsync(new AuthenticationRequestConfiguration("Autenticación Biométrica", "Usa tu huella digital para acceder."));
            return result.Authenticated;
        }
    }
}


using TokenService.Interfaces;

namespace TokenService.Services
{
    public class CacheService : ICacheService
    {
        private string _cachedToken;
        private DateTime? _tokenExpirationTime;

        public async Task<string> GetTokenAsync()
        {
            if (_cachedToken == null || _tokenExpirationTime <= DateTime.UtcNow)
            {
                return null;
            }

            return _cachedToken;
        }

        public async Task SetTokenAsync(string token)
        {
            _cachedToken = token;
            _tokenExpirationTime = DateTime.UtcNow.AddHours(1);
        }
    }

}

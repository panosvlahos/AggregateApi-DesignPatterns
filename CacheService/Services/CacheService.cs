using CacheServices;

public class CacheService : ICacheService
{
    private string _cachedToken;
    private DateTime? _tokenExpirationTime;
    private Dictionary<string, (object Data, DateTime ExpirationTime)> _cachedData = new();

    public string GetToken()
    {
        if (_cachedToken == null || _tokenExpirationTime <= DateTime.UtcNow)
        {
            return null;
        }
        return _cachedToken;
    }

    public void SetToken(string token)
    {
        _cachedToken = token;
        _tokenExpirationTime = DateTime.UtcNow.AddHours(1);
    }

    public T GetData<T>(string key)
    {
        if (_cachedData.ContainsKey(key) && _cachedData[key].ExpirationTime > DateTime.UtcNow)
        {
            return (T)_cachedData[key].Data;
        }
        return default;
    }

    public void SetData<T>(string key, T data, TimeSpan? expirationDuration = null)
    {
        var expirationTime = expirationDuration ?? TimeSpan.FromMinutes(5);
        _cachedData[key] = (data, DateTime.UtcNow.Add(expirationTime));
    }

    public void ClearData(string key)
    {
        if (_cachedData.ContainsKey(key))
        {
            _cachedData.Remove(key);
        }
    }

    public void ClearAllData()
    {
        _cachedData.Clear();
    }
}

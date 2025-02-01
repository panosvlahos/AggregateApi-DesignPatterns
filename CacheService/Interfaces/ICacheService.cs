using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheServices
{
    public interface ICacheService
    {
        string GetToken();
        void SetToken(string token);
        T GetData<T>(string key);
        void SetData<T>(string key, T data, TimeSpan? expirationDuration = null);
        void ClearData(string key); // Fixed signature
        void ClearAllData();
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokenService.Interfaces
{
    public interface ICacheService
    {
        Task<string> GetTokenAsync();
        Task SetTokenAsync(string token);
    }
}

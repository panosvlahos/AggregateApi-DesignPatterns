using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokenService.Interfaces
{
    public interface ITwitterServiceService
    {
        
            Task<string> GetAccessTokenAsync();
        
    }
}

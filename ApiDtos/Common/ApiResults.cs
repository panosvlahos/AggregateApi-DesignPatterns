using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDtos.Common
{
    public class ApiResult<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public string ErrorMessage { get; set; }
    }

}

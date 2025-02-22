﻿using Domain.Models.Request;
using Domain.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterServices
{
    public interface ITwitterServiceService
    {
        
            Task GetAccessDataAsync();
        Task<TwitterResponse> GetTwitterDataAsync();
    }
}

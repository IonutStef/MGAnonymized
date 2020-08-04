using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MGAnonymized.Web.Services.Interfaces
{
    public interface IDataInitializerService
    {
        Task InitializeData();
    }
}
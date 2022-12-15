using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TorneioCS.Services
{
    interface IDBService
    {
        void StartDB(IServiceProvider service);
        void StartDB(object serviceProvider);
    }
}

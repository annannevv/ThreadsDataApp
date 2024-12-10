using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrentDataApp.Interfaces
{
    internal interface IGenerator
    {
        Task<string> GenerateNumberAsync();
        Task<string> GenerateNameAsync();
    }
}

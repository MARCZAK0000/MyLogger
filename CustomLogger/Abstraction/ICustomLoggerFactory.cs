using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomLogger.Abstraction
{
    public interface ICustomLoggerFactory : IDisposable
    {
        ICustomLogger<T> CreateLogger<T>() where T: class;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Extensions
{
    public interface ICacheManager : IDisposable
    {
        T Get<T>(string key);

        void Set(string key, object data, int cacheTime);

        bool IsSet(string key);

        void Remove(string key);

        //void RemoveByPattern(string pattern);

        //void RemoveByPatternwithoutregex(string pattern);

        //void Clear();
    }
}

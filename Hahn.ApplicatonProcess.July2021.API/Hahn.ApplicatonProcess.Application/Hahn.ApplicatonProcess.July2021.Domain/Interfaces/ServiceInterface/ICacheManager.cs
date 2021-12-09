using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.July2021.Domain.Interfaces.ServiceInterface
{
    /// <summary>
    /// Specifies the methods related to Cache
    /// </summary>
    public interface ICacheManager
    {
        /// <summary>
        /// Adds the Cache value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="vlaue"></param>
        /// <param name="memoryCacheEntryOptions"></param>
        void Add<T>(string key, T vlaue, MemoryCacheEntryOptions memoryCacheEntryOptions);

        /// <summary>
        /// Gets the cached value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        T Get<T>(string key, out T val);

        /// <summary>
        /// Remove the cached value
        /// </summary>
        /// <param name="key"></param>
        void Remove(string key);
    }
}

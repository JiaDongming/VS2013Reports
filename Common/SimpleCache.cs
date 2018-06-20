using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Collections;

namespace Common
{
    public enum SimpleCacheType
    {
        Tree,
        Item,
        Folder
    }
    public delegate T OnGetCacheValue<T>();

    public class SimpleCache
    {
        public static T GetCacheValue<T>(object key, SimpleCacheType cacheType, OnGetCacheValue<T> onGetValue, int timeOut = 9999)
        {
            string cacheKey = (int)cacheType + "#_#" + key.ToString();
            T result = default(T);
            try
            {
                result = (T)HttpRuntime.Cache[cacheKey];
            }
            catch
            {
                result = default(T);
            }
            if (result == null && onGetValue != null)
            {
                result = onGetValue();
                if (result != null)
                {
                    HttpRuntime.Cache.Insert(cacheKey, result, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromSeconds(timeOut));
                }
            }
            return result;
        }

        public static void Remove(object key, SimpleCacheType cacheType)
        {
            string cacheKey = (int)cacheType + "#_#" + key.ToString();
            HttpRuntime.Cache.Remove(cacheKey);
        }

        public static void Clear()
        {
            List<string> toRemove = new List<string>();
            foreach (DictionaryEntry cacheItem in HttpRuntime.Cache)
            {
                toRemove.Add(cacheItem.Key.ToString());
            }
            foreach (string key in toRemove)
            {
                HttpRuntime.Cache.Remove(key);
            }
        }
    }
}

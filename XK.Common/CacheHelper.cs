using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;

namespace XK.Common {
    public static class CacheHelper {
        /// <summary>
        /// 绝对过期：添加缓存,并返回缓存(默认 20 分钟过期)
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="t">值</param>
        public static object Add<T>(string key, T t) {

            var obj = HttpRuntime.Cache.Add(key, t, null, DateTime.Now.AddMinutes(20), Cache.NoSlidingExpiration,
                CacheItemPriority.Default, null);
            if (obj == null) {
                //说明此缓存项不存在,添加成功

            }
            else {
                //添加失败
            }
            return obj;
        }

        /// <summary>
        /// 绝对过期：添加缓存(默认 20 分钟过期)
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="t">值</param>
        public static void Insert<T>(string key, T t) {

            HttpRuntime.Cache.Insert(key, t, null, DateTime.Now.AddMinutes(20), Cache.NoSlidingExpiration,
                CacheItemPriority.Default, null);
        }

        /// <summary>
        /// 绝对过期：添加缓存
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="t">值</param>
        /// <param name="expireTime">过期时间</param>
        public static void Insert<T>(string key, T t, DateTime expireTime) {
            HttpRuntime.Cache.Insert(key, t, null, expireTime, Cache.NoSlidingExpiration,
                CacheItemPriority.Default, null);
        }

        /// <summary>
        /// 平滑过期：添加缓存
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="t">值</param>
        /// <param name="timeSpan">从不过期的时间间隔</param>
        public static void InsertNoAbsoluteCache<T>(string key, T t, TimeSpan timeSpan) {
            HttpRuntime.Cache.Insert(key, t, null, Cache.NoAbsoluteExpiration, timeSpan,
                CacheItemPriority.Default, null);
        }

        /// <summary>
        /// 根据 key 获取缓存，不存在返回 null
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object Get(string key) {
            object cacheVal = null;
            if (HttpRuntime.Cache[key] != null) {
                cacheVal = HttpRuntime.Cache[key];
            }
            return cacheVal;
        }

        /// <summary>
        /// 删除 cache 指定项
        /// </summary>
        /// <param name="key"></param>
        public static void Remove(string key) {
            if (HttpRuntime.Cache[key] != null) {
                HttpRuntime.Cache.Remove(key);
            }
        }

        /// <summary>
        /// 更新 cache
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="t"></param>
        public static void Update<T>(string key, T t) {
            Insert(key, t);
        }
    }
}

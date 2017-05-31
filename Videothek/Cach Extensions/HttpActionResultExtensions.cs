using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

public static class HttpActionResultExtensions
{
    public static CachedResult<T> Cached<T>(
        this T actionResult,
        Cacheability cacheability = Cacheability.Private,
        string eTag = null,
        DateTimeOffset? expires = null,
        DateTimeOffset? lastModified = null,
        TimeSpan? maxAge = null,
        bool? noStore = null) where T : IHttpActionResult
    {
        return new CachedResult<T>(actionResult, cacheability, eTag, expires, lastModified, maxAge, noStore);
    }
}
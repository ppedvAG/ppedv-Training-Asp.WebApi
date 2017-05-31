using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

public class CachedResult<T> : IHttpActionResult
    where T : IHttpActionResult
{
    public CachedResult(
        T innerResult,
        Cacheability cacheability,
        string eTag,
        DateTimeOffset? expires,
        DateTimeOffset? lastModified,
        TimeSpan? maxAge,
        bool? noStore)
    {
        Cacheability = cacheability;
        ETag = eTag;
        Expires = expires;
        InnerResult = innerResult;
        LastModified = lastModified;
        MaxAge = maxAge;
        NoStore = noStore;
    }

    public Cacheability Cacheability { get; private set; }
    public string ETag { get; private set; }
    public DateTimeOffset? Expires { get; private set; }
    public T InnerResult { get; private set; }
    public DateTimeOffset? LastModified { get; private set; }
    public TimeSpan? MaxAge { get; private set; }
    public bool? NoStore { get; private set; }

    public async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
    {
        var response = await InnerResult.ExecuteAsync(cancellationToken);
        if (!response.Headers.Date.HasValue)
            response.Headers.Date = DateTimeOffset.UtcNow;

        response.Headers.CacheControl = new CacheControlHeaderValue
        {
            NoCache = Cacheability == Cacheability.NoCache,
            Private = Cacheability == Cacheability.Private,
            Public = Cacheability == Cacheability.Public
        };

        if (response.Headers.CacheControl.NoCache)
        {
            response.Headers.Pragma.TryParseAdd("no-cache");
            response.Content.Headers.Expires = response.Headers.Date;
            return response;  // None of the other headers are valid
        }

        response.Content.Headers.Expires = Expires;
        response.Content.Headers.LastModified = LastModified;
        response.Headers.CacheControl.MaxAge = MaxAge;

        if (!String.IsNullOrWhiteSpace(ETag))
            response.Headers.ETag = new EntityTagHeaderValue(String.Format("\"{0}\"", ETag));

        if (NoStore.HasValue)
            response.Headers.CacheControl.NoStore = NoStore.Value;

        return response;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

public SampleController : ApiController
{
    public IHttpActionResult GetExample(string name)
    {
        return Ok("Hello, " + name).Cached(Cacheability.Public, maxAge: TimeSpan.FromMinutes(15));
    }
}

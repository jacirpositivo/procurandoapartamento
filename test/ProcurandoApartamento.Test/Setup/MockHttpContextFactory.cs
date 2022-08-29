using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Options;

namespace ProcurandoApartamento.Test.Setup
{
    public class MockHttpContextFactory : IHttpContextFactory
    {
        private readonly DefaultHttpContextFactory _delegate;


        public HttpContext Create(IFeatureCollection featureCollection)
        {
            var httpContext = _delegate.Create(featureCollection);
            return httpContext;
        }

        public void Dispose(HttpContext httpContext)
        {
            _delegate.Dispose(httpContext);
        }
    }
}

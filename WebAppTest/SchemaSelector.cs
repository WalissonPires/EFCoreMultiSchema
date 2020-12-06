using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppTest
{
    public class SchemaSelector
    {
        private readonly HttpContext _httpContext;

        public SchemaSelector(IHttpContextAccessor httpContextAccessor)
        {
            _httpContext = httpContextAccessor.HttpContext;
        }

        public string GetSchema()
        {
            // Lógica para obter o schema com base no usuário aqui

            if (_httpContext != null && _httpContext.Request.Query.TryGetValue("environmentId", out var environmentId))
                return environmentId;

            return null;
        }
    }
}

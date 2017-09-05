using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Vanguard.Framework.Http.Resolvers
{
    public class SelectFieldContractResolver : CamelCasePropertyNamesContractResolver
    {
        public SelectFieldContractResolver(IQueryCollection query)
        {
            Query = query;
        }

        public IQueryCollection Query { get; }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);
            property.GetIsSpecified = (obj) =>
            {
                var fields = Query["select"];
                if (fields.Count == 0)
                {
                    return true;
                }

                return fields.Any(field => string.Equals(field, member.Name, StringComparison.OrdinalIgnoreCase));
            };

            return property;
        }
    }
}

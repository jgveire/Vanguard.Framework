using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Vanguard.Framework.Http.Extensions;

namespace Vanguard.Framework.Http.Resolvers
{
    public class SelectFieldContractResolver : CamelCasePropertyNamesContractResolver
    {
        public SelectFieldContractResolver(IQueryCollection query)
        {
            Query = query;
        }

        public IQueryCollection Query { get; }

        /// <inheritdoc />
        public override JsonContract ResolveContract(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            JsonContract contract = CreateContract(type);
            return contract;
        }

        /// <inheritdoc />
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);
            property.GetIsSpecified = (obj) =>
            {
                StringValues selectFields = Query["select"];
                if (selectFields.Count == 0)
                {
                    return true;
                }

                return IsFieldInSelect(member.Name, selectFields);
            };

            return property;
        }

        /// <inheritdoc />
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            var properties = base.CreateProperties(type, memberSerialization);
            return properties
                .OrderBy(p => p.DeclaringType.BaseTypesAndSelf().Count())
                .ToList();
        }

        private static bool IsFieldInSelect(string fieldName, StringValues selectFields)
        {
            return selectFields.Any(field => string.Equals(field, fieldName, StringComparison.OrdinalIgnoreCase));
        }
    }
}

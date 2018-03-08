namespace Node.Utilities.Json
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using Node.Utilities.Json.CustomAttributes;

    public class IgnorePropertiesContractResolver : DefaultContractResolver
    {
        private List<Type> propertiesToIgnore;

        public IgnorePropertiesContractResolver(params Type[] propertiesToIgnore)
        {
            this.propertiesToIgnore = propertiesToIgnore.ToList();
            this.propertiesToIgnore.Add(typeof(AlwaysIgnore));
        }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty property = base.CreateProperty(member, memberSerialization);
            foreach (var propertyToIgnore in this.propertiesToIgnore)
            {
                if (member.GetCustomAttributes(propertyToIgnore).Any())
                {
                    property.ShouldSerialize = instance => { return false; };
                    break;
                }
            }

            return property;
        }
    }
}

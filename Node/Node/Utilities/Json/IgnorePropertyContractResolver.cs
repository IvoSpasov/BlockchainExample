namespace Node.Utilities.Json
{
    using System.Linq;
    using System.Reflection;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public class IgnorePropertyContractResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty property = base.CreateProperty(member, memberSerialization);

            if (member.GetCustomAttributes<MyCustomIgnore>().Any())
            {
                property.ShouldSerialize = instance => { return false; };
            }

            return property;
        }
    }
}

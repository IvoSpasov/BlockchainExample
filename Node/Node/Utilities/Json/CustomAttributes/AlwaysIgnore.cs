namespace Node.Utilities.Json.CustomAttributes
{
    using System;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class AlwaysIgnore : Attribute
    {
    }
}

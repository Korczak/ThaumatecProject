using System;

namespace Thaumatec.Web.Configuration
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class AllowOnFirstLoginAttribute : Attribute
    {
    }
}

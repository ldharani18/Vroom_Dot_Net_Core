using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vroom.Models;

namespace Vroom.Extensions
{
    public static class ReflectionExtensions
    {
        public static string GetPropertyValue<T>(this T item,string propertyName)
        {
            return item.GetType().GetProperty(propertyName).GetValue(item, null).ToString();
        }
    }
}

using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Build.ObjectModelRemoting;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;

namespace ShopApp.WebUI.Extensions
{
    public static class TempDataExtensions
    {
        public static void Put<T>(this ITempDataDictionary tempData,string key, T value) where T : class
        {
            tempData[key] = JsonConvert.SerializeObject(value);
        }

        public static T Get<T>(this ITempDataDictionary tempData,string key) where T : class
        {
            object O;
            tempData.TryGetValue(key, out O);
            return O == null ? null : JsonConvert.DeserializeObject<T>((String)O);
        }
     }
}

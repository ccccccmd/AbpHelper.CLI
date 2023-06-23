using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Humanizer;
using Microsoft.CodeAnalysis;
using Scriban.Runtime;
using Volo.Abp.Http;
using Volo.Abp.Reflection;
using MethodInfo = EasyAbp.AbpHelper.Core.Models.MethodInfo;
using PropertyInfo = EasyAbp.AbpHelper.Core.Models.PropertyInfo;

namespace EasyAbp.AbpHelper.Core.Generator
{
    public class AbpFunctions : ScriptObject
    {
        public static string KebabCase(string text)
        {
            var parts = text.Split('.')
                .Select(part => part.ToKebabCase());
            return string.Join('.', parts);
        }

        public static string Kebaberize(string text)
        {
            var parts = text.Split('.')
                .Select(part => part.Kebaberize());
            return string.Join('.', parts);
        }

        public static string CamelCase(string text)
        {
            var parts = text.Split('.')
                .Select(part => part.ToCamelCase());
            return string.Join('.', parts);
        }



        public static bool IsIgnoreProperty(PropertyInfo propertyInfo)
        {
            if (propertyInfo.Type == "Guid?" && propertyInfo.Name == "TenantId") return true;
            return false;
        }

        public static string GetHttpVerb(MethodInfo method)
        {
            var verbs = HttpMethodHelper.ConventionalPrefixes.Keys.Select(prefix => $"Http{prefix}").ToList();
            var verb = method.Attributes.FirstOrDefault(attr => verbs.Contains(attr, StringComparer.InvariantCultureIgnoreCase));
            if (verb == null)
            {
                verb = HttpMethodHelper.GetConventionalVerbForMethodName(method.Name);
                return $"Http{Char.ToUpper(verb[0])}{verb.Substring(1).ToLower()}";
            }
            else
            {
                return verb;
            }
        }

        /// <summary>
        /// Get the route string of the specified method
        /// This implementation refers to https://github.com/abpframework/abp/blob/88a32fd4a49b4204c608cafffbf419156148935a/framework/src/Volo.Abp.AspNetCore.Mvc/Volo/Abp/AspNetCore/Mvc/Conventions/AbpServiceConvention.cs#L300
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public static string GetRoute(MethodInfo method)
        {
            string url = String.Empty;

            //Add {id} path if needed
            var idParameterModel = method.Parameters.FirstOrDefault(param => param.Name == "id");
            if (idParameterModel != null)
            {
                var type = Type.GetType(idParameterModel.FullType);
                if (type != null && TypeHelper.IsPrimitiveExtended(type, includeEnums: true))
                {
                    url += "/{id}";
                }
                else
                {
                    // For the composite key, the type is null here, so we can not use reflection,
                    // use ParameterSymbol.Type.GetMembers instead
                    var properties = idParameterModel.ParameterSymbol.Type.GetMembers().OfType<IPropertySymbol>();

                    foreach (var property in properties)
                    {
                        url += "/{" + property.Name + "}";
                    }
                }
            }

            //Add action name if needed
            string actionNameInUrl = HttpMethodHelper.RemoveHttpMethodPrefix(method.Name, HttpMethodHelper.GetConventionalVerbForMethodName(method.Name))
                    .RemovePostFix("Async")
                ;
            if (!actionNameInUrl.IsNullOrEmpty())
            {
                url += $"/{actionNameInUrl.ToKebabCase()}";

                //Add secondary Id
                var secondaryIds = method.Parameters.Where(p => p.Name.EndsWith("Id", StringComparison.Ordinal)).ToList();
                if (secondaryIds.Count == 1)
                {
                    url += $"/{{{secondaryIds[0].Name}}}";
                }
            }

            return url.RemovePreFix("/");
        }

        public static List<MethodInfo> Intersect(List<MethodInfo> collection1, List<MethodInfo> collection2)
        {
            return collection1.Intersect(collection2).ToList();
        }


        public static string CamelCaseUi(string str)
        {
            if (string.IsNullOrEmpty(str) || !char.IsUpper(str[0]))
                return str;

            char[] chars = str.ToCharArray();
            int length = chars.Length;

            for (int i = 0; i < length; i++)
            {
                if (i == 1 && !char.IsUpper(chars[i]))
                    break;

                bool hasNext = i + 1 < length;

                if (i > 0 && hasNext && !char.IsUpper(chars[i + 1]))
                {
                    if (IsSeparator(chars[i + 1]))
                        chars[i] = char.ToLower(chars[i]);

                    break;
                }

                chars[i] = char.ToLower(chars[i]);
            }

            return new string(chars); 
            static bool IsSeparator(char str)
            {
                string pattern = @"[\s\u2000-\u206F\u2E00-\u2E7F\\'!""#$%&()*+,\-.\/:;<=>?@\[\]^_`{|}~]+";
                return Regex.IsMatch(str.ToString(), pattern);
            }
        }




    }
}
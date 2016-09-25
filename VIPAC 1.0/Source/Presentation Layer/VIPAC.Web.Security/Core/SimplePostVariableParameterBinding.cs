﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;

namespace VIPAC.Web.Security.Core
{
    public class SimplePostVariableParameterBinding : HttpParameterBinding
    {
        private const string MultipleBodyParameters = "MultipleBodyParameters";

        public SimplePostVariableParameterBinding(HttpParameterDescriptor descriptor)
            : base(descriptor)
        {
        }

        /// <summary>
        ///     Check for simple binding parameters in POST data. Bind POST
        ///     data as well as query string data
        /// </summary>
        /// <param name="metadataProvider"></param>
        /// <param name="actionContext"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task ExecuteBindingAsync(ModelMetadataProvider metadataProvider,
            HttpActionContext actionContext,
            CancellationToken cancellationToken)
        {
            string stringValue = null;

            NameValueCollection col = TryReadBody(actionContext.Request);
            if (col != null)
                stringValue = col[Descriptor.ParameterName];

            // try reading query string if we have no POST/PUT match
            if (stringValue == null)
            {
                IEnumerable<KeyValuePair<string, string>> query = actionContext.Request.GetQueryNameValuePairs();
                if (query != null)
                {
                    IEnumerable<KeyValuePair<string, string>> matches =
                        query.Where(kv => kv.Key.ToLower() == Descriptor.ParameterName.ToLower());
                    var keyValuePairs = matches as KeyValuePair<string, string>[] ?? matches.ToArray();
                    if (keyValuePairs.Count() > 0)
                        stringValue = keyValuePairs.First().Value;
                }
            }

            object value = StringToType(stringValue);

            // Set the binding result here
            SetValue(actionContext, value);

            // now, we can return a completed task with no result
            var tcs = new TaskCompletionSource<AsyncVoid>();
            tcs.SetResult(default(AsyncVoid));
            return tcs.Task;
        }


        /// <summary>
        ///     Method that implements parameter binding hookup to the global configuration object's
        ///     ParameterBindingRules collection delegate.
        ///     This routine filters based on POST/PUT method status and simple parameter
        ///     types.
        /// </summary>
        /// <example>
        ///     GlobalConfiguration.Configuration.
        ///     .ParameterBindingRules
        ///     .Insert(0,SimplePostVariableParameterBinding.HookupParameterBinding);
        /// </example>
        /// <param name="descriptor"></param>
        /// <returns></returns>
        public static HttpParameterBinding HookupParameterBinding(HttpParameterDescriptor descriptor)
        {
            Collection<HttpMethod> supportedMethods = descriptor.ActionDescriptor.SupportedHttpMethods;

            // Only apply this binder on POST and PUT operations
            if (supportedMethods.Contains(HttpMethod.Post) ||
                supportedMethods.Contains(HttpMethod.Put))
            {
                var supportedTypes = new[]
                {
                    typeof (string),
                    typeof (int),
                    typeof (decimal),
                    typeof (double),
                    typeof (bool),
                    typeof (DateTime),
                    typeof (byte[])
                };

                if (supportedTypes.Count(typ => typ == descriptor.ParameterType) > 0)
                    return new SimplePostVariableParameterBinding(descriptor);
            }

            return null;
        }


        private object StringToType(string stringValue)
        {
            object value;

            if (stringValue == null)
                value = null;
            else if (Descriptor.ParameterType == typeof(string))
                value = stringValue;
            else if (Descriptor.ParameterType == typeof(int))
                value = int.Parse(stringValue, CultureInfo.CurrentCulture);
            else if (Descriptor.ParameterType == typeof(Int32))
                value = Int32.Parse(stringValue, CultureInfo.CurrentCulture);
            else if (Descriptor.ParameterType == typeof(Int64))
                value = Int64.Parse(stringValue, CultureInfo.CurrentCulture);
            else if (Descriptor.ParameterType == typeof(decimal))
                value = decimal.Parse(stringValue, CultureInfo.CurrentCulture);
            else if (Descriptor.ParameterType == typeof(double))
                value = double.Parse(stringValue, CultureInfo.CurrentCulture);
            else if (Descriptor.ParameterType == typeof(DateTime))
                value = DateTime.Parse(stringValue, CultureInfo.CurrentCulture);
            else if (Descriptor.ParameterType == typeof(bool))
            {
                value = false;
                if (stringValue == "true" || stringValue == "on" || stringValue == "1")
                    value = true;
            }
            else
                value = stringValue;

            return value;
        }

        /// <summary>
        ///     Read and cache the request body
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private NameValueCollection TryReadBody(HttpRequestMessage request)
        {
            object result;

            // try to read out of cache first
            if (!request.Properties.TryGetValue(MultipleBodyParameters, out result))
            {
                MediaTypeHeaderValue contentType = request.Content.Headers.ContentType;

                // only read if there's content and it's form data
                if (contentType != null && contentType.MediaType == "application/x-www-form-urlencoded")
                {
                    // parsing the string like firstname=Hongmei&lastname=ASDASD            
                    result = request.Content.ReadAsFormDataAsync().Result;
                }

                request.Properties.Add(MultipleBodyParameters, result);
            }

            return result as NameValueCollection;
        }

        private struct AsyncVoid
        {
        }
    }
}
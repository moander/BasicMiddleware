// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.AspNetCore.Rewrite.Internal;
using Microsoft.AspNetCore.Http;

namespace Microsoft.AspNetCore.Rewrite
{
    /// <summary>
    /// The builder to a list of rules for <see cref="RewriteOptions"/> and <see cref="RewriteMiddleware"/>
    /// </summary>
    public static class RewriteOptionsExtensions
    {
        /// <summary>
        /// Adds a rule to the current rules.
        /// </summary>
        /// <param name="options">The UrlRewrite options.</param>
        /// <param name="rule">A rule to be added to the current rules.</param>
        /// <returns>The Rewrite options.</returns>
        public static RewriteOptions Add(this RewriteOptions options, Rule rule)
        {
            options.Rules.Add(rule);
            return options;
        }

        /// <summary>
        /// Adds a rule to the current rules.
        /// </summary>
        /// <param name="options">The Rewrite options.</param>
        /// <param name="applyRule">A Func that checks and applies the rule.</param>
        /// <returns></returns>
        public static RewriteOptions Add(this RewriteOptions options, Action<RewriteContext> applyRule)
        {
            options.Rules.Add(new DelegateRule(applyRule));
            return options;
        }

        /// <summary>
        /// Rewrites the path if the regex matches the HttpContext's PathString
        /// </summary>
        /// <param name="options">The Rewrite options.</param>
        /// <param name="regex">The regex string to compare with.</param>
        /// <param name="replacement">If the regex matches, what to replace HttpContext with.</param>
        /// <returns>The Rewrite options.</returns>
        public static RewriteOptions AddRewrite(this RewriteOptions options, string regex, string replacement)
        {
            return AddRewrite(options, regex, replacement, urlPrefix: new PathString("/"), stopProcessing: false);
        }


        /// <summary>
        /// Rewrites the path if the regex matches the HttpContext's PathString
        /// </summary>
        /// <param name="options">The Rewrite options.</param>
        /// <param name="regex">The regex string to compare with.</param>
        /// <param name="replacement">If the regex matches, what to replace the uri with.</param>
        /// <param name="urlPrefix">The prefix of the desired url.</param>
        /// <returns>The Rewrite options.</returns>
        public static RewriteOptions AddRewrite(this RewriteOptions options, string regex, string replacement, PathString urlPrefix)
        {
            options.Rules.Add(new RewriteRule(regex, replacement, urlPrefix, stopProcessing: false));
            return options;
        }

        /// <summary>
        /// Rewrites the path if the regex matches the HttpContext's PathString
        /// </summary>
        /// <param name="options">The Rewrite options.</param>
        /// <param name="regex">The regex string to compare with.</param>
        /// <param name="replacement">If the regex matches, what to replace the uri with.</param>
        /// <param name="urlPrefix">The prefix of the desired url.</param>
        /// <param name="stopProcessing">If the regex matches, conditionally stop processing other rules.</param>
        /// <returns>The Rewrite options.</returns>
        public static RewriteOptions AddRewrite(this RewriteOptions options, string regex, string replacement,  PathString urlPrefix, bool stopProcessing)
        {
            options.Rules.Add(new RewriteRule(regex, replacement, urlPrefix, stopProcessing));
            return options;
        }

        /// <summary>
        /// Redirect the request if the regex matches the HttpContext's PathString
        /// </summary>
        /// <param name="options">The Rewrite options.</param>
        /// <param name="regex">The regex string to compare with.</param>
        /// <param name="replacement">If the regex matches, what to replace the uri with.</param>
        /// <returns>The Rewrite options.</returns>
        public static RewriteOptions AddRedirect(this RewriteOptions options, string regex, string replacement)
        {
            return AddRedirect(options, regex, replacement, urlPrefix:new PathString("/"), statusCode: 302);
        }

        /// <summary>
        /// Redirect the request if the regex matches the HttpContext's PathString
        /// </summary>
        /// <param name="options">The Rewrite options.</param>
        /// <param name="regex">The regex string to compare with.</param>
        /// <param name="replacement">If the regex matches, what to replace the uri with.</param>
        /// <param name="urlPrefix">The prefix of the desired url.</param>
        /// <returns>The Rewrite options.</returns>
        public static RewriteOptions AddRedirect(this RewriteOptions options, string regex, string replacement, PathString urlPrefix)
        {
            return AddRedirect(options, regex, replacement, urlPrefix, statusCode: 302);
        }
        /// <summary>
        /// Redirect the request if the regex matches the HttpContext's PathString
        /// </summary>
        /// <param name="options">The Rewrite options.</param>
        /// <param name="regex">The regex string to compare with.</param>
        /// <param name="replacement">If the regex matches, what to replace the uri with.</param>
        /// <param name="urlPrefix">The prefix of the desired url></param>
        /// <param name="statusCode">The status code to add to the response.</param>
        /// <returns>The Rewrite options.</returns>
        public static RewriteOptions AddRedirect(this RewriteOptions options, string regex, string replacement, PathString urlPrefix, int statusCode)
        {
            options.Rules.Add(new RedirectRule(regex, replacement, urlPrefix, statusCode));
            return options;
        }

        /// <summary>
        /// Redirect a request to https if the incoming request is http, with returning a 301
        /// status code for permanently redirected.
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public static RewriteOptions AddRedirectToHttpsPermanent(this RewriteOptions options)
        {
            return AddRedirectToHttps(options, statusCode: 301, sslPort: null);
        }

        /// <summary>
        /// Redirect a request to https if the incoming request is http
        /// </summary>
        /// <param name="options">The Rewrite options.</param>
        public static RewriteOptions AddRedirectToHttps(this RewriteOptions options)
        {
            return AddRedirectToHttps(options, statusCode: 302, sslPort: null);
        }

        /// <summary>
        /// Redirect a request to https if the incoming request is http
        /// </summary>
        /// <param name="options">The Rewrite options.</param>
        /// <param name="statusCode">The status code to add to the response.</param>
        public static RewriteOptions AddRedirectToHttps(this RewriteOptions options, int statusCode)
        {
            return AddRedirectToHttps(options, statusCode, sslPort: null);
        }

        /// <summary>
        /// Redirect a request to https if the incoming request is http
        /// </summary>
        /// <param name="options">The Rewrite options.</param>
        /// <param name="statusCode">The status code to add to the response.</param>
        /// <param name="sslPort">The SSL port to add to the response.</param>
        public static RewriteOptions AddRedirectToHttps(this RewriteOptions options, int statusCode, int? sslPort)
        {
            options.Rules.Add(new RedirectToHttpsRule { StatusCode = statusCode, SSLPort = sslPort });
            return options;
        }
    }
}

// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite.Internal;

namespace Microsoft.AspNetCore.Rewrite
{
    /// <summary>
    /// Options for the <see cref="RewriteMiddleware"/> 
    /// </summary>
    public class RewriteOptions : IRewriteOptionsInfrastructure
    {
        public RewriteOptions()
        {
            ((IRewriteOptionsInfrastructure)this).DefaultConfigFileProvider = CreateConfigFileProvider();
        }

        protected virtual IFileProvider CreateConfigFileProvider()
        {
#if NET451
            var stringBasePath = AppDomain.CurrentDomain.GetData("APP_CONTEXT_BASE_DIRECTORY") as string
                ?? AppDomain.CurrentDomain.BaseDirectory
                ?? string.Empty;
            return new PhysicalFileProvider(stringBasePath);
#else
            return new PhysicalFileProvider(AppContext.BaseDirectory ?? string.Empty);
#endif
        }

        /// <summary>
        /// A list of <see cref="Rule"/> that will be applied in order upon a request.
        /// </summary>
        public IList<Rule> Rules { get; } = new List<Rule>();

        /// <summary>
        /// Gets and sets the File Provider for file and directory checks. Defaults to <see cref="IHostingEnvironment.WebRootFileProvider"/>
        /// </summary>
        public IFileProvider StaticFileProvider { get; set; }

        /// <summary>
        /// Defaults to a file provider pointing to System.AppContext.BaseDirectory
        /// </summary>
        IFileProvider IRewriteOptionsInfrastructure.DefaultConfigFileProvider { get; set; }
    }
}

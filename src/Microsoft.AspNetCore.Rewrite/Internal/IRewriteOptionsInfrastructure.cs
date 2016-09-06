// Copyright(c) .NET Foundation.All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.Extensions.FileProviders;

namespace Microsoft.AspNetCore.Rewrite.Internal
{
    /// <summary>
    /// Internal API for configuring incognito rewrite options
    /// </summary>
    public interface IRewriteOptionsInfrastructure
    {
        IFileProvider DefaultConfigFileProvider { get; set; }
    }
}
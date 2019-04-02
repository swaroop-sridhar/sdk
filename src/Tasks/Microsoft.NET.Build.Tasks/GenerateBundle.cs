﻿// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Build.Framework;
using Microsoft.NET.HostModel.Bundle;
using System.Collections.Generic;

namespace Microsoft.NET.Build.Tasks
{
    public class GenerateBundle : TaskBase
    {
        [Required]
        public ITaskItem[] FilesToBundle { get; set; }
        [Required]
        public string AppHostName { get; set; }
        [Required]
        public bool EmbedPDBs { get; set; }
        [Required]
        public string OutputDir { get; set; }
        [Required]
        public bool ShowDisgnosticOutput { get; set; }

        protected override void ExecuteCore()
        {
            Bundler bundler = new Bundler(AppHostName, OutputDir, EmbedPDBs, ShowDisgnosticOutput);
            var fileSpec = new List<FileSpec>(FilesToBundle.Length);

            foreach(var item in FilesToBundle)
            {
                fileSpec.Add(new FileSpec(sourcePath: item.ItemSpec,
                                          bundleRelativePath: item.GetMetadata("RelativePath")));
            }

            bundler.GenerateBundle(fileSpec);
        }
    }
}

// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: 
//          - Gmail:    rimuru.dev@gmail.com
//          - GitHub:   https://github.com/RimuruDev
//          - LinkedIn: https://www.linkedin.com/in/rimuru/
//          - GitHub Organizations: https://github.com/Rimuru-Dev
//
// **************************************************************** //

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Internal.Codebase.Utilities.Exceptions;
using UnityEngine;
using Object = UnityEngine.Object;

namespace RimuruDev.Internal.Codebase.Infrastructure.Services.Resource
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public sealed class ResourceLoaderServiceService : IResourceLoaderService
    {
        public TResource Load<TResource>(string path) where TResource : Object
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException(path);

            var resource = Resources.Load<TResource>(path);

            if (resource == null)
                throw new AssetLoadException(path);

            return resource;
        }

        public TResource[] LoadAll<TResource>(string path) where TResource : Object
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException(path);

            var resources = Resources.LoadAll<TResource>(path);

            if (resources == null)
                throw new AssetLoadException(path);

            if (resources.Any(resource => resource == null))
                throw new AssetLoadException(path);

            return resources;
        }
    }
}
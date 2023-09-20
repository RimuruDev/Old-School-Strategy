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
using RimuruDev.Internal.Codebase.Infrastructure.Services.Resource;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace RimuruDev.Internal.Codebase.Infrastructure.AssetManagement
{
    [SuppressMessage("ReSharper", "MethodOverloadWithOptionalParameter")]
    public sealed class AssetProvider : IAssetProvider
    {
        private readonly IResourceLoaderService resourceLoader;

        [Inject]
        public AssetProvider(IResourceLoaderService resourceLoader)
        {
            this.resourceLoader = resourceLoader;
        }

        public GameObject Instantiate(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException(path);

            var prefab = resourceLoader.Load<GameObject>(path);

            return Object.Instantiate(prefab);
        }

        public T Instantiate<T>(string path, Transform parent = null) where T : Object
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException(path);

            var prefab = resourceLoader.Load<T>(path);

            return Object.Instantiate(prefab, parent);
        }

        public GameObject Instantiate(string path, Transform parent = null)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException(path);

            var prefab = resourceLoader.Load<GameObject>(path);

            return Object.Instantiate(prefab, parent);
        }

        public GameObject Instantiate(GameObject prefab, Transform parent = null)
        {
            if (prefab == null)
                throw new NullReferenceException();

            return Object.Instantiate(prefab, parent);
        }

        public GameObject Instantiate(string path, Vector3 at)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException(path);

            var prefab = resourceLoader.Load<GameObject>(path);

            return Object.Instantiate(prefab, at, Quaternion.identity);
        }

        public GameObject Instantiate(GameObject prefab, Vector3 at, Quaternion rotation)
        {
            if (prefab == null)
                throw new NullReferenceException();

            return Object.Instantiate(prefab, at, rotation);
        }

        public TPrefab Instantiate<TPrefab>(TPrefab prefab, Transform parent = null) where TPrefab : Object
        {
            if (prefab == null)
                throw new ArgumentNullException(nameof(prefab));

            return Object.Instantiate(prefab, parent);
        }
    }
}
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


using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace RimuruDev.Internal.Codebase.Infrastructure.AssetManagement
{
    [SuppressMessage("ReSharper", "MethodOverloadWithOptionalParameter")]
    public interface IAssetProvider
    {
        public GameObject Instantiate(string path);
        public GameObject Instantiate(string path, Vector3 at);
        public T Instantiate<T>(string path, Transform parent = null) where T : Object;
        public GameObject Instantiate(string path, Transform parent = null);
        public GameObject Instantiate(GameObject prefab, Transform parent = null);
        public GameObject Instantiate(GameObject prefab, Vector3 at, Quaternion rotation);


        public TPrefab Instantiate<TPrefab>(TPrefab prefab, Transform parent = null) where TPrefab : Object;
    }
}
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
using System.Collections;
using RimuruDev.Internal.Codebase.Infrastructure.Services.CoroutineRunner;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace RimuruDev.Internal.Codebase.Infrastructure.Services.SceneLoader
{
    public sealed class SceneLoaderService : ISceneLoaderService
    {
        private readonly ICoroutineRunner coroutineRunner;

        [Inject]
        public SceneLoaderService(ICoroutineRunner coroutineRunner) => 
            this.coroutineRunner = coroutineRunner;

        public void LoadScene(string sceneName, Action onSceneLoadedCallback = null) => 
            coroutineRunner.StartCoroutine(LoadSceneCoroutine(sceneName, onSceneLoadedCallback));

        public string GetCurrentSceneName() => 
            SceneManager.GetActiveScene().name;

        private IEnumerator LoadSceneCoroutine(string sceneName, Action onSceneLoadedCallback)
        {
            if (GetCurrentSceneName() == sceneName)
            {
                onSceneLoadedCallback?.Invoke();
                yield break;
            }

            var loadSceneOperation = SceneManager.LoadSceneAsync(sceneName);
            while (!loadSceneOperation.isDone)
                yield return new WaitForEndOfFrame();

            onSceneLoadedCallback?.Invoke();
        }
    }
}
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

namespace RimuruDev.Internal.Codebase.Infrastructure.Services.SceneLoader
{
    public interface ISceneLoaderService
    {
        public void LoadScene(string sceneName, Action onSceneLoadedCallback = null);
        public string GetCurrentSceneName();
    }
}
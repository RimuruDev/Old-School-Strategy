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
using System.Collections.Generic;

namespace RimuruDev.Internal.Codebase.Infrastructure.Services.Cleanup
{
    public class CleanupService : ICleanupService
    {
        private readonly List<Action> cleanupActions = new();

        public void RegisterCleanupAction(Action cleanupAction) =>
            cleanupActions.Add(cleanupAction);

        public void UnregisterCleanupAction(Action cleanupAction) =>
            cleanupActions.Remove(cleanupAction);

        public void Cleanup()
        {
            foreach (var cleanupAction in cleanupActions)
                cleanupAction?.Invoke();

            cleanupActions.Clear();
        }

        public void Dispose() =>
            Cleanup();
    }
}
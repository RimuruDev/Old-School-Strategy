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

namespace RimuruDev.Internal.Codebase.Infrastructure.Services.Cleanup
{
    public interface ICleanupService : IDisposable
    {
        public void Cleanup();
        public void RegisterCleanupAction(Action cleanupAction);
        public void UnregisterCleanupAction(Action cleanupAction);
    }
}
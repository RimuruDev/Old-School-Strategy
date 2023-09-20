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

namespace RimuruDev.Internal.Codebase.Infrastructure.Services.ActionUpdater
{
    public interface IActionUpdaterService : IDisposable
    {
        public void Subscribe(Action updateable, UpdateType updateType);
        public void Unsubscribe(Action updateable, UpdateType updateType);
        public void FixedUpdate();
        public void Update();
        public void LateUpdate();
        public void Pause(bool pause);
    }
}
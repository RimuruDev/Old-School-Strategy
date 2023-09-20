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
using System.Linq;

namespace RimuruDev.Internal.Codebase.Infrastructure.Services.ActionUpdater
{
    public sealed class ActionUpdaterService : IActionUpdaterService
    {
        private readonly List<Action> fixedUpdateActionCache = new();
        private readonly List<Action> updateActionCache = new();
        private readonly List<Action> lateUpdateActionCache = new();
        private bool isPause;

        private event Action OnFixedUpdate;
        private event Action OnUpdate;
        private event Action OnLateUpdate;

        public void Subscribe(Action updateable, UpdateType updateType)
        {
            switch (updateType)
            {
                case UpdateType.FixedUpdate:
                    OnFixedUpdate += updateable;
                    fixedUpdateActionCache.Add(updateable);
                    break;
                case UpdateType.Update:
                    OnUpdate += updateable;
                    updateActionCache.Add(updateable);
                    break;
                case UpdateType.LateUpdate:
                    OnLateUpdate += updateable;
                    lateUpdateActionCache.Add(updateable);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(updateType), updateType, null);
            }
        }

        public void Unsubscribe(Action updateable, UpdateType updateType)
        {
            switch (updateType)
            {
                case UpdateType.FixedUpdate:
                    OnFixedUpdate -= updateable;
                    fixedUpdateActionCache.Remove(updateable);
                    break;
                case UpdateType.Update:
                    OnUpdate -= updateable;
                    updateActionCache.Remove(updateable);
                    break;
                case UpdateType.LateUpdate:
                    OnLateUpdate -= updateable;
                    lateUpdateActionCache.Remove(updateable);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(updateType), updateType, null);
            }
        }

        public void FixedUpdate()
        {
            if (isPause)
                return;

            OnFixedUpdate?.Invoke();
        }

        public void Update()
        {
            if (isPause)
                return;

            OnUpdate?.Invoke();
        }

        public void LateUpdate()
        {
            if (isPause)
                return;

            OnLateUpdate?.Invoke();
        }

        public void Pause(bool pause) =>
            isPause = pause;

        public void Dispose()
        {
            foreach (var action in fixedUpdateActionCache.Where(action => action != null))
                OnFixedUpdate -= action;

            foreach (var action in updateActionCache.Where(action => action != null))
                OnUpdate -= action;

            foreach (var action in lateUpdateActionCache.Where(action => action != null))
                OnLateUpdate -= action;

            fixedUpdateActionCache.Clear();
            updateActionCache.Clear();
            lateUpdateActionCache.Clear();
        }
    }
}
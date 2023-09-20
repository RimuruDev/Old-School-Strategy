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

using Zenject;
using UnityEngine;
using System.Diagnostics.CodeAnalysis;
using RimuruDev.Internal.Codebase.Infrastructure.Services.ActionUpdater;
using RimuruDev.Internal.Codebase.Infrastructure.GeneralGameStateMachine.States;
using RimuruDev.Internal.Codebase.Infrastructure.GeneralGameStateMachine.StateMachine;

namespace RimuruDev.Internal.Codebase.Infrastructure.Boot
{
    [SuppressMessage("ReSharper", "Unity.PerformanceCriticalCodeInvocation")]
    public sealed class GameBootstrapper : MonoBehaviour
    {
        private GameStateMachine gameStateMachine;
        private IActionUpdaterService actionUpdaterService;

        [Inject]
        public void Constructor(GameStateMachine stateMachine, IActionUpdaterService actionUpdater)
        {
            gameStateMachine = stateMachine;
            actionUpdaterService = actionUpdater;
        }

        private void Awake() =>
            Initialize();

        private void Initialize()
        {
            if (Exist())
            {
                Destroy(gameObject);
                return;
            }

            ApplyDontDestroyOnLoad();

            EnterBootstrapState();
        }

        private bool Exist()
        {
            var bootstrupper = FindObjectOfType<GameBootstrapper>();

            return bootstrupper is not null && bootstrupper != this;
        }

        private void ApplyDontDestroyOnLoad()
        {
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
        }

        private void EnterBootstrapState()
        {
            gameStateMachine.Init();
            gameStateMachine.EnterState<BootstrapState>();
        }

        // TODO: Move the (Update's) in class responsible solely for calling events, and only needed for event forwarding from the MonoBehaviour facade.
        private void FixedUpdate() =>
            actionUpdaterService.FixedUpdate();

        private void Update() =>
            actionUpdaterService.Update();

        private void LateUpdate() =>
            actionUpdaterService.LateUpdate();
    }
}
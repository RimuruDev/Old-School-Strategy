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
using System.Diagnostics.CodeAnalysis;
using RimuruDev.Internal.Codebase.Infrastructure.GeneralGameStateMachine.StateMachine;
using RimuruDev.Internal.Codebase.Infrastructure.GeneralGameStateMachine.States;

namespace RimuruDev.Internal.Codebase.Infrastructure.Installers
{
    [SuppressMessage("ReSharper", "Unity.PerformanceCriticalCodeInvocation")]
    public sealed class GameStateInstaller : MonoInstaller
    {
        public override void InstallBindings() =>
            BindGameStateMachine();

        private void BindGameStateMachine()
        {
            Container.Bind<BootstrapState>().AsSingle();
            Container.Bind<MainMenuState>().AsSingle();

            Container.Bind<GameStateMachine>().AsSingle();
        }
    }
}
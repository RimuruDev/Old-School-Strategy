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

using RimuruDev.Internal.Codebase.Infrastructure.GeneralGameStateMachine.Interfaces;
using RimuruDev.Internal.Codebase.Infrastructure.GeneralGameStateMachine.StateMachine;
using RimuruDev.Internal.Codebase.Infrastructure.Services.Curtain;
using RimuruDev.Internal.Codebase.Infrastructure.Services.StaticData;
using Zenject;

namespace RimuruDev.Internal.Codebase.Infrastructure.GeneralGameStateMachine.States
{
    public sealed class MainMenuState : IStateNext
    {
        private readonly IStaticDataService staticData;
        private readonly ICurtainService curtain;
        private GameStateMachine stateMachine;

        [Inject]
        public MainMenuState(IStaticDataService staticData, ICurtainService curtain)
        {
            this.staticData = staticData;
            this.curtain = curtain;
        }

        public void Init(GameStateMachine stateMachine) =>
            this.stateMachine = stateMachine;

        public void Enter()
        {
            HideCurtain();
        }

        public void Exit()
        {
        }

        private void HideCurtain()
        {
            var hideDelay = staticData.ForCurtain().HideDelay;
            curtain.HideCurtain(hideDelay);
        }
    }
}
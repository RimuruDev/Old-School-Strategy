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

using System.Collections;
using RimuruDev.Internal.Codebase.Infrastructure.GeneralGameStateMachine.Interfaces;
using RimuruDev.Internal.Codebase.Infrastructure.GeneralGameStateMachine.StateMachine;
using RimuruDev.Internal.Codebase.Infrastructure.Services.CoroutineRunner;
using RimuruDev.Internal.Codebase.Infrastructure.Services.Curtain;
using RimuruDev.Internal.Codebase.Infrastructure.Services.SceneLoader;
using RimuruDev.Internal.Codebase.Infrastructure.Services.StaticData;
using UnityEngine;
using Zenject;

namespace RimuruDev.Internal.Codebase.Infrastructure.GeneralGameStateMachine.States
{
    public sealed class MainMenuState : IStateNext
    {
        private readonly IStaticDataService staticData;
        private readonly ICurtainService curtain;
        private readonly ISceneLoaderService sceneLoder;
        private readonly ICoroutineRunner coroutineRunner;
        private GameStateMachine stateMachine;

        [Inject]
        public MainMenuState(IStaticDataService staticData, ICurtainService curtain, ISceneLoaderService sceneLoder,
            ICoroutineRunner coroutineRunner)
        {
            this.staticData = staticData;
            this.curtain = curtain;
            this.sceneLoder = sceneLoder;
            this.coroutineRunner = coroutineRunner;
        }

        public void Init(GameStateMachine stateMachine) =>
            this.stateMachine = stateMachine;

        public void Enter()
        {
            HideCurtain();

            // Build MainMenuUI
            // Get UI View -> Button -> Subscribe -> stateMachine.Enter -> LoadGameplayState -> Prepare Battle -> ...


            // Oly for test
            coroutineRunner.StartCoroutine(TempCooldon());
        }

        private IEnumerator TempCooldon()
        {
            yield return new WaitForSeconds(2);

            curtain.ShowCurtain();
            
            yield return new WaitForSeconds(1);

            sceneLoder.LoadScene(SceneName.Gameplay, HideCurtain);
        }

        public void Exit()
        {
            // Dispose all cache for main menu scene
        }

        private void HideCurtain()
        {
            var hideDelay = staticData.ForCurtain().HideDelay;
            curtain.HideCurtain(hideDelay);
        }
    }
}
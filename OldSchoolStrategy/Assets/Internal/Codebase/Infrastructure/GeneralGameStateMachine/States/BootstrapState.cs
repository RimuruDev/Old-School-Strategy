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

using DG.Tweening;
using RimuruDev.Internal.Codebase.Infrastructure.GeneralGameStateMachine.Interfaces;
using RimuruDev.Internal.Codebase.Infrastructure.GeneralGameStateMachine.StateMachine;
using RimuruDev.Internal.Codebase.Infrastructure.Services.Curtain;
using RimuruDev.Internal.Codebase.Infrastructure.Services.SceneLoader;
using RimuruDev.Internal.Codebase.Infrastructure.Services.StaticData;
using RimuruDev.Internal.Codebase.Runtime.Common.Settings;
using Zenject;

namespace RimuruDev.Internal.Codebase.Infrastructure.GeneralGameStateMachine.States
{
    public sealed class BootstrapState : IStateNext
    {
        private readonly ICurtainService curtain;
        private readonly IStaticDataService staticData;
        private readonly ISceneLoaderService sceneLoader;
        private GameStateMachine gameStateMachine;

        [Inject]
        public BootstrapState(
            ICurtainService curtain,
            IStaticDataService staticData,
            ISceneLoaderService sceneLoader)
        {
            this.staticData = staticData;
            this.sceneLoader = sceneLoader;
            this.curtain = curtain;
        }

        public void Init(GameStateMachine stateMachine) =>
            gameStateMachine = stateMachine;

        public void Enter()
        {
            PrepareServices();
            AppyGeneralSettings();
            LoadMainMenuScene();
        }

        public void Exit()
        {
        }

        private void PrepareServices()
        {
            DOTween.Init();

            staticData.Initialize();

            curtain.Init();
            curtain.ShowCurtain(false);
        }

        private static void AppyGeneralSettings()
        {
            var fps = new FPSSettings();
            fps.ApplyTargetFrameRate();
        }

        private void LoadMainMenuScene() =>
            sceneLoader.LoadScene(SceneName.Menu, OnSceneLoaded);

        private void OnSceneLoaded() =>
            gameStateMachine.EnterState<MainMenuState>();
    }
}
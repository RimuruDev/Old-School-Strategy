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

using System.Diagnostics.CodeAnalysis;
using RimuruDev.Internal.Codebase.Infrastructure.AssetManagement;
using RimuruDev.Internal.Codebase.Infrastructure.Services.ActionUpdater;
using RimuruDev.Internal.Codebase.Infrastructure.Services.CoroutineRunner;
using RimuruDev.Internal.Codebase.Infrastructure.Services.Curtain;
using RimuruDev.Internal.Codebase.Infrastructure.Services.Resource;
using RimuruDev.Internal.Codebase.Infrastructure.Services.SceneLoader;
using RimuruDev.Internal.Codebase.Infrastructure.Services.StaticData;
using RimuruDev.Internal.Codebase.Runtime.Battle;
using Zenject;

namespace RimuruDev.Internal.Codebase.Infrastructure.Installers
{
    [SuppressMessage("ReSharper", "Unity.PerformanceCriticalCodeInvocation")]
    public sealed class ServiceInstaller : MonoInstaller, ICoroutineRunner
    {
        public override void InstallBindings() =>
            BindServices();

        private void BindServices()
        {
            Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
            Container.Bind<ICoroutineRunner>().FromInstance(this).AsSingle();
            Container.Bind<ICurtainService>().To<CurtainService>().AsSingle();
            Container.Bind<IStaticDataService>().To<StaticDataService>().AsSingle();
            Container.Bind<ISceneLoaderService>().To<SceneLoaderService>().AsSingle();
            Container.Bind<IResourceLoaderService>().To<ResourceLoaderServiceService>().AsSingle();
            Container.Bind<IActionUpdaterService>().To<ActionUpdaterService>().AsSingle();

            Container.Bind<IBattleObserver>().To<BattleObserver>().AsSingle();
        }
    }
}
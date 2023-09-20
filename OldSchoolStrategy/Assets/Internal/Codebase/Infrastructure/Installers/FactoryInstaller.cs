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
using RimuruDev.Internal.Codebase.Infrastructure.Factory.UI;
using Zenject;

namespace RimuruDev.Internal.Codebase.Infrastructure.Installers
{
    [SuppressMessage("ReSharper", "Unity.PerformanceCriticalCodeInvocation")]
    public sealed class FactoryInstaller : MonoInstaller
    {
        public override void InstallBindings() =>
            BindFactory();

        private void BindFactory()
        {
            Container.Bind<IUIFactory>().To<UIFactory>().AsSingle();
        }
    }
}
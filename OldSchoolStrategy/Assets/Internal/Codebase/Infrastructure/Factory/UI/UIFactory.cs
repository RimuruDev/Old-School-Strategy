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
using RimuruDev.Internal.Codebase.Runtime.Common.Curtain.View;
using RimuruDev.Internal.Codebase.Infrastructure.AssetManagement;
using RimuruDev.Internal.Codebase.Infrastructure.Services.StaticData;

namespace RimuruDev.Internal.Codebase.Infrastructure.Factory.UI
{
    public sealed class UIFactory : IUIFactory
    {
        private readonly IAssetProvider assetProvider;
        private readonly IStaticDataService staticData;

        [Inject]
        public UIFactory(IAssetProvider assetProvider, IStaticDataService staticData)
        {
            this.assetProvider = assetProvider;
            this.staticData = staticData;
        }

        public CurtainView CreateCurtain()
        {
            var config = staticData.ForCurtain();
            var instance = assetProvider.Instantiate(config.CurtainView.gameObject);

            var view = instance.GetComponent<CurtainView>();
            view.Constructor(config.AnimationDuration);

            return view;
        }
    }
}
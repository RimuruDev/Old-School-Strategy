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

using NaughtyAttributes;
using RimuruDev.Internal.Codebase.Runtime.Common.Curtain.View;
using UnityEngine;

namespace RimuruDev.Internal.Codebase.Runtime.Common.Curtain.Configs
{
    [CreateAssetMenu(menuName = "StaticData/Common/Create Curtain Config", fileName = "CurtainConfig", order = 0)]
    public sealed class CurtainConfig : ScriptableObject
    {
        [field: SerializeField] public CurtainView CurtainView { get; private set; }

        [field: SerializeField, ShowAssetPreview(128, 128)]
        public Sprite Icon { get; private set; }

        [field: SerializeField, Range(0f, 10f), Space(20)]
        public float HideDelay { get; private set; } = 1.3f;

        [field: SerializeField, Range(0f, 10f)]
        public float AnimationDuration { get; private set; } = 1.5f;
    }
}
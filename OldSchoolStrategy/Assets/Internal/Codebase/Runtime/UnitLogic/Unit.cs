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

using UnityEngine;

namespace RimuruDev.Internal.Codebase.Runtime.UnitLogic
{
    // TODO: Add SO <TUnitConfig>
    [SelectionBase]
    [DisallowMultipleComponent]
    public abstract class Unit : MonoBehaviour
    {
        [field: Header("Base Unit Settings")]
        [field: SerializeField, Min(0)] public float Health { get; private set; }
        [field: SerializeField] public Vector2Int Move { get; private set; }
        [field: SerializeField, Min(0)] public float Initiative { get; private set; }
    }
}
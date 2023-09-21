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
    // TODO: Add SO Unit<TUnitConfig> : MonoBehaviour
    [SelectionBase]
    [DisallowMultipleComponent]
    public abstract class Unit : MonoBehaviour, IUnit
    {
        // Health, Move, Initiative
    }

    public interface IUnit { }
}
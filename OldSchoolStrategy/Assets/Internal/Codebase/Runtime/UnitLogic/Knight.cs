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

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RimuruDev.Internal.Codebase.Runtime.UnitLogic
{
    public sealed class Knight : Unit
    {
        public List<SpriteRenderer> spriteRenderers;

        private void OnMouseDown()
        {
            spriteRenderers.ForEach(current => current.color = Color.yellow);
        }

        private void OnMouseEnter()
        {
            spriteRenderers.ForEach(current => current.color = Color.blue);
        }

        private void OnMouseExit()
        {
            spriteRenderers.ForEach(current => current.color = Color.white);
        }

        [System.Diagnostics.Conditional("DEBUG")]
        private void OnValidate()
        {
            spriteRenderers = new List<SpriteRenderer>();

            GetComponentsInChildren<SpriteRenderer>()
                .Where(sr => sr != null)
                .ToList()
                .ForEach(sr => { spriteRenderers.Add(sr); });
        }
    }
}
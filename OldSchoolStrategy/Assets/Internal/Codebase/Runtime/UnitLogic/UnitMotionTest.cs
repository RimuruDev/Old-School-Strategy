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
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using NaughtyAttributes;

namespace RimuruDev.Internal.Codebase.Runtime.UnitLogic
{
    public class UnitMotionTest : Unit
    {
        [SerializeField, ReadOnly] private Vector2 movementInput;
        [SerializeField, ReadOnly] private Vector3 direction;
        [SerializeField, ReadOnly] private bool hasMove;

        private void Update()
        {
            var horizontalInput = Input.GetAxis("Horizontal");
            var verticalInput = Input.GetAxis("Vertical");

            movementInput = new Vector2(horizontalInput, verticalInput);

            if (movementInput.x == 0)
            {
                hasMove = false;
            }
            else if (movementInput.x != 0 && !hasMove)
            {
                hasMove = true;
                GetMovementDirection();
            }
        }

        private void GetMovementDirection()
        {
            // Matching position
            switch (movementInput.x)
            {
                case < 0:
                    direction = movementInput.y switch
                    {
                        > 0 => new Vector3(-0.5f, 0.5f),
                        < 0 => new Vector3(-0.5f, -0.5f),
                        _ => new Vector3(-1f, 0f, 0f)
                    };

                    transform.position += direction;
                    break;
                case > 0:
                    direction = movementInput.y switch
                    {
                        > 0 => new Vector3(0.5f, 0.5f),
                        < 0 => new Vector3(0.5f, -0.5f),
                        _ => new Vector3(1f, 0f, 0f)
                    };

                    transform.position += direction;
                    break;
            }
        }
    }
}
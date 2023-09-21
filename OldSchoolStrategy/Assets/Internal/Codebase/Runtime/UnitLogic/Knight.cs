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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using RimuruDev.External.GridLogic.Pathfinding;
using RimuruDev.Internal.Codebase.Runtime.HexagonGridLogic;
using UnityEngine;

namespace RimuruDev.Internal.Codebase.Runtime.UnitLogic
{
    // Only for Test
    public sealed class Knight : Unit
    {
        [SerializeField] private float movementSpeed = 5f;
        [HideInInspector] public HexagonGridHolder HexagonGridHolder;
        [HideInInspector] public List<SpriteRenderer> spriteRenderers;

        #region Unity Callback

        private void OnEnable() =>
            HexagonGridHolder.OnPathFinde += TestMove;

        private void OnDisable() =>
            HexagonGridHolder.OnPathFinde -= TestMove;

        private void OnMouseDown() =>
            SetColor(Color.yellow);

        private void OnMouseEnter() =>
            SetColor(Color.blue);

        private void OnMouseExit() =>
            SetColor(Color.white);

        [System.Diagnostics.Conditional("DEBUG")]
        private void OnValidate()
        {
            spriteRenderers = new List<SpriteRenderer>();

            GetComponentsInChildren<SpriteRenderer>()
                .Where(sr => sr != null)
                .ToList()
                .ForEach(sr => { spriteRenderers.Add(sr); });

            if (HexagonGridHolder == null)
                HexagonGridHolder = FindObjectOfType<HexagonGridHolder>();
        }

        #endregion

        public void StartMovement(IEnumerable<IPathfindingNode> pathList)
        {
            if (pathList == null)
                return;

            StartCoroutine(MoveAlongPath(pathList));
        }

        private IEnumerator MoveAlongPath(IEnumerable<IPathfindingNode> path) =>
            path
                .Select(node => node.position)
                .Select(targetPosition => transform.DOMove(targetPosition, movementSpeed).WaitForCompletion())
                .GetEnumerator();

        private static void TestMove(IEnumerable<IPathfindingNode> paths)
        {
            foreach (var path in paths) 
                print($"path: {path.position}");
        }

        private void SetColor(Color color) =>
            spriteRenderers.ForEach(current => current.color = color);
    }
}
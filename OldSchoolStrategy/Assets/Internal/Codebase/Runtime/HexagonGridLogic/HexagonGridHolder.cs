using System;
using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using RimuruDev.External.GridLogic.Grids;
using RimuruDev.External.GridLogic.Pathfinding;

namespace RimuruDev.Internal.Codebase.Runtime.HexagonGridLogic
{
    public sealed class HexagonGridHolder : MonoBehaviour
    {
        public event Action<IEnumerable<IPathfindingNode>> OnPathFinde;

        public int width = 5;
        public int height = 5;
        public float cellSize = 10f;
        public bool doDebug = true;
        public GridAxis gridAxis = GridAxis.XZ;
        public Vector2 gridOffset = Vector2.zero;
        public Camera cameraRenderer;

        private HexagonGrid<GridCell> grid;
        private Pathfinder pathfinder;

        private void Start()
        {
            pathfinder = Pathfinder.instance;

            grid = new HexagonGrid<GridCell>(
                gridAxis, gridOffset, width, height, GenerateNewGridCell, cellSize,
                doDebug, 40, 0.075f);

            grid.GenerateMesh(GetComponent<MeshFilter>(), GetComponent<MeshCollider>());
        }

        private void Update()
        {
            if (!Input.GetMouseButtonDown(0))
                return;

            var cell = GridUtils.GetGridValueAtMousePos(grid, cameraRenderer, out var foundValue);

            if (!foundValue)
                return;

            grid.GetXY(cell, out var x, out var y);
            PathfindTo(x, y);
        }

        private void PathfindTo(int x, int y)
        {
            if (!pathfinder)
            {
                Debug.LogError("No pathfinder");

                return;
            }

            var graph = PathfinderUtils.ConvertGridToGraph(grid);

            var startNode = graph.GetValue(0, 0);
            var endNode = graph.GetValue(x, y);

            var debugDuration = 1f;

            var path = pathfinder.Init(graph, startNode, endNode, true, debugDuration);

            OnPathFinde?.Invoke(path);
        }

        private static GridCell GenerateNewGridCell(Grid<GridCell> source, int x, int y)
        {
            var movementDifficulty = Random.Range(1, 2);

            return new GridCell(source, x, y, movementDifficulty);
        }
    }
}
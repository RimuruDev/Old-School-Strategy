using NaughtyAttributes;
using RimuruDev.External.GridLogic.Grids;
using RimuruDev.External.GridLogic.Pathfinding;
using UnityEngine;

namespace RimuruDev.Internal.Codebase.Runtime.HexagonGridLogic
{
    public sealed class HexagonGridHolder : MonoBehaviour
    {
        // Remember grids start at 0,0 so a width and height of 5 will result in the top right corner being 4,4 on the grid.
        public int width = 5;
        public int height = 5;
        public float cellSize = 10f;
        public bool doDebug = true;
        public GridAxis gridAxis = GridAxis.XZ;
        public Vector2 gridOffset = Vector2.zero;
        public Camera cameraRenderer;
        public Vector3Int currentTestPositionForRay;

        private HexagonGrid<GridCell> grid;
        private Pathfinder pathfinder;

        private void Start()
        {
            pathfinder = Pathfinder.instance;

            // When Initializing a grid you need to provide it with a function that returns an instance of whatever type
            // of object the grid holds.

            grid = new HexagonGrid<GridCell>(gridAxis, gridOffset, width, height, GenerateNewGridCell, cellSize,
                doDebug, 40, 0.075f);

            // The key feature of a Hexagon grid, is that it can generate it's own mesh
            // I plan to add more features to this mesh generation (from following CatLikeCoding's course, highly recommended!)
            grid.GenerateMesh(GetComponent<MeshFilter>(), GetComponent<MeshCollider>());
        }

        public void PathfindTo(int x, int y)
        {
            if (!pathfinder)
            {
                Debug.LogError("No pathfinder");
                return;
            }

            // The pathfinder takes an Grid of IPathfindingNodes.
            // You can convert any grid of objects that inherit from IPathfindingNode to a
            // Grid of IPathfindingNodes via the PathfindingUtils class

            var graph = PathfinderUtils.ConvertGridToGraph(grid);

            var startNode = graph.GetValue(0,0); // Selected Unit
            var endNode = graph.GetValue(x, y); // Target Position

            var debugDuration = 1f;

            var path = pathfinder.Init(graph, startNode, endNode, true, debugDuration);

            // The pathfinder returns the path to the goal node in order from start to goal.
            // You can reverse engineer it with graph.GetWorldPositionCentered() to move something along it
            // I recommend DoTween for this

            // var pos = graph.GetWorldPosition(5, 5);
            // print($"pos(5:5) = {result}");
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

        private static GridCell GenerateNewGridCell(Grid<GridCell> source, int x, int y)
        {
            var movementDifficulty = Random.Range(1, 2);

            return new GridCell(source, x, y, movementDifficulty);
        }
    }
}
using System.Linq;
using System.Collections.Generic;
using RimuruDev.External.GridLogic.Grids;

namespace RimuruDev.External.GridLogic.Pathfinding
{
    public static class PathfinderUtils
    {
        public static IEnumerable<IPathfindingNode> ToIPathfindingNodes<T>(List<T> nodes)
        {
            var newList = new List<IPathfindingNode>();

            foreach (var node in nodes)
            {
                if (node is IPathfindingNode pathfindingNode)
                {
                    newList.Add(pathfindingNode);
                }
            }

            return newList;
        }

        public static List<IPathfindingNode> FilterTraversable(IEnumerable<IPathfindingNode> nodes) =>
            nodes.Where(node => node.traversable).ToList();

        public static Grid<IPathfindingNode> ConvertGridToGraph<T>(SquareGrid<T> grid) where T : IPathfindingNode
        {
            Grid<IPathfindingNode> graph = new SquareGrid<IPathfindingNode>(grid.Gridaxis, grid.WorldSpaceOffset,
                grid.Width, grid.Height, (_, _, _) => null, grid.WorldSpaceCellSize, grid.ConnectDiagonally);

            for (var x = 0; x < grid.Width; x++)
            {
                for (var y = 0; y < grid.Height; y++)
                {
                    graph.Map[x, y] = grid.GetValue(x, y);
                }
            }

            return graph;
        }

        public static Grid<IPathfindingNode> ConvertGridToGraph<T>(HexagonGrid<T> grid) where T : IPathfindingNode
        {
            Grid<IPathfindingNode> graph = new HexagonGrid<IPathfindingNode>(grid.Gridaxis, grid.WorldSpaceOffset,
                grid.Width, grid.Height, (_, _, _) => null, grid.WorldSpaceCellSize);

            for (var x = 0; x < grid.Width; x++)
            {
                for (var y = 0; y < grid.Height; y++)
                {
                    graph.Map[x, y] = grid.GetValue(x, y);
                }
            }

            return graph;
        }
    }
}
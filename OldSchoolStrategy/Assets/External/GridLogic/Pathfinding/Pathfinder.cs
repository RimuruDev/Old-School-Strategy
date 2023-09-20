using System.Collections.Generic;
using System.Linq;
using RimuruDev.External.GridLogic.Grids;
using RimuruDev.External.GridLogic.Utils;
using UnityEngine;

namespace RimuruDev.External.GridLogic.Pathfinding
{
    public class Pathfinder : BasicSingleton<Pathfinder>
    {
        public Mode mode = Mode.AStar;

        [Header("Debug")] public Color DEBUG_Colour = Color.red;

        private IPathfindingNode startNode;
        private IPathfindingNode goalNode;

        private Grid<IPathfindingNode> graph;

        private PriorityQueue<IPathfindingNode> frontierNodes;
        private List<IPathfindingNode> exploredNodes;
        private List<IPathfindingNode> pathNodes;

        private bool isComplete;
        private int iterations;

        public enum Mode
        {
            BreadthFirstSearch,
            Dijkstra,
            GreedyBestFirstSearch,
            AStar
        }

        public IEnumerable<IPathfindingNode> Init(Grid<IPathfindingNode> graph, IPathfindingNode startNode,
            IPathfindingNode goalNode, bool DEBUG_showPath = false, float DEBUG_duration = 10f)
        {
            if (graph == null)
            {
                Debug.LogWarning("Pathfinder Error: Graph is null");
                return null;
            }

            if (startNode == null)
            {
                Debug.LogWarning("Pathfinder Error: Start node is null");
                return null;
            }

            if (goalNode == null)
            {
                Debug.LogWarning("Pathfinder Error: Goal node is null");
                return null;
            }

            // Had to remove this line as I found checking if the startNode is traversable
            // got in the way with creating a cell inhabited system and I figure there is no reason to check it anyway
            // considering that if the start node is not traversable, you've probably done something wrong already and want the player out of there

            //if (!startNode.traversable) {Debug.LogWarning("Pathfinder Error: Start node is not traversable"); return null; }
            if (!goalNode.traversable)
            {
                Debug.LogWarning("Pathfinder Error: Goal node is not traversable");
                return null;
            }

            if (startNode == goalNode)
            {
                Debug.LogWarning("Pathfinder Error: Startnode == goal node");
                return null;
            }

            this.graph = graph;
            this.startNode = startNode;
            this.goalNode = goalNode;

            frontierNodes = new PriorityQueue<IPathfindingNode>();
            frontierNodes.Enqueue(startNode);

            exploredNodes = new List<IPathfindingNode>();
            pathNodes = new List<IPathfindingNode>();

            for (var x = 0; x < graph.Width; x++)
            {
                for (var y = 0; y < graph.Height; y++)
                {
                    graph.Map[x, y].Reset();
                }
            }

            isComplete = false;
            iterations = 0;

            startNode.distanceTravelled = 0;

            SearchRoutine();

            if (!DEBUG_showPath)
                return pathNodes;

            {
                var previous = pathNodes[0];

                foreach (var cell in pathNodes.Where(cell => cell != previous))
                {
                    graph.GetXY(previous, out var x, out var y);
                    graph.GetXY(cell, out var _x, out var _y);

                    previous = cell;

                    Debug.DrawLine(graph.GetWorldPosition(x, y, true), graph.GetWorldPosition(_x, _y, true),
                        DEBUG_Colour, DEBUG_duration, false);
                }
            }

            return pathNodes;
        }

        public void SearchRoutine()
        {
            while (!isComplete)
            {
                if (frontierNodes.Count > 0)
                {
                    var currentNode = frontierNodes.Dequeue();
                    iterations++;

                    if (!exploredNodes.Contains(currentNode))
                        exploredNodes.Add(currentNode);

                    switch (mode)
                    {
                        case Mode.BreadthFirstSearch:
                            ExpandFrontierBFS(currentNode);
                            break;
                        case Mode.Dijkstra:
                            ExpandFrontierDijkstra(currentNode);
                            break;
                        case Mode.GreedyBestFirstSearch:
                            ExpandFrontierGBFS(currentNode);
                            break;
                        case Mode.AStar:
                            ExpandFrontierAStar(currentNode);
                            break;
                    }

                    if (!frontierNodes.Contains(goalNode))
                        continue;

                    pathNodes = GetPathNodes(goalNode);

                    isComplete = true;
                }
                else
                    isComplete = true;
            }
            //Debug.Log("Time Elapsed: " + (Time.realtimeSinceStartup - timeStart).ToString() + " seconds");
        }

        #region ExpandFrontiers

        private void ExpandFrontierBFS(IPathfindingNode node)
        {
            if (node == null)
                return;

            foreach (var n in node.neighbours)
            {
                if (exploredNodes.Contains(n) || frontierNodes.Contains(n))
                    continue;

                var distance = graph.GetCellDistance(node.xIndex, node.yIndex, n.xIndex, n.yIndex);

                var newDistanceTravelled = distance + node.distanceTravelled + n.movementDifficulty;

                n.distanceTravelled = newDistanceTravelled;

                n.previous = node;
                n.priority = exploredNodes.Count;
                frontierNodes.Enqueue(n);
            }
        }

        private void ExpandFrontierDijkstra(IPathfindingNode node)
        {
            if (node == null)
                return;

            foreach (var n in node.neighbours)
            {
                if (exploredNodes.Contains(n))
                {
                    continue;
                }

                var distance = graph.GetCellDistance(node.xIndex, node.yIndex, n.xIndex, n.yIndex);
                var newDistanceTravelled = distance + node.distanceTravelled + n.movementDifficulty;

                if (float.IsPositiveInfinity(n.distanceTravelled) || newDistanceTravelled < n.distanceTravelled)
                {
                    n.previous = node;
                    n.distanceTravelled = newDistanceTravelled;
                }

                if (frontierNodes.Contains(n))
                    continue;

                n.priority = n.distanceTravelled;
                frontierNodes.Enqueue(n);
            }
        }

        public void ExpandFrontierGBFS(IPathfindingNode node)
        {
            if (node == null)
                return;

            foreach (var n in node.neighbours)
            {
                if (exploredNodes.Contains(n) || frontierNodes.Contains(n))
                {
                    continue;
                }

                var distance = graph.GetCellDistance(node.xIndex, node.yIndex, n.xIndex, n.yIndex);
                var newDistanceTravelled = distance + node.distanceTravelled + n.movementDifficulty;

                n.distanceTravelled = newDistanceTravelled;

                n.previous = node;
                n.priority = graph.GetCellDistance(node.xIndex, node.yIndex, goalNode.xIndex, goalNode.yIndex);
                frontierNodes.Enqueue(n);
            }
        }

        public void ExpandFrontierAStar(IPathfindingNode node)
        {
            if (node == null)
                return;

            foreach (var n in node.neighbours)
            {
                if (exploredNodes.Contains(n))
                    continue;

                var distance = graph.GetCellDistance(node.xIndex, node.yIndex, n.xIndex, n.yIndex);
                var newDistanceTravelled = distance + node.distanceTravelled + n.movementDifficulty;

                if (float.IsPositiveInfinity(n.distanceTravelled) || newDistanceTravelled < n.distanceTravelled)
                {
                    n.previous = node;
                    n.distanceTravelled = newDistanceTravelled;
                }

                if (frontierNodes.Contains(n))
                    continue;

                var distanceToGoal = graph.GetCellDistance(node.xIndex, node.yIndex, goalNode.xIndex, goalNode.yIndex);
                n.priority = n.distanceTravelled + distanceToGoal;
                frontierNodes.Enqueue(n);
            }
        }

        #endregion

        private static List<IPathfindingNode> GetPathNodes(IPathfindingNode endNode)
        {
            var path = new List<IPathfindingNode>();

            if (endNode == null)
                return path;

            path.Add(endNode);

            var currentNode = endNode.previous;

            while (currentNode != null)
            {
                path.Insert(0, currentNode);
                currentNode = currentNode.previous;
            }

            return path;
        }
    }
}
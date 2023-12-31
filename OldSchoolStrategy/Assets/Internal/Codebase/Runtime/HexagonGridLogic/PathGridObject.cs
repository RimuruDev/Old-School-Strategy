using UnityEngine;
using System.Collections.Generic;
using RimuruDev.External.GridLogic.Grids;
using RimuruDev.External.GridLogic.Pathfinding;

namespace RimuruDev.Internal.Codebase.Runtime.HexagonGridLogic
{
    public abstract class PathGridObject<T> : IPathfindingNode, System.IEquatable<PathGridObject<T>>
        where T : PathGridObject<T>, System.IEquatable<T>
    {
        public Grid<T> sourceGrid { get; set; }

        public int xIndex { get; set; }
        public int yIndex { get; set; }
        public Vector3 position { get; set; }
        public float movementDifficulty { get; set; }

        public virtual bool traversable =>
            movementDifficulty != 0;

        public float distanceTravelled { get; set; } = Mathf.Infinity;
        public float priority { get; set; }
        public IPathfindingNode previous { get; set; }

        public PathGridObject(Grid<T> sourceGrid, int xIndex, int yIndex, float movementDifficulty)
        {
            this.sourceGrid = sourceGrid;
            this.xIndex = xIndex;
            this.yIndex = yIndex;
            this.movementDifficulty = movementDifficulty;
            position = sourceGrid.GetWorldPosition(xIndex, yIndex);
        }

        public List<IPathfindingNode> neighbours =>
            PathfinderUtils.FilterTraversable(
                PathfinderUtils.ToIPathfindingNodes(sourceGrid.GetCellNeighbors(xIndex, yIndex)));

        public void Reset()
        {
            previous = null;
            distanceTravelled = Mathf.Infinity;
        }

        public int CompareTo(IPathfindingNode other)
        {
            if (priority < other.priority)
                return -1;

            return priority > other.priority ? 1 : 0;
        }

        public bool Equals(PathGridObject<T> other)
        {
            other.CheckNullArgumentException();

            return xIndex == other!.xIndex && yIndex == other.yIndex;
        }

        public bool Equals(IPathfindingNode other)
        {
            other.CheckNullArgumentException();

            return xIndex == other!.xIndex && yIndex == other.yIndex;
        }

        public override string ToString() =>
            "M Dif: " + movementDifficulty + "\nPos: (" + xIndex + "," + yIndex + ")";
    }
}
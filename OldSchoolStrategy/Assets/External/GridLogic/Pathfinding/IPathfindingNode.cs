using System.Collections.Generic;
using UnityEngine;

namespace RimuruDev.External.GridLogic.Pathfinding
{
    public interface IPathfindingNode : System.IComparable<IPathfindingNode>, System.IEquatable<IPathfindingNode>
    {
        int xIndex { get; set; }
        int yIndex { get; set; }
        Vector3 position { get; set; }

        float movementDifficulty { get; set; }
        bool traversable { get; }
        float distanceTravelled { get; set; }
        float priority { get; set; }
        IPathfindingNode previous { get; set; }
        List<IPathfindingNode> neighbours { get; }

        void Reset();
    }
}
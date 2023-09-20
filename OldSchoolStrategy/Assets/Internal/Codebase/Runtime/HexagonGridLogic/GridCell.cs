using System;
using RimuruDev.External.GridLogic.Grids;

namespace RimuruDev.Internal.Codebase.Runtime.HexagonGridLogic
{
    public sealed class GridCell : PathGridObject<GridCell>, IEquatable<GridCell>
    {
        public GridCell(Grid<GridCell> sourceGrid, int xIndex, int yIndex, float movementDifficulty)
            : base(sourceGrid, xIndex, yIndex, movementDifficulty)
        {
        }

        public bool Equals(GridCell other)
        {
            other.CheckNullArgumentException();

            return xIndex == other!.xIndex && yIndex == other.yIndex;
        }
    }
}
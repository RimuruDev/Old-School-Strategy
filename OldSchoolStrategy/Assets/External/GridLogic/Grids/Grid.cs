using UnityEngine;
using System.Collections.Generic;

namespace RimuruDev.External.GridLogic.Grids
{
    public abstract class Grid<T>
    {
        public event System.EventHandler<GridValueChangedEventArgs> GridValueChanged;

        public Vector3 WorldSpaceOffset { get; set; }
        public int Width { get; protected set; }
        public int Height { get; protected set; }

        public T[,] Map { get; protected set; }

        public float WorldSpaceCellSize { get; set; }

        public GridAxis Gridaxis { get; protected set; }

        public Grid(GridAxis gridAxis, Vector3 offset, int width, int height, System.Func<Grid<T>, int, int, T> createGridObjectFunc, float cellSize)
        {
            Gridaxis = gridAxis;
            WorldSpaceOffset = offset;
            Width = width;
            Height = height;
            WorldSpaceCellSize = cellSize;

            Map = new T[width, height];

            for (var x = 0; x < Map.GetLength(0); x++)
            {
                for (var y = 0; y < Map.GetLength(1); y++)
                {
                    var newObject = createGridObjectFunc(this, x, y);
                    
                    Map[x, y] = newObject;
                }
            }
        }

        public virtual float GetCellDistance(int x1, int y1, int x2, int y2) =>
            0f;

        public virtual void SetValue(int x, int y, T value)
        {
            if (!WithinBounds(x, y))
            {
                throw new System.ArgumentException($"x:{x}, y:{y} are out of bounds");
            }

            Map[x, y] = value;
            OnValueChanged(x, y);
        }

        public virtual void SetValue(Vector3 position, T value)
        {
            int x, y;
            GetXY(position, out x, out y);

            if (!WithinBounds(x, y))
                throw new System.ArgumentException($"{x}, {y} does not exist within Grid");

            SetValue(x, y, value);
        }

        public virtual T GetValue(int x, int y)
        {
            if (WithinBounds(x, y))
            {
                return Map[x, y];
            }

            throw new System.ArgumentException($"{x}, {y} does not exist within Grid");
        }

        public bool WithinBounds(int x, int y)
        {
            if ((x < 0f || y < 0f) || (x > Map.GetLength(0) - 1 || y > Map.GetLength(1) - 1))
            {
                return false;
            }

            return true;
        }

        public virtual void GetXY(Vector3 worldPosition, out int x, out int y)
        {
            worldPosition.x -= WorldSpaceOffset.x;
            worldPosition.z -= WorldSpaceOffset.y;
            x = Mathf.FloorToInt(worldPosition.x / WorldSpaceCellSize);
            y = Mathf.FloorToInt(worldPosition.z / WorldSpaceCellSize);
        }

        public virtual void GetXY(T cell, out int x, out int y)
        {
            for (int _x = 0; _x < Map.GetLength(0); _x++)
            {
                for (int _y = 0; _y < Map.GetLength(1); _y++)
                {
                    if (cell.Equals(Map[_x, _y]))
                    {
                        x = _x;
                        y = _y;
                        return;
                    }
                }
            }

            x = -1;
            y = -1;
        }

        public virtual List<T> GetCellNeighbors(int x, int y, List<Vector2Int> directions = null)
        {
            if (directions == null)
                throw new System.ArgumentNullException(nameof(directions));

            List<T> neighbors = new List<T>();

            foreach (Vector2Int dir in directions)
            {
                int newX = x + dir.x;
                int newY = y + dir.y;

                if (WithinBounds(newX, newY))
                {
                    neighbors.Add(Map[newX, newY]);
                }
            }

            return neighbors;
        }

        public virtual Vector3 GetWorldPosition(int x, int y, bool centered = false)
        {
            if (centered)
            {
                if (Gridaxis == GridAxis.XY)
                    return GetWorldPosition(x, y, false) +
                           new Vector3(WorldSpaceCellSize / 2, WorldSpaceCellSize / 2, 0);

                return GetWorldPosition(x, y, false) +
                       new Vector3(WorldSpaceCellSize / 2, 0, WorldSpaceCellSize / 2);
            }

            if (Gridaxis == GridAxis.XY)
                return new Vector3(x + WorldSpaceOffset.x, y + WorldSpaceOffset.y, WorldSpaceOffset.z) *
                       WorldSpaceCellSize;

            return new Vector3(x + WorldSpaceOffset.x, WorldSpaceOffset.y, y + WorldSpaceOffset.y) *
                   WorldSpaceCellSize;
        }

        public Vector3 GetWorldPosition(T cell, bool centered = false)
        {
            GetXY(cell, out int x, out int y);

            return GetWorldPosition(x, y, centered);
        }

        public class GridValueChangedEventArgs : System.EventArgs
        {
            public int x { get; set; }
            public int y { get; set; }

            public GridValueChangedEventArgs(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        public void OnValueChanged(int x, int y)
        {
            GridValueChanged?.Invoke(this, new GridValueChangedEventArgs(x, y));
        }
    }

    public enum GridAxis
    {
        XY,
        XZ
    }
}
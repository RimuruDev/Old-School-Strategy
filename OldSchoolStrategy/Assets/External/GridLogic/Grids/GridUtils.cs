using UnityEngine;

namespace RimuruDev.External.GridLogic.Grids
{
    public static class GridUtils
    {
        // The grid needs to be ontop of a surface with a collider as this uses raycasts
        public static T GetGridValueAtMousePos<T>(Grid<T> grid, Camera camera, out bool foundValue)
        {
            Vector2 mousePosition = Input.mousePosition;

            var mouseRay =
                camera.ScreenPointToRay(new Vector3(mousePosition.x, mousePosition.y, camera.nearClipPlane));

            if (Physics.Raycast(mouseRay, out RaycastHit hit))
            {
                grid.GetXY(hit.point, out var x, out var y);
                Debug.Log(x + " " + y);

                T cell;
                
                try
                {
                    cell = grid.GetValue(x, y);
                }
                catch (System.ArgumentException)
                {
                    foundValue = false;
                    return default;
                }

                foundValue = true;
                return cell;
            }

            foundValue = false;
            return default;
        }
    }
}
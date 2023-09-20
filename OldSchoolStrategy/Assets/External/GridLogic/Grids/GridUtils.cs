using UnityEngine;

namespace RimuruDev.External.GridLogic.Grids
{
    public static class GridUtils
    {
        /// <summary>
        /// The grid needs to be ontop of a surface with a collider as this uses raycasts
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="camera"></param>
        /// <param name="foundValue"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetGridValueAtMousePos<T>(Grid<T> grid, Camera camera, out bool foundValue)
        {
            Vector2 mousePosition = Input.mousePosition;

            var mouseRay =
                camera.ScreenPointToRay(new Vector3(mousePosition.x, mousePosition.y, camera.nearClipPlane));

            if (Physics.Raycast(mouseRay, out RaycastHit hit))
            {
                grid.GetXY(hit.point, out int x, out int y);
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
            else
            {
                foundValue = false;
                return default;
            }
        }
    }
}
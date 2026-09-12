using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
    public class MovementGrid : MonoBehaviour
    {
        [SerializeField] int gridSizeX;
        [SerializeField] int gridSizeY;
        [SerializeField] float gridStep;
        private float gridHalfStep;

        public GridSquare[,] grid;
        public List<GridSquare> path;

        private static MovementGrid instance;
        public static MovementGrid Instance {  get { return instance; } }

        private void Awake()
        {
            instance = this;
            gridHalfStep = gridStep / 2;

            Vector3 center = transform.position;
            grid = new GridSquare[gridSizeX, gridSizeY];

            for(int x = 0; x < gridSizeX; x++)
            {
                for(int y = 0; y < gridSizeY; y++)
                {
                    Vector3 worldPosition = new Vector3(
                        transform.position.x + gridHalfStep + x * (2 * gridHalfStep), 
                        transform.position.y + gridHalfStep + y * (2 * gridHalfStep), 
                        transform.position.z);

                    grid[x, y] = new GridSquare(worldPosition, x, y);
                }
            }
        }

        public GridSquare GetGridSnap(Vector3 worldPosition)
        {
            Vector3 diff = worldPosition - transform.position;
            float percentX = (Mathf.Abs(diff.x)) / (gridSizeX * 2 * gridHalfStep);
            float percentY = (Mathf.Abs(diff.y)) / (gridSizeY * 2 * gridHalfStep);

            percentX = Mathf.Clamp01(percentX);
            percentY = Mathf.Clamp01(percentY);

            int x = Mathf.RoundToInt((gridSizeX-1) * percentX);
            int y = Mathf.RoundToInt((gridSizeY-1) * percentY);

            if (diff.x < 0 && diff.y < 0) return grid[0, 0];
            if (diff.x < 0) return grid[0, y];
            if (diff.y < 0) return grid[x, 0];

            return grid[x, y];
        }
        public List<GridSquare> GetNeighbours(GridSquare currentSquare)
        {
            List<GridSquare> neighbours = new List<GridSquare>();

            Vector2[] dirs = { Vector2.up, Vector2.down, Vector2.right, Vector2.left };
            foreach (Vector2 dir in dirs)
            {
                int checkX = currentSquare.gridX + (int)dir.x;
                int checkY = currentSquare.gridY + (int)dir.y;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                    neighbours.Add(grid[checkX, checkY]);

            }

            return neighbours;
        }
        public int GetEnemyAmount(GridSquare square)
        {
            Collider[] coll = Physics.OverlapBox(square.worldPosition, gridHalfStep * Vector3.one);
            int counter = 0;
            foreach (Collider col in coll) 
            { 
                if (col.CompareTag("Enemy")) 
                {  counter++; } 
            } 
            return counter;
        }
        private void OnDrawGizmos()
        {
            for (int x = 0; x < gridSizeX; x++)
            {
                for (int y = 0; y < gridSizeY; y++)
                {
                    Vector3 position = new Vector3(
                        x * gridStep + gridHalfStep,
                        y * gridStep + gridHalfStep,
                        0
                    );

                    Gizmos.DrawWireCube(
                        position,
                        new Vector3(gridStep, gridStep, 0)
                    );
                }
            }
        }
    }
}
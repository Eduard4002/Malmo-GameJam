using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class GridSystem : MonoBehaviour
{
    
    [SerializeField, Range(0.1f, 2f)] private float cellSize = 1f;
    [SerializeField] private float radius = 5f; // Radius of the circle
    [SerializeField] private Vector2 gridCenter;
    [SerializeField] private GameObject chunkPrefab; // Prefab for each square chunk

    private Vector3[] pointsInCircle;



    void Start()
    {
    }
    private void OnValidate() {
        UpdateGrid();
    }

    void UpdateGrid()
    {
         // Calculate screen boundaries in world space
        Vector3 screenBottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 screenTopRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        float screenWidth = screenTopRight.x - screenBottomLeft.x;
        float screenHeight = screenTopRight.y - screenBottomLeft.y;

        // Calculate grid dimensions
        int columns = Mathf.CeilToInt(screenWidth / cellSize);
        int rows = Mathf.CeilToInt(screenHeight / cellSize);

       // Calculate the grid's origin offset (0,0 is at the circle's position)
        Vector3 origin = gridCenter;
        int index = 0;
        pointsInCircle = new Vector3[columns * rows];

        // Loop through rows and columns to create the grid
        for (int x = -columns / 2; x <= columns / 2; x++)
        {
            for (int y = -rows / 2; y <= rows / 2; y++)
            {
                // Calculate world position for the current cell
                Vector3 cellPosition = origin + new Vector3(x * cellSize, y * cellSize, 0);

                // Instantiate cell prefab
               /*  GameObject cell = Instantiate(cellPrefab, cellPosition, Quaternion.identity, transform);
                cell.transform.localScale = new Vector3(cellSize, cellSize, 1); */
                pointsInCircle[index++] = cellPosition;
            }
        
        }
    }
    Vector2[] PointsInCircle(Vector2 circlePos, float radius, float scale) {
        List<Vector2> temp = new List<Vector2>();
        var minX = circlePos.x - radius;
        var maxX = circlePos.x + radius;
    
        var minY = circlePos.y - radius;
        var maxY = circlePos.y + radius;

        for (var y = minY; y <= maxY; y += scale) {
            for (var x = minX; x <= maxX; x += scale) {
                if (InCircle(new Vector2(x, y), circlePos, radius)) {
                    temp.Add(new Vector2(x,y));
                }
            }
        }
        return temp.ToArray();

    }
    private void OnDrawGizmos() {
        /* for(int i = 0 ; i < pointsInCircle.Length;i++){
            if(InCircle(pointsInCircle[i], gridCenter, radius)){
                Gizmos.color = Color.green;
                Gizmos.DrawWireCube(pointsInCircle[i], new Vector3(1,1,1));
            }
        } */
        Vector2[] points = PointsInCircle(gridCenter, radius, cellSize);
        for(int i = 0 ; i < points.Length;i++){
                Gizmos.color = Color.green;
                Gizmos.DrawWireCube(points[i], new Vector3(cellSize,cellSize,cellSize));
            
        }

    }
    bool InCircle(Vector2 point, Vector2 circlePoint, float radius) {
        return (point - circlePoint).sqrMagnitude <= radius * radius;
    }
}

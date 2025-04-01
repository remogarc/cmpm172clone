using UnityEngine;
using System.Collections.Generic;

public class TrailIntersection : MonoBehaviour
{
    public float pointSpacing = 0.5f; // Minimum distance before adding a new position
    public int maxStoredPositions = 100; // Prevent excessive memory usage
    public GameObject player;

    private List<Vector3> pastPositions = new List<Vector3>();

    void Update()
    {
        Vector3 currentPosition = player.transform.position;

        // Only add a new position if the player moved far enough
        if (pastPositions.Count == 0 || Vector3.Distance(pastPositions[pastPositions.Count - 1], currentPosition) > pointSpacing)
        {
            AddNewPosition(currentPosition);
        }

        // Check for self-intersection
        if (CheckSelfIntersection())
        {
            Debug.Log("Player has crossed their own path!");
        }
    }

    void AddNewPosition(Vector3 newPosition)
    {
        pastPositions.Add(newPosition);

        // Limit the stored positions to avoid performance issues
        if (pastPositions.Count > maxStoredPositions)
        {
            pastPositions.RemoveAt(0);
        }
    }

    bool CheckSelfIntersection()
    {
        int count = pastPositions.Count;
        if (count < 4) return false; // Need at least 4 points to check intersection

        Vector3 A = pastPositions[count - 2]; // Last segment start
        Vector3 B = pastPositions[count - 1]; // Last segment end

        // Check if the new segment (A to B) intersects with any older segments
        for (int i = 0; i < count - 3; i++)
        {
            Vector3 C = pastPositions[i];
            Vector3 D = pastPositions[i + 1];

            if (LinesIntersect(A, B, C, D))
            {
                return true;
            }
        }
        return false;
    }

    bool LinesIntersect(Vector3 A, Vector3 B, Vector3 C, Vector3 D)
    {
        float denominator = (B.x - A.x) * (D.z - C.z) - (B.z - A.z) * (D.x - C.x);
        if (Mathf.Abs(denominator) < Mathf.Epsilon) return false; // Parallel lines

        float numerator1 = (A.z - C.z) * (D.x - C.x) - (A.x - C.x) * (D.z - C.z);
        float numerator2 = (A.z - C.z) * (B.x - A.x) - (A.x - C.x) * (B.z - A.z);

        float r = numerator1 / denominator;
        float s = numerator2 / denominator;

        return (r >= 0 && r <= 1 && s >= 0 && s <= 1);
    }
}

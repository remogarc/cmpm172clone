using UnityEngine;
using System.Collections.Generic;

public class PathTo3DShape : MonoBehaviour
{
    public float pointSpacing = 0.5f;
    public int maxStoredPositions = 100;
    public float extrusionHeight = 1f; // Height of the 3D shape

    private List<Vector3> pastPositions = new List<Vector3>();
    private bool intersectionDetected = false;
    private List<Vector3> closedLoop = new List<Vector3>();
    public GameObject player; 
    public Material m;
    public GameObject cam;
    Vector3 currentPosition;
    void Update()
    {
        Debug.Log(currentPosition);
        if (intersectionDetected) return; // Stop after first intersection

        currentPosition = player.transform.position;

        if (pastPositions.Count == 0 || Vector3.Distance(pastPositions[pastPositions.Count - 1], currentPosition) > pointSpacing)
        {
            AddNewPosition(currentPosition);
        }

        int intersectionIndex = CheckSelfIntersection();
        if (intersectionIndex != -1)
        {
            Debug.Log("Loop Detected!");
            ExtractLoop(intersectionIndex);
            Generate3DShape();
            // intersectionDetected = true; // Prevent multiple shapes from being created
        }
    }

    void AddNewPosition(Vector3 newPosition)
    {
        pastPositions.Add(newPosition);

        if (pastPositions.Count > maxStoredPositions)
        {
            pastPositions.RemoveAt(0);
        }
    }

    int CheckSelfIntersection()
    {
        int count = pastPositions.Count;
        if (count < 4) return -1;

        Vector3 A = pastPositions[count - 2];
        Vector3 B = pastPositions[count - 1];

        for (int i = 0; i < count - 3; i++)
        {
            Vector3 C = pastPositions[i];
            Vector3 D = pastPositions[i + 1];

            if (LinesIntersect(A, B, C, D))
            {
                return i; // Return the index of the intersection start point
            }
        }
        return -1;
    }

    void ExtractLoop(int intersectionIndex)
    {
        closedLoop.Clear();
        for (int i = intersectionIndex; i < pastPositions.Count; i++)
        {
            closedLoop.Add(pastPositions[i]);
        }
        Debug.Log("Loop extracted with " + closedLoop.Count + " points.");
    }

    void Generate3DShape()
    {
        GameObject shapeObject = new GameObject("3DLoopShape");
        shapeObject.transform.position = Vector3.zero;

        MeshFilter meshFilter = shapeObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = shapeObject.AddComponent<MeshRenderer>();
        meshRenderer.material = m;

        Mesh mesh = new Mesh();

        // Create vertices for both the bottom and top loops
        int loopSize = closedLoop.Count;
        Vector3[] vertices = new Vector3[loopSize * 2];
        for (int i = 0; i < loopSize; i++)
        {
            vertices[i] = new Vector3(closedLoop[i].x, 0, closedLoop[i].z); // Bottom face
            vertices[i + loopSize] = new Vector3(closedLoop[i].x, extrusionHeight, closedLoop[i].z); // Top face
        }

        // Triangulate the top and bottom faces
        List<int> triangles = new List<int>();
        TriangulateFace(triangles, vertices, 0, loopSize); // Bottom face
        TriangulateFace(triangles, vertices, loopSize, loopSize); // Top face (inverted)

        // Create side walls
        for (int i = 0; i < loopSize; i++)
        {
            int next = (i + 1) % loopSize;

            // First triangle (bottom to top)
            triangles.Add(i);
            triangles.Add(next);
            triangles.Add(i + loopSize);

            // Second triangle (top to bottom)
            triangles.Add(next);
            triangles.Add(next + loopSize);
            triangles.Add(i + loopSize);
        }

        // Assign the mesh
        mesh.vertices = vertices;
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();

        meshFilter.mesh = mesh;
    // Add a MeshCollider to the shape to match the mesh
        MeshCollider meshCollider = shapeObject.AddComponent<MeshCollider>();
        meshCollider.sharedMesh = mesh; // Set the mesh for the collider
        meshCollider.convex = true; // Set convex if you need physics interactions
        Rigidbody rb = shapeObject.AddComponent<Rigidbody>();
        rb.useGravity = true;  // Enable gravity (you can disable it based on your need)
        rb.mass = 1;  
        shapeObject.transform.position = new Vector3(currentPosition.x/24, 5f, currentPosition.z/24);
    }

    void TriangulateFace(List<int> triangles, Vector3[] vertices, int startIndex, int loopSize)
    {
        for (int i = 1; i < loopSize - 1; i++)
        {
            triangles.Add(startIndex);
            triangles.Add(startIndex + i);
            triangles.Add(startIndex + i + 1);
        }
    }

    bool LinesIntersect(Vector3 A, Vector3 B, Vector3 C, Vector3 D)
    {
        float denominator = (B.x - A.x) * (D.z - C.z) - (B.z - A.z) * (D.x - C.x);
        if (Mathf.Abs(denominator) < Mathf.Epsilon) return false;

        float numerator1 = (A.z - C.z) * (D.x - C.x) - (A.x - C.x) * (D.z - C.z);
        float numerator2 = (A.z - C.z) * (B.x - A.x) - (A.x - C.x) * (B.z - A.z);

        float r = numerator1 / denominator;
        float s = numerator2 / denominator;

        return (r >= 0 && r <= 1 && s >= 0 && s <= 1);
    }
}

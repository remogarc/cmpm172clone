using UnityEngine;

public class PlayerShadow : MonoBehaviour
{
    public GameObject shadow;
    public float offset = 0.5f;  // Height to cast the ray from the player
    public float raycastDistance = 10f; // Maximum distance for the raycast

    private void FixedUpdate()
    {
        // Start the ray just below the player (based on offset)
        Vector3 rayOrigin = transform.position - new Vector3(0, offset, 0);
        Ray downRay = new Ray(rayOrigin, Vector3.down);

        // Cast the ray to detect the ground
        if (Physics.Raycast(downRay, out RaycastHit hit, raycastDistance))
        {
            // If the ray hits something (ground or surface), move the shadow to that point
            shadow.transform.position = hit.point;
        }
        else
        {
            // If the ray misses, keep the shadow at the player's position
            shadow.transform.position = transform.position;
        }
    }
}

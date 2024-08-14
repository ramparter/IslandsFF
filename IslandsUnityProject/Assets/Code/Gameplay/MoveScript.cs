using System;
using UnityEngine;

/// <summary>
/// Simply moves the current game object
/// </summary>
public class MoveScript : MonoBehaviour
{
    // 1 - Designer variables

    /// <summary>
    /// Object speed
    /// </summary>
    public Vector2 speed = new Vector2(10, 10);

    /// <summary>
    /// Moving direction
    /// </summary>
    public Vector2 direction = new Vector2(-1, 0);

    private Vector2 movement;
    float cameraWidth;

    void Start()
    {
        cameraWidth = (Camera.main.ViewportToWorldPoint(Vector2.right).x - Camera.main.transform.position.x) * 2;

    }

    void Update()
    {
        if (Camera.main.transform.position.x > transform.position.x + cameraWidth)
            Destroy(gameObject);

        movement = new Vector2(
          speed.x * direction.x,
          speed.y * direction.y) * Time.fixedDeltaTime;
        
    }

    void FixedUpdate()
    {
        // Apply movement to the rigidbody
        transform.position = new Vector3(transform.position.x + movement.x, transform.position.y + movement.y, transform.position.z);
    }
}
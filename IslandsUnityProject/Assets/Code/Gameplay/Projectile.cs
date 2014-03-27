using UnityEngine;

/// <summary>
/// Projectile behavior
/// </summary>
public class Projectile : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 10);
    }
}
using System.CodeDom.Compiler;
using UnityEngine;

/// <summary>
/// Launch projectile
/// </summary>
public class WeaponScript : MonoBehaviour
{
    //--------------------------------
    // 1 - Designer variables
    //--------------------------------

    /// <summary>
    /// Projectile prefab for shooting
    /// </summary>
    public Transform shotPrefab;

    public Transform smokePrefab;

    /// <summary>
    /// Cooldown in seconds between two shots
    /// </summary>
    public float shootingRate = 0.25f;

    public float temperatureCooldownRate = 2f;
    public float temperatureShotIncrease = 10f;
    public float minTemperature = 10f;
    public float maxTemperature = 200f;


    //--------------------------------
    // 2 - Cooldown
    //--------------------------------

    private float shootCooldown;
    private float temperature;

    void Start()
    {
        shootCooldown = 0f;
        temperature = minTemperature;
    }

    void Update()
    {
        if (shootCooldown > 0)
        {
            shootCooldown -= Time.deltaTime;
        }
        if (temperature > minTemperature)
        {
            temperature -= Time.deltaTime * temperatureCooldownRate;
        }
    }

    //--------------------------------
    // 3 - Shooting from another script
    //--------------------------------

    /// <summary>
    /// Create a new projectile if possible
    /// </summary>
    public void Attack(Alliance alliance)
    {
        if (CanAttack)
        {
            shootCooldown = shootingRate;

            Shoot(alliance);
            ProduceSmoke();
        }
    }

    private void Shoot(Alliance alliance)
    {
        var shotTransform = Instantiate(shotPrefab) as Transform;
        // Assign position
        shotTransform.position = transform.position;
        shotTransform.rotation = transform.rotation;

        // The is enemy property
        DamageOnContact shot = shotTransform.gameObject.GetComponent<DamageOnContact>();
        if (shot != null)
        {
            shot.alliance = alliance;
        }

        // Make the weapon shot always towards it
        MoveScript move = shotTransform.gameObject.GetComponent<MoveScript>();
        if (move != null)
        {
            move.direction = this.transform.right; // towards in 2D space is the right of the sprite
            move.speed += GetComponentInParent<Rigidbody2D>().velocity * 1f;
        }
    }
    private void ProduceSmoke()
    {
        var smoke = Instantiate(smokePrefab) as Transform;
        // Assign position
        smoke.position = transform.position;
        smoke.rotation = transform.rotation;

        // The is enemy property
 
        // Make the weapon shot always towards it
        MoveScript move = smoke.gameObject.GetComponent<MoveScript>();
        if (move != null)
        {
            move.direction = this.transform.right; // towards in 2D space is the right of the sprite
            move.speed += GetComponentInParent<Rigidbody2D>().velocity * 1f;
        }
    }

    /// <summary>
    /// Is the weapon ready to create a new projectile?
    /// </summary>
    public bool CanAttack
    {
        get
        {
            return shootCooldown <= 0f && temperature <= maxTemperature;
        }
    }
}
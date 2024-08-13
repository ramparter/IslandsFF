using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class MovingProperties
{
    public Vector2 mobilityAcceleration = new Vector2(200, 200);
    public Vector2 acceleration = new Vector2(50, 0);
    //public Vector2 maxSpeed = new Vector2(800, 0);
    public float mobilityAirDragFactor = 0.6f;
    public float airDragFactor = 0.875f;
}

[Serializable]
public class SteamProperties
{
    public float maxSteam = 100;
    public float steamConsumptionMax = 4;
    public float steamConsumptionCurve = 2f;

}

/// <summary>
/// Player controller and behavior
/// </summary>
public class PlayerScript : MonoBehaviour
{
    public MovingProperties movingProperties;
    public SteamProperties steamProperties;
    public GUISkin skin;

    private Vector2 mobilitySpeed;
    private Vector2 speed;
    private float steam;

    // input
    private Vector2 movement;
    private bool shoot;

    // weapons
    WeaponScript[] weapons;

    GUIStyle guiStyle;

    public Vector2 Movement { get { return movement; } }


    void Start()
    {

        speed = Vector2.zero;
        steam = 50;
        weapons = GetComponentsInChildren<WeaponScript>();
    }

    void Update()
    {
        GetInput();

        UpdateMove();
        UpdateSteam();

        Shoot();
    }

    private void Shoot()
    {
        if (shoot)
        {
            foreach (WeaponScript weapon in weapons)
            {
                // Auto-fire
                if (weapon != null && weapon.CanAttack)
                {
                    weapon.Attack(Alliance.player);
                }
            }
        }
    }

    private void GetInput()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        //Debug.Log(movement);

        // Careful: For Mac users, ctrl + arrow is a bad idea
        shoot = Input.GetButton("Fire1");
        shoot |= Input.GetButton("Fire2");

        #if UNITY_ANDROID
            if(movement == Vector2.zero)
                movement = AccelReader.GetAccelMovement();
        #endif

    }

    private void UpdateMove()
    {
        mobilitySpeed.x += Time.deltaTime * movingProperties.mobilityAcceleration.x * movement.x;
        mobilitySpeed.y += Time.deltaTime * movingProperties.mobilityAcceleration.y * movement.y;
        mobilitySpeed *= movingProperties.mobilityAirDragFactor;

        speed += Time.deltaTime * (0f + 1 * steam / steamProperties.maxSteam) * movingProperties.acceleration;
        speed *= movingProperties.airDragFactor;

        InBounds();
    }

    private void UpdateSteam()
    {
        steam -= Time.deltaTime * steamProperties.steamConsumptionMax * Mathf.Pow(steam / steamProperties.maxSteam, steamProperties.steamConsumptionCurve) ;
        steam = Mathf.Clamp(steam, 0, steamProperties.maxSteam);
    }

    private void InBounds()
    {
        var dist = (transform.position - Camera.main.transform.position).z;
        var leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
        var rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
        var topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;
        var bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y;

        transform.position = new Vector3(
          Mathf.Clamp(transform.position.x, leftBorder, rightBorder),
          Mathf.Clamp(transform.position.y, topBorder, bottomBorder),
          transform.position.z
        );
    }

    void FixedUpdate()
    {
        Camera.main.transform.Translate(speed * Time.deltaTime);

        GetComponent<Rigidbody2D>().velocity = mobilitySpeed + speed;

        //rigidbody2D.AddForce(mobilitySpeed + speed);

    }

    public void OnCollect(Collectable collectable)
    {
        if (collectable.GetComponent<Cloud>())
            steam += collectable.GetComponent<Cloud>().steamAmount;

        if (collectable.GetComponent<Valuable>())
            GameObject.FindObjectOfType<LevelController>().UpdateValuables(collectable.GetComponent<Valuable>());
    }

    void OnGUI()
    {
        GUI.skin = skin;

        Gu.Label(Gu.Left(60), Gu.Top(20), Gu.Dim(36), "Steam: " + (int)steam + " / " + steamProperties.maxSteam, false);
        HealthScript health = GetComponent<HealthScript>();
        if (health)
        {
            Gu.Label(Gu.Left(60), Gu.Top(70), Gu.Dim(36), "Health: " + (int)health.HP + " / " + health.maxHP, false);
        }

        Gu.Label(Gu.Left(60), Gu.Top(120), Gu.Dim(36), "Speed: " + speed.x.ToString("0.000"), false);

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        speed = Vector2.zero;

    }
}

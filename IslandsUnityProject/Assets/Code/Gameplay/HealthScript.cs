using UnityEngine;

public enum Alliance
{
    player = 0,
    enemy,
    obstacle,
    all,
    none
}

/// <summary>
/// Handle hitpoints and damages
/// </summary>
public class HealthScript : MonoBehaviour
{
    /// <summary>
    /// Total hitpoints
    /// </summary>
    public float maxHP = 100;
    public float HP { get { return hp; } }
    public float regeneration = 5;
    public int score = 0;

    private float hp;
    private bool alive = true;
    private LevelController levelController;


    public float hitDuration = 1;
    public Color hitColor = new Color(0.8f, 0.5f, 0.5f);
    public float hitFlickFrequeny = 4;

    float hitTimer = 0;

    public bool Alive { get { return alive; } }
    /// <summary>
    /// Enemy or player?
    /// </summary>
    public Alliance alliance = Alliance.enemy;

    void Start()
    {
        levelController = FindAnyObjectByType<LevelController>().GetComponent<LevelController>();
        hp = maxHP;

    }

    void Update()
    {
        if (hitTimer > 0)
            UpdateHit();

        if (alive && regeneration > 0)
            hp += regeneration * Time.deltaTime;
        hp = Mathf.Clamp(hp, 0, maxHP);
    }

    private void UpdateHit()
    {
        hitTimer -= Time.deltaTime;

        if (hitTimer > 0)
        {
            float t = 0.5f + 0.5f * Mathf.Cos(Time.time * hitFlickFrequeny * Mathf.PI * 2);
            GetComponent<SpriteRenderer>().color = new Color((1 - t) + t * hitColor.r, (1 - t) + t * hitColor.g, (1 - t) + t * hitColor.b, hp == 0 ? 1 - hitTimer / hitDuration : 1);
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        // Is this a shot?
        DamageOnContact damage = collider.gameObject.GetComponent<DamageOnContact>();
        if (damage != null)
        {
            // Avoid friendly fire
            if (damage.alliance != alliance)
            {
                OnHit(damage.gameObject);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!alive) return;
        if (hitTimer > 0) return;
        //if (collision.collider.gameObject.GetComponent<HealthScript>().alliance == alliance)
        //    return;
        if (collision.collider.GetComponent<DamageOnContact>())
            OnHit(collision.gameObject);
    }

    void OnHit(GameObject otherObject)
    {
        hitTimer = hitDuration;
        hp -= otherObject.GetComponent<DamageOnContact>().damageOnContact;
        if (hp <= 0)
        {
            OnKilled();
        }
    }

    private void OnKilled()
    {
        // Dead!
        alive = false;
        Destroy(GetComponent<Collider2D>());
        Destroy(gameObject, hitDuration);
        if (score > 0)
            levelController.UpdateScore(score);
    }
}
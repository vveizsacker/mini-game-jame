using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected SpriteRenderer spriterenderer;
    [SerializeField] protected Animator animator;
    [SerializeField] protected Bar healthBar;
    [SerializeField] protected Entity target;
    [SerializeField] protected bool isAlly;
    [SerializeField] protected LayerMask enemyLayer,environmentLayer;

    [Header("Base Stats")]
    public int level;

    [SerializeField] protected int baseMaxHealth;
    [SerializeField] protected int baseDamage;
    [SerializeField] protected int baseSpeed;
    [SerializeField] protected float baseAttackRate;
    [SerializeField] protected float baseCritChance;
    [SerializeField] protected float baseResistance;

    [SerializeField] protected int maxHealth,health;
    [SerializeField] protected int damage;
    [SerializeField] protected int speed;
    [SerializeField] protected float attackRate;
    [SerializeField] protected float critChance;
    [SerializeField] protected float resistance;

    [SerializeField] protected bool isRanged = false;

    List<Effect> effects = new List<Effect>();

    protected float moveCounter;
    protected float attackCounter;

    [SerializeField] protected int attackRange;

    [SerializeField] protected bool canAttack;
    [SerializeField] protected bool canMove;


    void Start()
    {
        maxHealth = baseMaxHealth;
        health = maxHealth;
        damage = baseDamage;
        speed = baseSpeed;
        attackRate = baseAttackRate;
        critChance = baseCritChance;
        resistance = baseResistance;

        Upgrade();
        healthBar.SetMaxValue(maxHealth);
    }

    protected void Attack(Entity entity)
    {
        entity.TakeDamage(damage,this);
        entity.KnockBack((entity.transform.position - transform.position).normalized, baseDamage);
        animator.Play("attack");
    }

    public GameObject projectile;
    
    protected void CreateProjectile(Entity target)
    {
    
        GameObject go = Instantiate(projectile,transform.position,Quaternion.identity);

        Projectile p = go.AddComponent<Projectile>();
        p.Set(damage, this, target);
    }
    protected bool CheckObstacle(Vector2 dir)
    {
        RaycastHit2D obst = Physics2D.Raycast(transform.position, dir, 1,enemyLayer);
        if (obst.collider == null) return false;
        return true;
    }
    protected void Move(Vector2 dir)
    {
        rb.velocity = dir * speed * Time.deltaTime * 100;
    }

    public void Stop()
    {
        rb.velocity = Vector2.zero;
    }

    protected Vector2 getRandomDir()
    {
        return new Vector2(Random.Range(-1, 2), Random.Range(-1, 2));
    }

    public static Entity[] getEntitiesInRange(Vector2 pos , float range , LayerMask layer)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(pos, range,layer);
        
        Entity[] entities = new Entity[colliders.Length];
        for (int i = 0; i < colliders.Length; i++)
        {
            entities[i] = colliders[i].GetComponent<Entity>();
        }
        return entities;
    }

    public Entity getClosestEntity()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, Mathf.Infinity, enemyLayer);
        
        if (collider != null)
        {
            Entity entity = collider.GetComponent<Entity>();
            return entity;
        }
        return null;
    }
    
    protected void Look(float x)
    {
        Vector3 scale = Vector3.one;
        if (x > 0) scale.x = 1;
        else scale.x = -1;
        spriterenderer.transform.localScale = scale;
    }

    protected bool CheckInRange(Vector2 pos,float range)
    {
        return Vector2.Distance(transform.position, pos) < range;
    }

    public void KnockBack(Vector2 dir , int force)
    {
        rb.AddForce(dir * force * 100);
    }

    public void TakeDamage(int damage,Entity entity)
    {
        if (entity != null)
            target = entity;

        GameManager.instance.CreatePopup(damage.ToString(), Color.red, transform.position);
        
        StartCoroutine(displayDamage());
        health -= damage;
        healthBar.updateValue(health);
        if (health <= 0) Die();
    }

    IEnumerator displayDamage()
    {
        spriterenderer.color = Color.red;
        yield return new WaitForSeconds(.25f);
        spriterenderer.color = Color.white;
    }

    public void Heal(int amount)
    {
        health += amount;
        health = Mathf.Clamp(health, 0, baseMaxHealth);
    }

    public void Freez(float duration)
    {
        StartCoroutine(_freez(duration));
    }

    IEnumerator _freez(float duration)
    {
        canMove = false;
        yield return new WaitForSeconds(duration);
        canMove = true;
    }

    public void AddEffect(Effect effect)
    {
        effects.Add(effect);
    }

    void Die()
    {
        Destroy(gameObject);
        Debug.Log("died");
    }
    
    public void Upgrade()
    {
        level++;
        maxHealth += baseMaxHealth / 4 * level;
        health = maxHealth;
        damage += baseDamage / 5 * level;
        resistance += baseResistance / 4 * level;
        speed += baseSpeed / 5 * level;
    }
}

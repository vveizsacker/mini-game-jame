using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected SpriteRenderer spriterenderer;
    [SerializeField] protected Animator animator;
    [SerializeField] protected Bar healthBar;

    [SerializeField] protected bool isAlly;
    [SerializeField] protected LayerMask enemyLayer;

    [SerializeField] protected int maxHealth;
    protected int health;
    [SerializeField] protected int baseDamage;
    [SerializeField] protected float attackRate;
    [SerializeField] protected float critChance;
    [SerializeField] protected float resistance;
    [SerializeField] protected bool isRanged = false;

    protected float moveCounter;
    protected float attackCounter;

    [SerializeField] protected int speed;
    [SerializeField] protected int attackRange;

    [SerializeField] protected bool canAttack;
    [SerializeField] protected bool canMove;


    void Start()
    {
        health = maxHealth;
        healthBar.SetMaxValue(maxHealth);
    }

    protected void Attack(Entity entity)
    {
        entity.TakeDamage(baseDamage);
        //entity.KnockBack((entity.transform.position - transform.position).normalized, baseDamage);
        animator.Play("attack");
    }

    protected void Move(Vector2 dir)
    {
        rb.velocity = dir * speed * Time.deltaTime * 100;
    }

    protected Vector2 getRandomDir()
    {
        return new Vector2(Random.Range(-1, 2), Random.Range(-1, 2));
    }

    public Entity[] getEntitiesInRange(Vector2 pos , float range)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(pos, range);
        
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

    public void Stun(float duration)
    {

    }

    public void AddHealth(int amount)
    {
        if (health + amount > maxHealth) health = maxHealth;
        else health += amount;
    }

    protected bool CheckInRange(Vector2 pos,float range)
    {
        return Vector2.Distance(transform.position, pos) < range;
    }

    public void KnockBack(Vector2 dir , int force)
    {
        rb.AddForce(dir * force * 100);
    }

    public void TakeDamage(int damage)
    {
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
    void Die()
    {
        Destroy(gameObject);
        Debug.Log("died");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public Rigidbody2D rb;
    public SpriteRenderer spriterenderer;
    public Animator animator;
    public Bar healthBar;

    public bool isAlly;

    public int maxHealth;
    public int health;
    public int baseDamage;
    public float attackRate;
    public float critChance;
    public float resistance;
    public bool isRanged = false;

    protected float moveCounter;
    protected float attackCounter;

    public int speed;
    public int attackRange;

    public bool canAttack;
    public bool canMove;


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
        Entity[] entities = new Entity[10];
        return entities;
    }
    public Entity getClosestEntity()
    {
        Entity entity = null;
        return entity;
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

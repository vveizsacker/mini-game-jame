using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rb;
    Entity attacker;
    Entity target;
    GameObject visualFx;
    GameObject contactFx;

    int damage;

    public void Set(int damage ,Entity attacker, Entity target)
    {
        rb = gameObject.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0;

        this.damage = damage;
        this.attacker = attacker;
        this.target = target;
    }

    public void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        if (Vector2.Distance(transform.position, target.transform.position) > 1)
        {
            rb.velocity = (target.transform.position - transform.position).normalized * 10;
        }
        else
        {
            target.TakeDamage(damage, attacker);
            Destroy(gameObject);
        }
            
    }
}

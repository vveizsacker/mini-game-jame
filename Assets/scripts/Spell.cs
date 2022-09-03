using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell 
{
    protected string spellName;
    protected string spellDescription;

    public bool isAreaOfEffect = false;
    public float baseCooldown;

    protected int manaCost;
    protected Player player;

    public virtual void Cast()
    {

    }
    public virtual void Cast(Entity entity)
    {

    }
    public virtual void Cast(Entity[] entites)
    {

    }
}


public class Heal : Spell
{
    int healAmount = 50;
    
    public Heal()
    {
        spellName = "Heal Spell";
        spellDescription = "Heal your minions";
        isAreaOfEffect = true;
        baseCooldown = 10;
    }

    public override void Cast(Entity[] entities)
    {
        foreach(Entity e in entities)
        {
            e.Heal(healAmount);
        }
    }
}

public class Freez : Spell
{
    float freezDuration = 2;

    public Freez()
    {
        spellName = "Freez Spell";
        baseCooldown = 15;
        isAreaOfEffect = true;
    }

    public override void Cast(Entity[] entities)
    {
        foreach (Entity e in entities)
        {
            e.Freez(freezDuration);
        }
        
    }
}

public class FireBall : Spell
{
    int damage = 100;

    public FireBall()
    {
        spellName = "Fire Ball";
        baseCooldown = 20;
        isAreaOfEffect = true;
    }

    public override void Cast(Entity[] entities)
    {
        foreach (Entity e in entities)
        {
            e.TakeDamage(damage,null);
        }
    }
}

public class Rage : Spell
{
    float rageDuration = 5;
    public Rage()
    {
        spellName = "Rage Spell";
        isAreaOfEffect = false;
        baseCooldown = 25;
    }

    public override void Cast(Entity[] entities)
    {
        for (int i = 0; i < entities.Length; i++)
        {
            //entities[i].
        }
    }
}

public class BlackHole : Spell
{

    float duration;
    public BlackHole()
    {
        spellName = "Black Hole";
        isAreaOfEffect = true;
        baseCooldown = 25;
    }

    public override void Cast(Entity[] entities)
    {
        //
    }
}
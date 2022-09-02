using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell 
{
    protected string spellName;
    protected string spellDescription;
    int manaCost;
    Player player;

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
    
    public Heal(Entity[] entities)
    {
        spellName = "Heal Spell";
        spellDescription = "Heal your minions";
    }

    public override void Cast(Entity[] entities)
    {
        foreach(Entity e in entities)
        {
            e.AddHealth(healAmount);
        }
    }
}

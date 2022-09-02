using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell 
{
    protected string spellName;
    
    public virtual void Cast()
    {

    }
}


public class Heal : Spell
{
    int healAmount = 50;
    
    public Heal(Entity[] entities)
    {
        spellName = "Heal Spell";       
    }

    public override void Cast()
    {
        
    }
}

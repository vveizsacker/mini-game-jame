using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect 
{
    public string effectName = "";
    public string effectDescription = "";
    protected bool isTemporary = false;
    protected float duration;

    protected Entity entity;

    public virtual void StartEffect()
    {

    }
    public virtual void Trigger()
    {

    }
    public virtual void EndEffect()
    {

    }

}

public class PowerUp : Effect
{
    float time;

    public PowerUp(Entity entity)
    {
        duration = 5;
        time = duration;

        effectName = "PowerUp";
        isTemporary = true;
    }
    public override void StartEffect()
    {
    
    }
    public override void Trigger()
    {
        time -= Time.deltaTime;
    }
}


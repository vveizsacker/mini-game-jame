using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxMana;
    int mana;

    public Dictionary<string, Spell> availableSpells = new Dictionary<string, Spell>(); //list of avilabeSpells , Spell objects ready to be added
    public Spell[] spells = new Spell[5]; //store spell objects after unlocking them
    
    public GameObject[] baseMinions; // refference minion prefabs
    public List<Entity> minions = new List<Entity>(); // store spawned minions to perform spells on or add effects to
    int minionsLevel;

    public int maxMinionAmount; //limit how much player can spawn minions , more after each level up
    private int  minionAmount;

    public void Start()
    {
        availableSpells.Add("heal", new Heal());
    }

    public void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Summon(0);
        }
    }

    public void LoadMinions()
    {

    }

    public void Summon(int index)
    {
        if (minionAmount >= maxMinionAmount) return;

        GameObject go = Instantiate(baseMinions[index],transform.position,Quaternion.identity);
        minions.Add(go.GetComponent<Entity>());
        
        for (int i = 0; i < minionsLevel; i++)
        {
            go.GetComponent<Entity>().Upgrade();
        }

        minionAmount++;
        minionAmount = Mathf.Clamp(minionAmount, 0, maxMinionAmount);
    }

    public void AddSpell(string spellName)
    {
        for (int i = 0; i < spells.Length; i++)
        {
            if(spells[i] == null)
            {
                spells[i] = availableSpells[spellName];
                return;
            }
        }
    }

    public void AddEffectToMinions()
    {

    }

    public void UpgradeMinionsStats()
    {
        foreach (Entity minion in minions)
        {
            minion.Upgrade();
        }
    }

    public void CastSpell(int index)
    {
        if (spells[index].isAreaOfEffect)
        {

        }
    }

}

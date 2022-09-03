using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] points;

    public Minion[] enemies;

    public int summonAmount;

    public int phase;

    public struct Minion{
        public GameObject go;
        public int phase;
        public float summonRate;
    }

    public void createMinion(Vector2 position,int index)
    {
        //check phase
        //check rarity
        //check amount
        //upgrade based on phase
        //choose random spot
    }

}

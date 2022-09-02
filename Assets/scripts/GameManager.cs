using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    void Awake()
    {
        instance = this;
    }

    public void CreatePopup(string text,Color color,Vector2 position)
    {
        Popup p = new Popup(text,color,position);
        StartCoroutine(p.FloatUp());
    }
}

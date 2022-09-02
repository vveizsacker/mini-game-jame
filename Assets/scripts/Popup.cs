using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Popup 
{
    public string text;
    public Color color;
    public TextMeshPro textmesh;
    GameObject go;

    public Popup(string t,Color c,Vector2 position)
    {
        go = new GameObject();
        textmesh = go.AddComponent<TextMeshPro>();
        textmesh.alignment = TextAlignmentOptions.Center;
        textmesh.fontSize = 4;
        textmesh.sortingOrder = 999;
        text = t;
        color = c;
        
        textmesh.color = c;
        textmesh.text = text;
        go.transform.position = position;
    }

    public IEnumerator FloatUp()
    {
        float duration = .5f;
        while(duration > 0)
        {
            duration -= Time.deltaTime;
            
            Vector2 movement = go.transform.position;
            movement.y += Time.deltaTime / 2;

            go.transform.position = movement;    
            yield return null;
        }
        GameObject.Destroy(go);
    }
}

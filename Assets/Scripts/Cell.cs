using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public bool alive;
    public bool aliveNext;
    public bool justDied;
    public int stableLifeCount;

    SpriteRenderer spriteRenderer;    

    public void UpdateStatus()
    {
        spriteRenderer ??= GetComponent<SpriteRenderer>();        
                
        spriteRenderer.enabled = alive;
        if (stableLifeCount > 10)
            spriteRenderer.color = new Color(stableLifeCount * 0.01f, stableLifeCount * 0.1f, 0);
        else
            spriteRenderer.color = Color.black;
    }
}
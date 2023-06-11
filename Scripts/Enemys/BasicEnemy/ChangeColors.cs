using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColors : MonoBehaviour
{
    public SpriteRenderer render;
    
    public void paintYellow()
    {
        if(render != null)
        {
            render.color = Color.yellow;
        }
        
    }

    public void paintRed()
    {
        if(render != null)
        {
            render.color = Color.red;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    public int health;
    public int numOfHearts;

    public RawImage[] hearts;
    public Sprite fullLine;
    public Sprite emptyLine;

    void Update()
    {
     
        for (int i = 0; i < hearts.Length; i++)
        {
            if(i < health)
            {
                hearts[i].
            }
        }


        for (int i = 0; i < hearts.Length; i++)
        {
            if(i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }


}

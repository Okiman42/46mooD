using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    public int health;
    public int numOfHearts;

    public Image[] hearts;
    public Sprite fullLine;
    public Sprite emptyLine;

    [SerializeField] PlayerHealth playerHealth;

    public void Start()
    {
        Debug.Log(hearts);
    }

    void Update()
    {
        health = (int)playerHealth.GetCurrentHealth();
     
        if ( health > numOfHearts)
        {
            //Debug.Log(health + "health 1");
            health = numOfHearts;
            //Debug.Log(health + "health 2");
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullLine;
            }
            else
            {
                hearts[i].sprite = emptyLine;
            }
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }

        Debug.Log(health + " update");
    }

    

}

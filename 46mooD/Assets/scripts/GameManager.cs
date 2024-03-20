using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public bool weapon1;


    public void Start()
    {
        weapon1 = true;
    }

    public void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weapon1 = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {

            weapon1 = false;
        }
    }

}

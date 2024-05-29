using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saverLine : MonoBehaviour
{
    public List<int> savesObject;

    private int number;
    


    private void Start()
    {
        savesObject = null;
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        string tags = collision.tag;
        if(tags == "enemy") {
            number=collision.GetComponent<EnemyController>().enemyNumber;
            number += 200;
            savesObject.Add(number);
        }else if(tags == "trap")
        {
            number = 3;

        }else if(tags == "player")
        {
            number = 1;
        }

    }
}

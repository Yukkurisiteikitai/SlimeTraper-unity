using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Delete");
    }

    // Update is called once per frame
    void Update()
    {
           
    }
    IEnumerator Delete()
    {
        //after 4 second Delete addObject
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}

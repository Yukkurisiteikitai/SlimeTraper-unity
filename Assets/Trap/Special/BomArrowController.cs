using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomArrowController : MonoBehaviour
{
    public GameObject bon;

    public bool switchBOn = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(switchBOn == false)
        {
            switchBOn = true;
            Instantiate(bon,gameObject.transform.position,Quaternion.identity);
            Destroy(gameObject);
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialHub : MonoBehaviour
{
    public CaracterKind ck;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(ck+":"+collision);
        Debug.Log("dafe");
    }
}
public enum CaracterKind
{
    enemy, trap
}
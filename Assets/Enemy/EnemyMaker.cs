using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMaker : MonoBehaviour
{
    public GameObject Enemy;
    public int EnemyNumber;
    public int value;

    

    private Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < value; i++)
        {
            pos = new Vector3(Randomreturn(-8, 8), Randomreturn(-4, 4), 0);
            
            Instantiate(Enemy,pos,Quaternion.identity);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }float Randomreturn(float mix,float max)
    {
        return Random.Range(mix, max);
    }
}

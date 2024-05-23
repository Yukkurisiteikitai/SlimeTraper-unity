using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditBlockController : MonoBehaviour
{
    SpriteRenderer sr;
    public enum kindBlock
    {
        trap = 0,
        enemy = 1,
        player = 2
    };

    public kindBlock block_kind;
    [SerializeField]public EnemyItem enemydata;
    public int enemyID;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        Debug.Log(block_kind);
        enemyID = CreateManager.nowID;
        sr.sprite = enemydata.DataList[enemyID].sprite_nomal;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class DictinaryManager : MonoBehaviour
{
    //Object
    public Image panelColor;

    public TextMeshProUGUI name_text;
    public TextMeshProUGUI hp_text;
    public TextMeshProUGUI speed_text;
    public TextMeshProUGUI defence_text;
    

    [SerializeField] EnemyItem enemyDataBase;
    [SerializeField] TrapItem trapDataBase;

    //ALL
    private string Id;
    private float Hp;
    private int Attack;
    

    //Enemy
    private int Defense;
    private float speed;
    private TYPE wake;
    private TYPE strong;

    //Trap
    private effect effect;


    public GameObject test;

    private EnemyController eC;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            EnemyPrint(test);
        }

    }
    void EnemyPrint(GameObject enemy) {
        int enemyNumber = 0;

        eC = enemy.GetComponent<EnemyController>();
        enemyNumber = eC.eN_publicer;
        Id = enemyDataBase.DataList[enemyNumber].Id;
        Hp = eC.HP;
        speed = enemyDataBase.DataList[enemyNumber].speed;
        Defense = enemyDataBase.DataList[enemyNumber].Defense;
        wake = enemyDataBase.DataList[enemyNumber].wakeType;
        strong = enemyDataBase.DataList[enemyNumber].strongType;

        panelColor.color = new Color32(224, 54, 59, 210);

        name_text.text = Id;
        hp_text.text = Hp.ToString();
        /*
        speed = speed - (speed % 20);
        speed /= 20;
        if(speed < 1)
        {
            speed = 1;
        }
        */
        speed = levelChange(speed);
        speed_text.text =  "Lv" + speed.ToString();
        Defense = levelChange(Defense);
        
        defence_text.text = "Lv" + Defense.ToString();



    }
    int levelChange(float level)
    {
        level = level - (level % 20);
        level /= 20;
        if(level < 1)
        {
            level = 1;
        }
        int a = (int)level;
        return a;
    }

}

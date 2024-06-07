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

    public TextMeshProUGUI information_text;
    public TextMeshProUGUI name_text;
    public TextMeshProUGUI hp_text;
    public TextMeshProUGUI speed_text;
    public TextMeshProUGUI defence_text;
    

    [SerializeField] EnemyItem enemyDataBase;
    [SerializeField] TrapItem trapDataBase;


    public Sprite[] effect_sprite;
    public Sprite[] type_sprite;
    //ALL
    private string Id;
    private float Hp;
    private float Attack;



    //Enemy
    private int trapNumber;
    private int Defense;
    private float speed;
    private TYPE wake;
    private TYPE strong;

    //Trap
    private effect effect;
    private TYPE trapType;

    public GameObject test;

    private EnemyController eC;
    private TrapController tC;

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
    void TrapPrint(GameObject trap)
    {
        information_text.text = "HP\nattack\ntype\neffect";

        tC = trap.GetComponent<TrapController>();
        trapNumber = tC.TrapNumber;
        Hp = tC.HP;
        Attack = trapDataBase.TrapList[trapNumber].damage;
        trapType = tC.traptype;

        panelColor.color = new Color32(127, 18, 255, 210);
        name_text.text = trapDataBase.TrapList[trapNumber].Trap_name;
        hp_text.text = Hp.ToString();

         

         
    }


    void EnemyPrint(GameObject enemy) {
        information_text.text = "HP\nspeed\ndifence\nwakeType\nstrongType ";

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
    string ToStr(int joi)
    {
        string a = joi.ToString();
        return a;
    }

}

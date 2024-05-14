using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GsmeManeger : MonoBehaviour
{
    //map[y,x]  で管理してる

    /* 0= なんもない
     * 1=　プレイヤー
     * 2=　敵
     * 3=　トラップ
     */
    public Transform player;
    public GameObject enemy;
    public GameObject trap;

    public List<int> enemyList;


    [SerializeField] public StageDate stagedate;
    public int stageNumber;

    public Text EnemyText;
    public static int EnemyCount;

    public static int enemyNumber = 0;
    public int GetSetenemyNumber
    {
        get { return enemyNumber; }
        //set { enemyNumber = value; }
    }
    private int mapnumberMix = 0;


    int[,] map = new  int[9,17] {
        { 2, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 2,2 },
        { 2, 0, 0, 0, 0, 2, 2, 0, 2, 2, 0, 0, 0, 0, 0, 2,2 },
        { 2, 0, 0, 2, 2, 0, 0, 0, 0, 0, 2, 2, 0, 0, 0, 2,2 },
        { 2, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 0, 2,2 },
        { 2, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 2, 2,2 },
        { 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2,2 },
        { 2, 0, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2,2 },
        { 2, 0, 0, 0, 2, 2, 2, 0, 0, 0, 0, 2, 2, 2, 0, 2,2 },
        { 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2,2 }
    };
    int mapNumber = 0;
    int y_number = 0;
    int x_number = 0;
    // Start is called before the first frame update
    void Start()
    {

        //stageNumber = MenuManager.menuStageNumber;


        for(int y = 4; y >=  -5; y--)
        {
            
            for(int x = -11; x <= 11; x++)
            {
                if (x_number > 22)
                {
                    x_number = 0;
                }
                //map[y, x];
                //mapNumber = map[y_number, x_number];
                mapNumber = stagedate.stageList[stageNumber].yValue[y_number].xValue[x_number];

                //Debug.Log("Alldo");
                //Debug.Log(mapNumber);
                switch (mapNumber)
                {
                    
                    case 1:
                        player.position = new Vector3(x, y, 0);
                        //Debug.Log("player");
                        break;
                        /*
                    case 2:
                        Instantiate(enemy, new Vector3(x, y, 0), Quaternion.identity);
                        //Debug.Log("enemy");
                        EnemyCount++;
                        break;
                        */
                    case 3:
                        Instantiate(trap, new Vector3(x, y, 0), Quaternion.identity);
                        //Debug.Log("trap");
                        break;
                        
                        
                }
                mapnumberMix = mapNumber % 100;
                //Debug.Log("mapnumberMix" +mapnumberMix);

                int mapcheack = mapNumber - mapnumberMix;
                //Debug.Log("mapcheack" + mapcheack);
                if(mapcheack == 200)
                {
                    //EnemyController.enemyNumber = mapnumberMix;
                    enemyNumber = mapnumberMix;
                    //Debug.Log("enemyNumberG" + enemyNumber);
                    //Debug.Log("CheckSmapnumberMix" + mapnumberMix);
                    Instantiate(enemy, new Vector3(x, y, 0), Quaternion.identity);
                    EnemyCount++;
                    enemyList.Add(enemyNumber);
                }

                
                x_number++;
                
            }
            y_number++;
        }

        PrintEnemyCount();

    }

    // Update is called once per frame
    void Update()
    {
        PrintEnemyCount();

        if(EnemyCount < 0)
        {
            Debug.Log("Why EnemyCount < 0");

        }
    }

    public void PrintEnemyCount()
    {
        EnemyText.text = EnemyCount.ToString();
    }

    public void DestroyEnemy()
    {
        EnemyCount--;
        PrintEnemyCount();
    }
}

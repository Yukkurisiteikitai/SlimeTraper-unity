using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using TMPro;

[Serializable]public class Total
{
    public int EnemyKill;
    public float PlayingTime;
    public int SetTrap;
}

public class GsmeManeger : MonoBehaviour
{
    //@result-Use result menu.

    //map[y,x]  で管理してる
    /* 0= なんもない
     * 1=　プレイヤー
     * 2=　敵
     * 3=　トラップ
     */
    public List<int> enemyManagerID = new List<int>();

    public Transform player;
    public GameObject enemy;
    public GameObject trap;

    public GameObject menu;

    public List<int> enemyList = new List<int>();//@bag

    //total
    public Total total;

    [SerializeField] public StageDate stagedate;
    public int stageNumber;

    public Text EnemyText;
    public static int EnemyCount;//@result

    public TextMeshProUGUI enemyCounter_text;

    public static int enemyNumber = 0;
    public int GetSetenemyNumber
    {
        get { return enemyNumber; }
        //set { enemyNumber = value; }
    }
    private int mapnumberMix = 0;

    //@result.
    public static float TakenDamage = 0;//Other.
    public float ClearTime = 0;
    public static int DownEnemy = 0;//Other.
    public static int UsedTrap = 0;//Other.
    //text.
    public TextMeshProUGUI ResultValue;
    public Image ResultPanel;
    public TextMeshProUGUI ScoreALL_text;

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

    public SpriteRenderer background;
    [SerializeField] GameDate gamestageDateBase;


    public bool EndGame = false;
    public bool Use_menuListNumber = true;

    private void ResetStates()
    {
        EnemyController.enemyListNumber = 0;
        EnemyCount = 0;
        UsedTrap = 0;
        DownEnemy = 0;
        TakenDamage = 0;
        menu.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        ResetStates();

        if (Use_menuListNumber)
        {
            stageNumber = MenuManager.menuStageNumber;
        }

        background.sprite = gamestageDateBase.stagelist[stageNumber].background;

        //SetStage 
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

                switch (mapNumber)
                {
                    case 1:
                        player.position = new Vector3(x, y, 0);
                        break;
                    case 3:
                        Instantiate(trap, new Vector3(x, y, 0), Quaternion.identity);
                        break;
                        
                        
                }
                mapnumberMix = mapNumber % 100;
                
                int mapcheack = mapNumber - mapnumberMix;
                if(mapcheack == 200)
                {
                    enemyNumber = mapnumberMix;
                    Instantiate(enemy, new Vector3(x, y, 0), Quaternion.identity);
                    EnemyCount++;
                    enemyList.Add(enemyNumber);//@bag
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
        if(EndGame == false)
        {
            ClearTime += Time.deltaTime;

        }
        PrintEnemyCount();

        if(EnemyCount == 0)
        {
            GameSet("clear");
        }
        if(EnemyCount < 0)
        {
            Debug.Log("Why EnemyCount < 0");

        }


        if (Input.GetKey(KeyCode.Q))
        {
            if (Input.GetKey(KeyCode.E))
            {
                SceneManager.LoadScene("GmeScene");
            }
        }


        
        
    }

    public void PrintEnemyCount()
    {
        //EnemyText.text = EnemyCount.ToString();
        enemyCounter_text.text = EnemyCount.ToString();
    }

    public void DestroyEnemy()
    {
        EnemyCount--;
        PrintEnemyCount();
    }public void LoadSence(string name)
    {
        SceneManager.LoadScene(name);
    }
    void TotalGet()
    {
        total.EnemyKill = EnemyCount;
        //total.SetTrap
    }public void GameSet(string state) {
        menu.SetActive(true);
        PrintResult();

        //color
        if(state == "over") {
            Debug.Log("GameOver");
            ResultPanel.color = new Color32(214, 14, 0, 203);
        }
        else if(state == "clear")
        {
            Debug.Log("GameClear");
            ResultPanel.color = new Color32(48, 48, 48, 219);
        }

    }

    void PrintResult()
    {
        EndGame = true;

        //time useTrap DownEnemy Taken Damege
        string text = "";

        text += ClearTime.ToString() + "\n";//w
        text += UsedTrap.ToString() + "\n";//w
        text += DownEnemy.ToString() + "\n";//w
        text += TakenDamage.ToString() + "\n";//k



        //enemyscore or nomal score
        float Score = 100;


        Score += TakenDamage * 1.01f;
        if (ClearTime > DownEnemy * 5)
        {
            Score -= ClearTime * 1.4f;
        }
        else
        {
            Score += ((DownEnemy * 5) - ClearTime) * 5;
        }
        if (UsedTrap > DownEnemy)
        {
            Score -= (UsedTrap * 2);
        }

        //text +=  Score.ToString() + "\n";
        ScoreALL_text.text =  "ALL "+Score.ToString();

        ResultValue.text = text;
    }
}

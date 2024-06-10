using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private TrapItem spia;
    public float damaga_e;
    [SerializeField] private EnemyItem enemydate;
    [SerializeField] private TypeItem typeData;

    //public List<int> enemyList;

    public int enemyNumber = 0;
    public static int enemyListNumber = 0;
    private int enemyAll = GsmeManeger.EnemyCount;
    private TYPE waketype;
    private TYPE myType;
    


    public enum JANKEN_TYPE
    {
        shife = 0,
        gost = 1,
        GOd = 2,
    }

    private Transform enemy_t;

    public GameObject target;
    public Transform target_t;
    public float speed;
    public float HP = 10;
    public bool life = true;// before private
    public bool go = false;
    private TYPE type_wake;
    //private TYPE type_strong;
    

    public TYPE traptype;
    
    


    public string enemyId;

    public SpriteRenderer enemyRender;

    private float timer = 0;

    public Sprite[] enemys = new Sprite[2];

    private PlayerController target_script;

    private GsmeManeger gm;

    public int eN_publicer;

    // Start is called before the first frame update
    void Start()
    {
        


        gm = GameObject.Find("GameManger").GetComponent<GsmeManeger>();
        if(enemyListNumber < enemyAll) {
            enemyNumber = gm.enemyList[enemyListNumber];
            enemyListNumber++;
        }
        enemyId = enemyNumber.ToString() + "nick" + rmd(0.0000001f,1.000000f);



        target = GameObject.Find("slimeBase");
        //target_t = target.GetComponent<Transform>();
        enemy_t = GetComponent<Transform>();

        target_script = target.GetComponent<PlayerController>();


        damaga_e = spia.TrapList[0].damage;
        enemyRender = GetComponent<SpriteRenderer>();
        //enemyNumber = gm.enemyNumber;
        Debug.Log(gm);
        int eN = enemyNumber;//GsmeManeger.enemyNumber;
        eN_publicer = eN;
        Debug.Log("eN" +eN);


        //waketype = enemydate.DataList[eN].wakeType;
        //strongtype = enemydate.DataList[eN].strongType;
        

        HP = enemydate.DataList[eN].Hp;
        speed = enemydate.DataList[eN].speed;
        Debug.Log("enemyNumber" + eN);
        enemyRender.sprite = enemydate.DataList[eN].sprite_nomal;
        enemys[0] = enemydate.DataList[eN].sprite_nomal;
        enemys[1] = enemydate.DataList[eN].sprite_dealete;
        myType = enemydate.DataList[eN].type;
        
        //type_strong = enemydate.DataList[eN].strongType;

        //type_wake = enemydate.DataList[eN].wakeType;@todo.

        
        for(int i = 0; i < 5; i++)
        {
            if(typeData.TypeList[i].myType == myType)
            {
                type_wake = typeData.TypeList[i].wakeType[0];
            }
        }
        Debug.Log(myType);
        Debug.Log(type_wake);




        //enemyNumber = 0;
    }

    // Update is called once per frame
    void Update()
    {
        go = target_script.NowMove;
        
        if (life == true&&go==true) {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            
            //MoveEnemy();
        }
        timer += Time.deltaTime;
        if(HP <= 3)
        {
            StartCoroutine("FlashNoooooo");
        }
        if (HP <= 0 && life == true)
        {
            //Debug.LogError("Death");
            life = false;
            GsmeManeger.EnemyCount--;
            GetComponent<SpriteRenderer>().sprite = enemys[1];
            StartCoroutine("Dealete");
        }


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.tag == "trap")
        {
            // Damege Processing

            //HP -= collision.GetComponent<TrapController>().damage;
            TrapController tc = collision.GetComponent<TrapController>();
            traptype = tc.traptype;

            HP -= damageCheack(traptype, tc.damage);
            /*
            if(traptype == waketype)
            {
                HP -= collision.GetComponent<TrapController>().damage * 4;
            }else
            {
                HP -= collision.GetComponent<TrapController>().damage;
            }
            */
            
        }
        enemyRender.enabled = true;
        
        if(collision.tag == "bon") {
            HP -= HP*0.75f;
        }

    }
    IEnumerator FlashNoooooo()
    {
        enemyRender.enabled = false;
        yield return new WaitForSeconds(3);
        enemyRender.enabled = true;
    }IEnumerator Dealete()
    {
        
        //after 4 second Delete addObject
        yield return new WaitForSeconds(4);
        //GsmeManeger.EnemyCount--;
        Destroy(gameObject);
    }float rmd(float mix,float max)
    {
        return Random.Range(mix, max);
    }

    //damege cheack

    float damageCheack(TYPE damage_type,float damage_base)
    {
        // 
        float damage = damage_base;
        if(type_wake == damage_type) {
            damage *= 2;
        }
        /*
        if(type_strong == damage_type)
        {
            damage /= 2;
        }
        */
        return damage;
    }
    /*
    public void MoveEnemy()
    {
        target_t = target.GetComponent<Transform>();
        // プレイヤーの動きと完全一致して動く
        if (transform.position.x > target_t.position.x)
        {
            transform.Translate(-speed, 0, 0);
        }
        if (enemy_t.position.x < target_t.position.x)
        {
            transform.Translate(speed, 0, 0);
        }
        if (enemy_t.position.y > target_t.position.y)
        {
            transform.Translate(0, -speed, 0);
        }
        if (enemy_t.position.y < target_t.position.y)
        {
            transform.Translate(0, speed, 0);
        }
    }
    */
    
}

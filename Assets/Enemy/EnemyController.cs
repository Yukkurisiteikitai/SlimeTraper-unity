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

    Rigidbody2D rb;

    
    // hosei.
    Vector3 ResetPos = new Vector3();

    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();

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
        enemys[1] = enemydate.DataList[eN].sprite_damage;
        enemys[2] = enemydate.DataList[eN].sprite_dealete;
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

        if (Input.GetKeyDown(KeyCode.P))
        {
            MoveNavEnemy();
        }


        if (life == true&&go==true) {
            //Move 
            rb.AddForce(AddForcePower(target.transform.position, this.transform.position));



            //transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            //rb.AddForce(Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime) * -1);// balling
            //rb.MovePosition(target.transform.position * Time.deltaTime);
            //MoveEnemy();
            //StartCoroutine("Move");
        }

        timer += Time.deltaTime;

        if (timer > 2.5f && life == true) {
            //rb.velocity = Vector3.zero;
            enemyRender.sprite = enemys[0];

            timer = 0;
        }


        if(HP <= 3)
        {
            StartCoroutine("FlashNoooooo");
        }
        if (HP <= 0 && life == true)
        {
            //Debug.LogError("Death");
            life = false;
            GsmeManeger.EnemyCount--;
            GetComponent<SpriteRenderer>().sprite = enemys[2];
            StartCoroutine("Dealete");
        }
        else
        {
            //rb.velocity = Vector3.zero;
        }



        //OverSpace.
        /*
        if(transform.position.y > 8.5f|| transform.position.y < -7.6f|| transform.position.x < -17.5f|| transform.position.x > 17.5f)
        {
            RePlase();
        }*/

        //===================================================TEST-CODE===================================================.
        if (Input.GetKey(KeyCode.T))
        {
            //RightBody velecity zero
            if (Input.GetKeyDown(KeyCode.R))
            {
                rb.velocity = Vector3.zero;
            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                RePlase();
            }
        }
        //==============================================~END-TEST-CODE===================================================.
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.tag == "trap")
        {
            // Damege Processing.

            //HP -= collision.GetComponent<TrapController>().damage;
            TrapController tc = collision.GetComponent<TrapController>();
            traptype = tc.traptype;
            float damage = damageCheack(traptype, tc.damage);
            HP -= damage;
            GsmeManeger.TakenDamage += damage;
            rb.velocity *= 0.75f;
            enemyRender.sprite = enemys[1];
        }
        enemyRender.enabled = true;
        
        if(collision.tag == "bon") {
            HP -= HP*0.75f;
        }
        if (collision.tag == "enemy")
        {
            // hurue.
            float power = 2.2f;


            int a = Random.Range(0, 3);
            Vector3 g = new Vector3(0, 0, 0);
            int g_x = 0;
            int g_y = 0;
            //randomGo
            
            switch (a)
            {
                case 0://up.
                    g = new Vector3(0, 1, 0);
                    g_y = 1;
                    break;
                case 1://down.
                    g = new Vector3(0, -1, 0);
                    g_y = -1;
                    break;
                case 2://left.
                    g = new Vector3(-1, 0, 0);
                    g_x = -1;
                    break;
                case 3://right.
                    g = new Vector3(1, 0, 0);
                    g.x = 1;
                    break;
            }

            //g = new Vector3(g.x *power,g.y * power, power);
            g = new Vector3(g_x * power, g_y* power, power);
            //Debug.Log(g);

            rb.AddForce(g);
            //Debug.Log(rb.velocity);


        }

        //[Block] remove from sight.
        if (collision.tag == "wall")
        {
            rb.velocity = Vector3.zero;
            if (transform.position.y > 8.5f || transform.position.y < -7.6f || transform.position.x < -17.5f || transform.position.x > 17.5f)
            {
                RePlase();
            }
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        //rb.velocity = Vector3.zero;
    }
    //移動する方向をを取得する.
    Vector3 AddForcePower(Vector3 target_pos, Vector3 center_pos)
    {
        Vector3 temp_pos = target_pos - center_pos;

        //Change.
        if(temp_pos.x < 0)
        {
            temp_pos.x = -1;
        }else if(temp_pos.x > 0)
        {
            temp_pos.x = 1;
        }
        if (temp_pos.y < 0)
        {
            temp_pos.y = -1;
        }
        else if (temp_pos.y > 0)
        {
            temp_pos.y = 1;
        }
        temp_pos*= speed;

        return temp_pos;
    }


    // 体力減少時の点滅処理.
    IEnumerator FlashNoooooo()
    {
        enemyRender.enabled = false;
        yield return new WaitForSeconds(3);
        enemyRender.enabled = true;
    }
    //倒された際の処理.
    IEnumerator Dealete()
    {
        //after 4 second Delete addObject.
        yield return new WaitForSeconds(4);
        //GsmeManeger.EnemyCount--;
        GsmeManeger.DownEnemy++;
        Destroy(gameObject);
    }
    IEnumerator Move()
    {
        rb.AddForce(AddForcePower(target.transform.position, this.transform.position));

        //after 4 second Delete addObject.
        yield return new WaitForSeconds(2);
        //GsmeManeger.EnemyCount--;
        rb.velocity = Vector3.zero;
    }

    //Random Easy Do Method.
    float rmd(float mix,float max)
    {
        return Random.Range(mix, max);
    }

    //damege cheack.
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
    /*過去失敗したやつ.
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
    private void MoveNavEnemy()
    {
        //NavMesh TO MOVE
        
        int[,] DIRECTION = // 方向.
        {
            {0,1 },//UP.
            {1,0 },//Right.
            {0,-1 },//Down.
            {-1,0 }//left.
        };

        string[] Dr = {"UP","Right","Down","left"};


        for(int i = 0; i < DIRECTION.GetLength(0); i++)
        {
            Vector2 a = this.transform.position;


            //Vector2 test = a + new Vector2(DIRECTION[i, 0], DIRECTION[i, 1]);
            Vector2 test = Vector2Pluse(a, new Vector2(DIRECTION[i, 0], DIRECTION[i, 1]));

            Debug.Log(Dr[i]);
            Debug.Log(test); 
        }
    }

    Vector2 Vector2Pluse(Vector2 v_one,Vector2 v_two)
    {
        Vector2 answer = new Vector2();
        answer = new Vector2(v_one.x + v_two.x, v_one.y + v_two.y);
        return answer;
    }


    // hosei

    void RePlase()
    {
        
        if (transform.position.x < -17.5f)//leftOver.
        {
            ResetPos.x = -10.3f;

        }
        if (transform.position.x > 17.5f)//RightOver.
        {
            ResetPos.x = 10.3f;
        }
        if (transform.position.y < -7.6f)//DownOver.
        {
            ResetPos.y = -5;
        }
        if (transform.position.y > 8.5f)//UpOver.
        {
            ResetPos.y = 4;

        }
        transform.position = ResetPos;

    }


}

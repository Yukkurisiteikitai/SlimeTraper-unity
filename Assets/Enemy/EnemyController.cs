using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private TrapItem spia;
    public float damaga_e;
    [SerializeField] private EnemyItem enemydate;

    //public List<int> enemyList;

    public int enemyNumber = 0;
    public static int enemyListNumber = 0;
    private int enemyAll = GsmeManeger.EnemyCount;
    


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
    private bool life = true;

    public SpriteRenderer enemyRender;

    private float timer = 0;

    public Sprite[] enemys = new Sprite[2];

    private PlayerController target_script;

    private GsmeManeger gm;

    // Start is called before the first frame update
    void Start()
    {
        

        gm = GameObject.Find("GameManger").GetComponent<GsmeManeger>();
        if(enemyListNumber < enemyAll) {
            enemyNumber = gm.enemyList[enemyListNumber];
            enemyListNumber++;
        }


        target = GameObject.Find("slimeBase");
        //target_t = target.GetComponent<Transform>();
        enemy_t = GetComponent<Transform>();

        target_script = target.GetComponent<PlayerController>();


        damaga_e = spia.TrapList[0].damage;
        enemyRender = GetComponent<SpriteRenderer>();
        //enemyNumber = gm.enemyNumber;
        Debug.Log(gm);
        int eN = enemyNumber;//GsmeManeger.enemyNumber;
        Debug.Log("eN" +eN);

        HP = enemydate.DataList[eN].Attack;
        speed = enemydate.DataList[eN].speed;
        Debug.Log("enemyNumber" + eN);
        enemyRender.sprite = enemydate.DataList[eN].sprite_nomal;
        enemys[0] = enemydate.DataList[eN].sprite_nomal;
        enemys[1] = enemydate.DataList[eN].sprite_dealete;
        //enemyNumber = 0;
    }

    // Update is called once per frame
    void Update()
    {
        life = target_script.NowMove;
        if (life == true) {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);

            //MoveEnemy();
        }
        timer += Time.deltaTime;
        if(HP <= 3)
        {
            StartCoroutine("FlashNoooooo");
        }

        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "trap")
        {
            // Damege Processing
            HP -= collision.GetComponent<TrapController>().damage;
            if(HP <= 0&&life==true)
            {
                life = false;
                GsmeManeger.EnemyCount--;
                GetComponent<SpriteRenderer>().sprite = enemys[1];
                StartCoroutine("Dealete");
            }
        }
        enemyRender.enabled = true;
        

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

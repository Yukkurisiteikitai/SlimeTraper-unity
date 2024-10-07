using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    /* @bag =　バグの温床
     * 
     */
    //

    public GsmeManeger gm;

    [SerializeField] private TrapItem TrapDateBase;
    public GameObject Trap;
    public Text TrapCounter;
    public Image TrapImage;
    public int TrapHave;
    public int TrapNumber = 0;

    public int countGoo = 100;

    public float speed;
    public float interval;
    private float timer = 0;
    public int heart = 3;
    Transform slime_t;
    public bool life = true;

    public float movePoint;
    public bool NowMove;


    public bool NothingEnemy;

    private SpriteRenderer sliem_sprite;
    public Sprite[] slimes = new Sprite[4];

    public TextMeshProUGUI TrapCounter_text;


    
    private Animator slime_animator;

    private float takeInterval_Trap = 45;
    private float takeTrapTimer = 0;
    //public TextMesh TrapCounter_text;
    
    //public SpriteRenderer[] slime_heart = new SpriteRenderer[3];

    public GameObject[] slime_heart = new GameObject[3];
    //0= slime_stop,1=slime_move,2=slime_back,3=slime_death
    void Start()
    {
        slime_animator = GetComponent<Animator>();

        PrinteTrap();
        TrapImageChange(TrapNumber);
        sliem_sprite = GetComponent<SpriteRenderer>();
        sliem_sprite.sprite = slimes[0];
        slime_t = GetComponent<Transform>();
    }

    // Update is called once per frame.
    void Update()
    {
        //WaitMotion.
        timer += Time.deltaTime;
        takeTrapTimer += Time.deltaTime;

        if (timer == timer % interval) {
            
            SpriteChange(0);
        }
        if(takeTrapTimer >= takeInterval_Trap)
        {
            takeTrapTimer = 0;
            TrapHave++;
            PrinteTrap();
        }


        NowMove = false;
        //Move （領域制限版）.
        //if(入力ボタン&&〇〇から出ないで).

        if(life == true&& Input.anyKeyDown)
        {
            //slime_animator.SetBool("jump", false);
            if (Input.anyKeyDown)
            {
                Debug.Log("aa");
            }
            if (Input.GetKeyDown(KeyCode.W)|| Input.GetKeyDown(KeyCode.UpArrow))//up @bag
            {
                if(slime_t.position.y < 4)
                {
                    transform.Translate(0, speed, 0);
                    //SpriteChange(2);
                    slime_animator.SetInteger("state", 1);


                    TimerReset();
                    NowMove = true;
                }  
            }
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))//down @bag
            {
                if(slime_t.position.y > -5)
                {
                    transform.Translate(0, -speed, 0);
                    //SpriteChange(1);
                    slime_animator.SetInteger("state", 2);

                    TimerReset();
                    NowMove = true;
                }
            }
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))//right @bag
            {
                if(slime_t.position.x < 11)
                {
                    transform.Translate(speed, 0, 0);
                    //SpriteChange(1);
                    slime_animator.SetInteger("state", 0);

                    transform.localScale = new Vector3(-7.668242f, 7.668242f, 1);
                    TimerReset();
                    NowMove = true;
                }
            }
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))//left @bag
            {
                if(slime_t.position.x > -11) {
                    transform.Translate(-speed, 0, 0);
                    //SpriteChange(1);
                    slime_animator.SetInteger("state", 0);


                    transform.localScale = new Vector3(7.668242f, 7.668242f, 1);
                    TimerReset();
                    NowMove = true;
                }
            }
        }


        //SetingTrap
        if (Input.GetKeyDown(KeyCode.Space)&&TrapHave >= 1)
        {
            Vector3 I_t = slime_t.position;
            slime_t = GetComponent<Transform>();
            Instantiate(Trap,I_t,Quaternion.identity);
            TrapHave--;
            PrinteTrap();
            //slime_animator.SetBool("jump", true);
            StartCoroutine("Jump");
            GsmeManeger.UsedTrap++;
        }
        //TrapSetChange
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            //TrapChange
            TrapNumber++;
            if(TrapNumber > 3)//TrapNumberMax
            {
                TrapNumber = 0;
            }
            TrapImageChange(TrapNumber);
        }
        if (life == false)
        {
            Death();
        }

        //Tester
        if (Input.GetKey(KeyCode.T))
        {
            if (Input.GetKey(KeyCode.R))
            {
                if (Input.GetKey(KeyCode.A))
                {
                    if (Input.GetKey(KeyCode.P))
                    {
                        TrapHave += 100;
                    }
                }
            }
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "enemy")
        {
            SpriteChange(3);
            //Debug.Log("NOOOOOOOOOoh");
            Damege();
        }
    }
    void PrinteTrap() {
        //TrapCounter.text = TrapHave.ToString();
        ///TrapCounter_text.text = TrapHave.ToString();
        TrapCounter_text.text = TrapHave.ToString();
    }
    void SpriteChange(int number)
    {
        sliem_sprite.sprite = slimes[number];
    } void TimerReset()
    {
        timer = 0;
    }void TrapImageChange(int i)
    {
        TrapImage.sprite = TrapDateBase.TrapList[i].sprite;
    }

    //heartControll
    void Damege() {
        int health = heart - 1;
        Debug.Log("goo" + heart);
        if(heart < 1)
        {
            Debug.Log("Hello hall");
            life = false;
        }
        if(heart > 0&& NothingEnemy == false)
        {
            slime_heart[health].SetActive(false);
            heart -= 1;
            
        }

    }

    void Death() {
        
        //if(countGoo > 0){
            slime_t.Rotate(0,0, 5);
            slime_t.Rotate(0, 0, -4);
            countGoo--;
            gm.GameSet("over");
            
        //}
    }
    IEnumerator Jump()
    {
        slime_animator.SetBool("jump", true);
        yield return new WaitForSeconds(0.9f);
        slime_animator.SetBool("jump", false);
    }
    //slime_animator.SetBool("jump", true);
}

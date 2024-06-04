using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class TrapController : MonoBehaviour
{

    

    private PlayerController player;
    //[SerializeField] public Kind Trapkind;

    public int TrapNumber;
    [SerializeField] private TrapItem TrapDateBase;
    public float damage;
    public float slowly;
    public int HP;
    public bool setplayer;

    public TYPE traptype;

    public bool bootting  = false;

    public AudioClip sound;
    public AudioClip destorySound;

    public AudioSource adio;

    //save
    public SaveMapping saveMapping = new SaveMapping();
    public GameObject sM_trap;
    private bool saveStart = false;
    // Start is called before the first frame update
    void Start()
    {
        // 作動中
        bootting = true;
        //GetComponent<SpriteRenderer>().sprite = TrapDateBase.TrapList[(int)Trapkind].sprite;

        player = GameObject.Find("slimeBase").GetComponent<PlayerController>();
        TrapNumber = player.TrapNumber;

        //Download DataBase 
        Debug.Log(TrapDateBase.TrapList[TrapNumber].type);//cheak
        traptype = TrapDateBase.TrapList[TrapNumber].type;
        HP = TrapDateBase.TrapList[TrapNumber].HP;
        GetComponent<SpriteRenderer>().sprite = TrapDateBase.TrapList[TrapNumber].sprite;
        damage = TrapDateBase.TrapList[TrapNumber].damage;
        sound = TrapDateBase.TrapList[TrapNumber].Attacksound;
        //End Download
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "enemy")
        {
            adio.PlayOneShot(sound);
            HP--;
            if(HP <= 0&&bootting == true)
            {
                bootting = false;
                //KillTrap();
                StartCoroutine("KillTrap");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (saveStart) {
            Save();
            saveStart = false;
        }

    }
    
    IEnumerator KillTrap()
    {
        GetComponent<SpriteRenderer>().sprite = TrapDateBase.TrapList[TrapNumber].sprite_breack;
        adio.PlayOneShot(destorySound);
        //after 4 second Delete addObject
        yield return new WaitForSeconds(4);
        Destroy(gameObject);
    }
    // SaveStart
    public void Save() {
        // save Setting
        saveMapping.saveX = transform.position.x;
        saveMapping.saveY = transform.position.y;

        int changeX = (int)saveMapping.saveX;
        int changeY = (int)saveMapping.saveY;
        // 範囲y 4<-5 x-11 <11

        // change ren number
        if (changeX < 0)
        {
            changeX = 11 + changeX;

        }
        else if (changeX >= 0)
        {
            changeX += 11;
        }
        //
        if (changeY < 0)
        {
            changeY = (changeY * -1) + 4;
        }
        else if (changeY >= 0)
        {
            changeY = 4 - changeY;
        }

        saveMapping.saveCode = 3;
        /*test
        Debug.Log("X" + saveMapping.saveX);
        Debug.Log("Y" + saveMapping.saveY);
        Debug.Log("cX" + changeX);
        Debug.Log("cY" + changeY);
        Debug.Log("Cod" + saveMapping.saveCode);
        */
        //save End
        sM_trap = GameObject.Find("SaveManager");
        saveMapping.sM = sM_trap.GetComponent<SaveManager>();
        saveMapping.sM.InputTo(changeY, changeX, saveMapping.saveCode);
    }
}



public enum Kind
{
    spia = 0,
    poison = 1,
};

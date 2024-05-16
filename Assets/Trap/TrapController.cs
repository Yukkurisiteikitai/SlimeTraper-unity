using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public bool bootting = true;

    public AudioClip sound;
    public AudioClip destorySound;

    public AudioSource adio;

    // Start is called before the first frame update
    void Start()
    {
        bootting = true;
        //GetComponent<SpriteRenderer>().sprite = TrapDateBase.TrapList[(int)Trapkind].sprite;

        player = GameObject.Find("slimeBase").GetComponent<PlayerController>();
        TrapNumber = player.TrapNumber;

        Debug.Log(TrapDateBase.TrapList[TrapNumber].type);
        traptype = TrapDateBase.TrapList[TrapNumber].type;
        HP = TrapDateBase.TrapList[TrapNumber].HP;
        GetComponent<SpriteRenderer>().sprite = TrapDateBase.TrapList[TrapNumber].sprite;
        damage = TrapDateBase.TrapList[TrapNumber].damage;

        sound = TrapDateBase.TrapList[TrapNumber].Attacksound;
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
        
    }
    
    IEnumerator KillTrap()
    {
        GetComponent<SpriteRenderer>().sprite = TrapDateBase.TrapList[TrapNumber].sprite_breack;
        adio.PlayOneShot(destorySound);
        //after 4 second Delete addObject
        yield return new WaitForSeconds(4);
        Destroy(gameObject);
    }
}


public enum Kind
{
    spia = 0,
    poison = 1,
};

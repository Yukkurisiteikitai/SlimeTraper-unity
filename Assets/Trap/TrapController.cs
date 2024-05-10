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


    public AudioClip sound;
    public AudioSource adio;

    // Start is called before the first frame update
    void Start()
    {
        
        //GetComponent<SpriteRenderer>().sprite = TrapDateBase.TrapList[(int)Trapkind].sprite;

        player = GameObject.Find("slimeBase").GetComponent<PlayerController>();
        TrapNumber = player.TrapNumber;
        
        GetComponent<SpriteRenderer>().sprite = TrapDateBase.TrapList[TrapNumber].sprite;
        damage = TrapDateBase.TrapList[TrapNumber].damage;

        sound = TrapDateBase.TrapList[TrapNumber].Attacksound;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "enemy")
        {
            adio.PlayOneShot(sound);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
public enum Kind
{
    spia = 0,
    poison = 1,
};

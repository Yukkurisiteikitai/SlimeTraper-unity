using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateManager : MonoBehaviour
{
    public AudioClip Buttonsound;
    public AudioClip saveSound;
    public AudioSource adio;

    /*if=enemy
     * Shife = 0
    Gorst = 1
    Firedol = 2
    Joy = 3
    PixelBat = 4
    Wiser = 5
     */
    public Dropdown id_dropdown;
    /*Enemy = 0
     * Trap = 1
     * Player = 2
     * Terrain = 3
     */
    public Dropdown kind_dropdown;

    private int kind = 0;
    private int id = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SaveStart() {
        adio.PlayOneShot(saveSound);
    }

    public void PushButton()
    {
        adio.PlayOneShot(Buttonsound);
        kind = kind_dropdown.value;
        id = id_dropdown.value;
        //Debug.Log(id.ToString + "human" + kind.ToString);
    }
}

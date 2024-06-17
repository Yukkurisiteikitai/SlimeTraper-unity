using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateManager : MonoBehaviour
{
    

    public AudioClip Buttonsound;
    public AudioClip saveSound;
    public AudioSource adio;

    Vector3 screenPoint;

    [SerializeField] EnemyItem enemydatabase;
    public Image preview;

    public static bool NowStop = true;
    public static int nowID;

    public GameObject block_enemy;
    public GameObject block_terrain;
    public GameObject block_trap;
    public Transform block_player;
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
        NowStop = true;
        InputDropdown();
    }

    // Update is called once per frame
    void Update()
    {
        //ChageID
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                id++;
                ChangeDropDown();
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                id--;
                ChangeDropDown();
            }
        };
        // setting
        if (Input.GetMouseButtonDown(0)) {
            Debug.Log("fejfeo");
            this.screenPoint = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 a = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            //transform.position = Camera.main.ScreenToWorldPoint(a);
            switch (kind)
            {
                case 0:
                    Instantiate(block_enemy, Camera.main.ScreenToWorldPoint(a), Quaternion.identity);
                    break;
                case 1:
                    Instantiate(block_trap, Camera.main.ScreenToWorldPoint(a), Quaternion.identity);

                    break;
                case 2:
                    block_player.position = Camera.main.ScreenToWorldPoint(a);

                    break;
                case 3:
                    Instantiate(block_terrain, Camera.main.ScreenToWorldPoint(a), Quaternion.identity);
                    break;
            }





        }

        //
        nowID = id;

    }
    public void SaveStart() {
        adio.PlayOneShot(saveSound);
    }

    public void PushButton()
    {
        adio.PlayOneShot(Buttonsound);
        InputDropdown();
        if (kind == 0)
        {
            preview.sprite = enemydatabase.DataList[id].sprite_nomal;
        }
        //Debug.Log(id.ToString + "human" + kind.ToString);
    }
    private void InputDropdown()
    {
        kind = kind_dropdown.value;
        id = id_dropdown.value;
        Debug.Log("kind" + kind);
        Debug.Log("id" + id);
    }
    private void ChangeDropDown() {
        kind_dropdown.value = kind;
        id_dropdown.value = id;
    }

    
}

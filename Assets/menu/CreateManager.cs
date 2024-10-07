using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateManager : MonoBehaviour
{
    
    //Sound
    public AudioClip Buttonsound;
    public AudioClip saveSound;
    public AudioSource adio;

    
    Vector3 screenPoint;


    [SerializeField] EnemyItem enemydatabase;
    public Image preview;

    //Editor Mode Switch
    public static bool NowStop = true;

    //IDnumber-Teller
    public static int nowID;

    //IDnumber-Teller
    public static int nowKIND;

    //EditorBlock
    public GameObject block_enemy;
    public GameObject block_terrain;
    public GameObject block_trap;
    public Transform block_player;

    //Dropdown
    #region
    //==================================================.
    /*if=enemy
     * Shife = 0
    Gorst = 1
    Firedol = 2
    Joy = 3
    PixelBat = 4
    Wiser = 5
     */
    public Dropdown id_dropdown;
    //==================================================.
    /*Enemy = 0
     * Trap = 1
     * Player = 2
     * Terrain = 3
     */
    public Dropdown kind_dropdown;
    //==================================================.
    #endregion
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
            Debug.Log("Input-LeftClick");
            this.screenPoint = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 a = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

            //LogCameraPos(a);
            if (CheackInLine(a) == true)
            {
                //kind choice
                switch (kind)
                {
                    case 0://Enemy
                        Instantiate(block_enemy, Camera.main.ScreenToWorldPoint(a), Quaternion.identity);
                        break;
                    case 1://Trap
                        Instantiate(block_trap, Camera.main.ScreenToWorldPoint(a), Quaternion.identity);

                        break;
                    case 2://Player
                        block_player.position = Camera.main.ScreenToWorldPoint(a);

                        break;
                    case 3://Terrain
                        Instantiate(block_terrain, Camera.main.ScreenToWorldPoint(a), Quaternion.identity);
                        break;
                }
            }
            
        }

        //Update
        nowID = id;
        nowKIND = kind;
    }

    bool CheackInLine(Vector3 pos)
    {
        if (pos.x <= 1885 && pos.x >= 28)
        {
            if (pos.y < 880)
            {
                return true;
            }
        }
        return false;
    }

    

    //Save
    public void SaveStart() {
        adio.PlayOneShot(saveSound);
    }


    //Debug
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

    //DropDown
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


    //@Debug
    void LogCameraPos(Vector3 a)
    {
        Debug.Log(a);
        Debug.Log(Camera.main.ScreenToWorldPoint(a));
    }

}

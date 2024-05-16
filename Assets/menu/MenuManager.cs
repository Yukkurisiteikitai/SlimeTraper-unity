using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Transform startCircle;
    public float speed;

    public RectTransform startButtne;
    public Transform selectButtne;


    

    private bool changeSelect = false;
    bool goSelect = false;

    


    private bool go = false;

    public static int menuStageNumber;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(startCircle.localScale.x < 21.97f&&go == true)
        {
            startCircle.localScale += new Vector3(speed * Time.deltaTime, speed * Time.deltaTime, 0);
            
        }if(startCircle.localScale.x > 21.97f){
            //GoGame();
            changeSelect = true;
        }
        if (changeSelect == true)
        {
            if(startButtne.position.y < 1800.35f)
            {
                //startButtne.position += new Vector3(0, speed, 0);
                startButtne.position += new Vector3(0, speed * 15 * Time.deltaTime, 0);
                
            }if(startButtne.position.y > 1800.35f)
            {
                Debug.Log("goSelect=true");//@debug
                goSelect = true;
            }
        }
        if (goSelect == true)
        {
            if (selectButtne.position.y < 250.1f)
            {
                selectButtne.position += new Vector3(0, speed * 15 * Time.deltaTime, 0);
            }
        }




    }
    public void PrickGo() {
        go = true;
    }public void GoGame(int number) {
        menuStageNumber = number;
        SceneManager.LoadScene("GmeScene");
        
    }


    public void UpSelect()
    {
        selectButtne.position += new Vector3(0,speed * Time.deltaTime,0);
    }
    public void GoToSence(string name)
    {
        SceneManager.LoadScene(name);
    }
    public void FixedUpdate()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

public class TutorialController : MonoBehaviour
{
    [Header("Master Setting")]
    [SerializeField] private TuatrialTexter Character;
    public charactor nowSpeaeker;
    public int chapter = 1;
    //The number of help pages seen increases with each chapter.

    [Header("helpPage Setting")]
    public GameObject helpPage;
    public bool nowTutorial;
    public int helpPageNumber = 1;
    public List<GameObject> helpPageS;
    public GameObject navigateKey;
    public GameObject navigateLight;
    private RectTransform navigateLight_RectTransform;
    public Vector2[] navigateLight_PosSetList;
    /*0= ButtonPos
     *1= HatenaPos
     */

    [Header("InputDates&DateBox")]
    public GameObject slime;
    private Transform slime_transform;
    private int slime_heart;

    

    /*helpPageNumber
     * 1 = sousahouhou操作方法
     * [wasd] or [←↑↓→] is move.
     * Shift is Change Trap
     * Space is Set Trap
     * click is State print Scouter 
     * 2 = menu  メニュー
     * 
     * 3 = win or lose What if勝利・敗北/条件
     * win
     * enemy Counter = 0
     *  
     * lose
     * heart = 0
     */


    /*
     * 
     * ここから必要な要素
     * 移動についての説明
     * メニューの説明ok
     * みんなの説明
     * win if 
     * lose if
     * 
     */



    /*
     *  手順
     *  右に移動（dまたは→）
     *  if x < 30{//目的の位置 
     *      if(hp < 3){//get damage right print helpMenu page 2(menu page)
     *          HelpPageNumberPrintup(2);
     *      }
     *  }else{
     *   chapter = 2
     *  }
     *  
     *  
     *
     */
    // Start is called before the first frame update
    void Start()
    {
        if (nowTutorial)
        {
            Debug.Log(navigateLight_PosSetList[0]);
            ActiveObjectSetting(navigateKey, true);
            helpPage.SetActive(false);

            for(int i = 0; i <helpPageS.Count; i++)
            {
                helpPageS[i].SetActive(false); 
            }
            slime_transform = slime.GetComponent<Transform>();
            slime_heart = slime.GetComponent<PlayerController>().heart;
            navigateLight_RectTransform = navigateLight.GetComponent<RectTransform>();
            Debug.Log(navigateLight_RectTransform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (nowTutorial)
        {
            switch (chapter)
            {
                case 1://move.       
                    if (slime_transform.position.x < 2)
                    {//目的の位置.
                        if (slime_heart < 3)
                        {//get damage right print helpMenu page 2(menu page).
                            HelpPageNumberPrintup(1);
                        }
                    }
                    else
                    {
                        ActiveObjectSetting(navigateKey, false);
                        Debug.Log("Go to GOOOOOOOOOOOL" + "chapter" + chapter);
                        HelpPageNumberPrintup(0);
                        ActiveObjectSetting(navigateLight, true);
                        navigateLightSetPoint(navigateLight_PosSetList[0]);
                        chapter = 2;
                    }
                    break;
                case 2://menu. 
                    break;
                case 3://win or lose.
                    navigateLightSetPoint(navigateLight_PosSetList[1]);

                    break;
                case 4://tutorial claer.
                    break;
            }
        }
    }


    public void OChelpPage()//Used  helpPage in button.
    {
        if (nowTutorial)
        {
            if (helpPage.activeSelf)
            {
                if (chapter == 3)
                {
                    ActiveObjectSetting(navigateLight, false);
                    chapter = 4;
                }
                helpPage.SetActive(false);
                
            }
            else
            {
                helpPage.SetActive(true);
                helpPageS[helpPageNumber].SetActive(true);
            }
        }
        
    }
    public void PageUpHelpPage()//HelpPage (page up)[Used Page Up Button].
    {
        if (nowTutorial)
        {
            helpPageS[helpPageNumber].SetActive(false);
            if (chapter == 2 && helpPageNumber == 1)
            {
                chapter = 3;
            }
            helpPageNumber++;
            

            if (helpPageNumber >= helpPageS.Count)       
            {
                helpPageNumber = 0;
            }
             
            helpPageS[helpPageNumber].SetActive(true);
        }
        
    }

    private void HelpPageNumberPrintup(int number)//特定のページのヘルプを表示するための関数.
    {
        if (nowTutorial)
        {
            OChelpPage();
            helpPageS[helpPageNumber].SetActive(false);
            helpPageNumber = number;
            helpPageS[helpPageNumber].SetActive(true);
        }
    }
    private void ActiveObjectSetting(GameObject obj,bool settings)
    {
        obj.SetActive(settings);
    }
    private void navigateLightSetPoint(Vector2 pos)
    {
        navigateLight_RectTransform.anchoredPosition = pos;
    }
}
public enum charactor
{
    slime, narrator
}
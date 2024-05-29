using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
[System.Serializable]
public class Data
{
    public List<int> MapLine_First;
    public List<int> MapLine_Second;
    public List<int> MapLine_Third;
    public List<int> MapLine_Fourth;
    public List<int> MapLine_Fifth;
    public List<int> MapLine_Sixth;
    public List<int> MapLine_Seventh;
    public List<int> MapLine_Eighth;
    public List<int> MapLine_Ninth;
    public List<int> MapLine_Tenth;
}*/
public class SaveManager : MonoBehaviour
{
    public　static int[,] Map_saveList = new int[10,23]{
    {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
    {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}
    };
    

//public List<GameObject> saverLine = new List<GameObject>(10);   

    void Start()
    {
        //Data savedata = new Data();
        Debug.Log(SaveManager.Map_saveList);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CheackMapSave();
        }
    }


    void CheackMapSave()
    {
        for (int y = 0;y < 10; y++){
            for (int x = 0; x < 23; x++)
            {
                Debug.Log(y.ToString() + ',' + x.ToString() + '＝' + Map_saveList[y, x].ToString());
            }
        }
    }
}
public class SaveMapping{
    public int saveCode;
    public int saveX;
    public int saveY;

}

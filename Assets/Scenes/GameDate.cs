using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]public class GameDateBase
{
    public string id;
    public Sprite background;
}


[CreateAssetMenu(fileName = "GameDate", menuName = "GameDate/GameDateBase")]
public class GameDate : ScriptableObject
{
    [SerializeField] StageDate map;
    public List<GameDateBase> stagelist;
}

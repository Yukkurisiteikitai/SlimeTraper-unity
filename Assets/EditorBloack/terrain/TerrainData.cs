using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;


[Serializable] public class TerrainBasic {
    public Sprite terrainSprite;
    public int StopGo;
    /*0 = 誰でも通れる   
     * 1＝プレイヤー＋投擲物通れる
     * 2=誰も通れない
     */
}

[CreateAssetMenu(fileName = "TerrainDate", menuName = "GameDate/TrrainDateBase")]
public class TerrainData : ScriptableObject
{
    public List<TerrainBasic> TerrainList;
}

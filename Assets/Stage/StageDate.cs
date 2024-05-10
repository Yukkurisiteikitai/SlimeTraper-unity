using System;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable] public class StageValueX
{
    public List<int> xValue;
}[Serializable] public class StageValueY
{
    public List<StageValueX> yValue;
}

[CreateAssetMenu(fileName = "StageDate", menuName = "GameDate/StageDateBase")]  
public class StageDate : ScriptableObject{
    public List<StageValueY> stageList;
}
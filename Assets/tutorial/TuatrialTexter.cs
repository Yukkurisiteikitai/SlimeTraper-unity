using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]public class TuatrialTextBasic
{
    public charactor speacker;
    public string text;
}
[CreateAssetMenu(fileName = "TuatrialTextDateBase", menuName = "GameDate/TuatrialTextDateBase")]
public class TuatrialTexter : ScriptableObject
{
    public List<TuatrialTextBasic> DateList;
}
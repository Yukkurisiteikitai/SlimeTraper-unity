using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable] public class TrapBasicDate
{
    public string Trap_name;
    public int damage;
    public AudioClip Attacksound;
    public float slowly;
    public Sprite sprite;
}
[CreateAssetMenu(fileName = "TrapData", menuName = "GameDate/TrapData")]
public class TrapItem : ScriptableObject
{
    public List<TrapBasicDate> TrapList;
}
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
    public Sprite sprite_breack;

    public TYPE type;
    public effect effect;
    public int HP;
    
}
[CreateAssetMenu(fileName = "TrapData", menuName = "GameDate/TrapData")]
public class TrapItem : ScriptableObject
{
    public List<TrapBasicDate> TrapList;
}
public enum TYPE
{
    fire,spia,water,poison,wind
};public enum effect {
    slow,poison,stop,slashDamage
};

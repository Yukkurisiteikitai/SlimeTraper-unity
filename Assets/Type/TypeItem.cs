using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TypeBase
{
    
    public string Type_name;// spotLight
    public TYPE myType;
    public List<TYPE> wakeType;
    /*
    public int damage;
    public AudioClip Attacksound;
    public float slowly;
    public Sprite sprite;
    public Sprite sprite_breack;

    public TYPE type;
    public effect effect;
    public int HP;
    */
    
}
[CreateAssetMenu(fileName = "TypeData", menuName = "GameDate/TypeData")]
public class TypeItem : ScriptableObject
{
    public List<TypeBase> TypeList;
}
/*
public enum TYPE
{
    fire, spia, water, poison, wind
}; public enum effect
{
    slow, poison, stop, slashDamage
};
*/
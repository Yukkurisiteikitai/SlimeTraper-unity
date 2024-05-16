using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable] public class EnemyData{
    public string Id;
    public int Hp;
    public int Attack;
    public int Defense;
    public int speed;
    public int Exp;
    public Sprite sprite_nomal;
    public Sprite sprite_damage;
    public Sprite sprite_dealete;

    public TYPE wakeType;
    public TYPE strongType;
}
[CreateAssetMenu(fileName = "EnemyDate", menuName = "GameDate/EnemyDateBase")]
public class EnemyItem : ScriptableObject
{
    public List<EnemyData> DataList;
}
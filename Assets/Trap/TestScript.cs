using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    [Header("ジョブを選択してください")]
    public JobList job;  //列挙型の値を格納する変数
}

//列挙型の定義
public enum JobList
{
    Soldier, Shielder, Archer, Berserker
}

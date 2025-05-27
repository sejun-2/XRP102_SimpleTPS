using CustomUtility.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [field: SerializeField] private CsvTable MonsterCSV { get; set; }
    [field: SerializeField] private CsvDictionary MonsterDic { get; set; }

    private void Awake()
    {
        Init();
    }


    private void Init()
    {
        CsvReader.Read(MonsterCSV);
        CsvReader.Read(MonsterDic);
    }

    //private void OnBeforeTransformParentChanged()
    //{
    //    Debug.Log(_monsterCSV.GetData(1, 1));
    //}

    public enum MonsterData
    {
        Name = 1,
        Atk,
        Dfe,
        Spd,
        Dsc
    }

}

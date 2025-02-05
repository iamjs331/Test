using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PWKeyManager : MonoBehaviour
{
    Dictionary<int, string> PcData;
    public InputField PW;

    void Start()
    {
        PcData = new Dictionary<int, string>();
        GeneratePcDate();

    }
    public bool GetPc(int id)
    {
        if (PW.text == PcData[id])
            return true;
        else
            return false;
    }


    void GeneratePcDate()
    {
        PcData.Add(301, "1017");
    }
}

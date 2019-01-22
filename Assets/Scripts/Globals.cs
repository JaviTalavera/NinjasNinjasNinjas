using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class Globals {

    public static void WriteLog(string sms)
    {
        GameObject.Find("tbLog").GetComponent<Text>().text += "\n" + sms;
    }
}

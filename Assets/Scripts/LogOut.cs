using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LogOut : MonoBehaviour
{
    private static LogOut instance = null;
    private string icpPth = "Assets/ResultLogs/";
    private int n = 0;

    //public InputField InputField;
    // Start is called before the first frame update
    void Start()
    {
        n = 0;
        do
        {
            if (false == File.Exists(icpPth + "ThisIsLog_" + n.ToString() + ".txt"))
            {
                var file = File.CreateText(icpPth + "ThisIsLog_" + n.ToString() + ".txt");
                file.Close();
                break;
            }
            else n++;
        } while (true);

        //StreamWriter sw = new StreamWriter(icpPth + InputField.text + "ThisIsLog_" + n.ToString() + ".txt");
        //sw.WriteLine(Log);
        //sw.WriteLine("dataa");
        //sw.Flush();
        //sw.Close();
    }

    public void WriteLog(string Log)
    {
        StreamWriter sw = new StreamWriter(icpPth + "ThisIsLog_" + n.ToString() + ".txt");
        sw.WriteLine(Log);
        //sw.WriteLine("dataa");
        sw.Flush();
        sw.Close();
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public static LogOut Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A)) WriteLog("Hello, world! \n Hello World!");
        //if(Input.GetKeyDown(KeyCode.Z)) 
    }
}

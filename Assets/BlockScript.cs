using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockScript : MonoBehaviour
{
    public Text sTextNum;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetText(string str)
    {
        this.sTextNum.text = str;
    }
    public void SetText(int num)
    {
        if(num != 0)
        {
            this.sTextNum.text = "" + num;
        }
        else
        {
            this.sTextNum.text = "";
        }
    }
}

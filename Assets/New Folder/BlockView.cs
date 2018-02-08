using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockView : MonoBehaviour
{
    //[System.Serializable]
    //public struct BlockFace //用來記錄每個數字級別對應到什麼顏色的結構
    //{
    //    public int iLevel;
    //    public Color colorBackgroundColor;
    //}

    //[SerializeField] private LevelColer[] m_arrLevelColor;  //每個數字級別分別該有顏色，存在該陣列裡
    [SerializeField] private Text m_textNum; //外頭顯示的字幕
    private int m_iNum; //裡面儲存的數值
    private Image m_imgBlockFace; //格子的外觀

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetText(string _sText)
    {
        this.m_textNum.text = _sText;
    }
    public void SetText(int _iNum)
    {
        this.m_iNum = _iNum;
        //this.ChangeColor();
        if (_iNum != 0)
        {
            this.m_textNum.text = "" + _iNum;
        }
        else
        {
            this.m_textNum.text = "";
        }
    }
    public void SetText(BlockData _data)
    {
        if(_data.GetBlockType() == BlockData.EBlockType.Normal) //只有當我發現你是普通格子時，我才會去更動我的數字
        {
            this.SetText(_data.GetBlockValue());
        }
    }
    //public void ChangeColor()
    //{
    //    foreach (LevelColer levelColor in arrLevelColor)
    //    {
    //        if (this.iNum >= levelColor.iLevel)
    //        {
    //            this.GetComponent<Image>().color = levelColor.cBackgroundColor;
    //        }
    //    }
    //}
}

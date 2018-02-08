using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockData
{
    public enum EBlockType //格子種類
    {
        Normal, //普通
        Obstruct, //障礙物(不可越過)
        None //沒東西(類似障礙物，但可以越過)
    }
    private EBlockType m_eBlockType; //自己的格子種類
    private int m_iBlockValue; //自己所帶的數值

    public BlockData(EBlockType _eBlockType , int _iBlockValue)
    {
        this.m_eBlockType = _eBlockType;
        this.m_iBlockValue = _iBlockValue;
    }
    public BlockData(EBlockType _eBlockType)
        :this(_eBlockType , 0) //假如只給格子種類的話就預設數值為0
    {
    }
    public BlockData()
        :this(EBlockType.Normal , 0) //假如什麼都不設定就假設自己是普通格子，且數值為0
    {
    }
    
    public void PlusOn(BlockData _anotherBlock)
    {
        this.m_iBlockValue += _anotherBlock.m_iBlockValue;
        _anotherBlock.m_iBlockValue = 0;
    }

    public void SetBlockType(EBlockType _newType)
    {
        this.m_eBlockType = _newType;
    }
    public EBlockType GetBlockType()
    {
        return this.m_eBlockType;
    }
    //我感覺如果只是這樣寫的話，那似乎也不用get和set，所以有些猶豫
    public void SetBlockValue(int _iNewValue)
    {
        this.m_iBlockValue = _iNewValue;
    }
    public int GetBlockValue()
    {
        return this.m_iBlockValue;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamingLogic2048
{
    private List<BlockData> m_listBoard; //整個棋盤
    private int m_iScore; //分數
    private int m_iLengthX, m_iLengthY; //X軸邊長，和Y軸邊長
    private int m_iBoardSize; //整個棋盤的大小

    public GamingLogic2048(int _iLengthX, int _iLengthY, int _iScore) //最完善的建構子
    {
        this.m_iLengthX = _iLengthX;
        this.m_iLengthY = _iLengthY;
        this.m_iScore = _iScore;
        this.m_iBoardSize = m_iLengthX * m_iLengthY;
        this.m_listBoard = new List<BlockData>();

        for (int i = 0; i < m_iBoardSize; i++)
        {
            m_listBoard.Add(new BlockData());
        }
    }
    public GamingLogic2048() //最基本的遊戲開局
        : this(4, 4, 0)
    {
    }

    public void SpawnBlock(int _iSpawnPos, BlockData _block) //生成格子
    {
        if (this.m_listBoard != null && _iSpawnPos < this.m_iBoardSize)//只在棋盤有被生出來，而且你選定的索引值在範圍內時才允許生成
        {
            this.m_listBoard[_iSpawnPos] = _block;
        }
    }

    public void SpawnRandomBlock(int _iSpawnNum)
    {
        List<BlockData> listEmptyBlock = new List<BlockData>(); //用來記憶哪些格子是空(0)的
        for (int i = 0; i<m_iBoardSize; i++)
        {
            if (m_listBoard[i].GetBlockType() == BlockData.EBlockType.Normal && m_listBoard[i].GetBlockValue() == 0) //只去找普通格子，判斷其是否為0
            {
                listEmptyBlock.Add(m_listBoard[i]);
            }
        }
        for (int i = 0; i < _iSpawnNum && listEmptyBlock.Count > 0 ; i++) //listEmptyBlock.Count > 0的用意是要檢查還有沒有空(為0)的格子
        {
            int iRandomValue = Random.Range(1, 3) * 2;
            int iBlockChose = Random.Range(0, listEmptyBlock.Count);
            listEmptyBlock[iBlockChose].SetBlockValue(iRandomValue);
            listEmptyBlock.RemoveAt(iBlockChose); //在設定完亂數後，將該選定之格子踢出列表，下一輪亂數就不會再出現了
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamingLogic2048
{
    public enum EPushingDirection
    {
        Left,
        Right,
        Up,
        Down
    }

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
        this.SpawnRandomBlock(2);
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
        for (int i = 0; i < m_iBoardSize; i++)
        {
            if (m_listBoard[i].GetBlockType() == BlockData.EBlockType.Normal && m_listBoard[i].GetBlockValue() == 0) //只去找普通格子，判斷其是否為0
            {
                listEmptyBlock.Add(m_listBoard[i]);
            }
        }
        for (int i = 0; i < _iSpawnNum && listEmptyBlock.Count > 0; i++) //listEmptyBlock.Count > 0的用意是要檢查還有沒有空(為0)的格子
        {
            int iRandomValue = Random.Range(1, 3) * 2;
            int iBlockChose = Random.Range(0, listEmptyBlock.Count);
            listEmptyBlock[iBlockChose].SetBlockValue(iRandomValue);
            listEmptyBlock.RemoveAt(iBlockChose); //在設定完亂數後，將該選定之格子踢出列表，下一輪亂數就不會再出現了
        }
    }

    public List<List<BlockData>> GetPushingList(EPushingDirection _ePushingDirection) //獲得正要進行 "壓縮" 的行線群，所以是list(行線)的list(群)
    {
        List<List<BlockData>> listPushing = new List<List<BlockData>>();
        switch (_ePushingDirection)
        {
            case EPushingDirection.Left:
                for (int i = 0; i < m_iLengthY; i++)
                {
                    List<BlockData> listPushingLine = new List<BlockData>();
                    for (int j = 0; j < m_iLengthX; j++)
                    {
                        int iChosen = (i * m_iLengthX) + j;
                        listPushingLine.Add(m_listBoard[iChosen]);
                    }
                    listPushing.Add(listPushingLine);
                }
                break;
            case EPushingDirection.Right:
                for (int i = 0; i < m_iLengthY; i++)
                {
                    List<BlockData> listPushingLine = new List<BlockData>();
                    for (int j = 0; j < m_iLengthX; j++)
                    {
                        int iChosen = ((i + 1) * m_iLengthX - 1) - j;
                        listPushingLine.Add(m_listBoard[iChosen]);
                    }
                    listPushing.Add(listPushingLine);
                }
                break;
            case EPushingDirection.Up:
                for (int i = 0; i < m_iLengthX; i++)
                {
                    List<BlockData> listPushingLine = new List<BlockData>();
                    for (int j = 0; j < m_iLengthY; j++)
                    {
                        int iChosen = i + (j * m_iLengthX);
                        listPushingLine.Add(m_listBoard[iChosen]);
                    }
                    listPushing.Add(listPushingLine);
                }
                break;
            case EPushingDirection.Down:
                for (int i = 0; i < m_iLengthX; i++)
                {
                    List<BlockData> listPushingLine = new List<BlockData>();
                    for (int j = 0; j < m_iLengthY; j++)
                    {
                        int iChosen = i + ((m_iLengthY - 1) - j) * m_iLengthX;
                        listPushingLine.Add(m_listBoard[iChosen]);
                    }
                    listPushing.Add(listPushingLine);
                }
                break;
        }
        return listPushing;
    }

    public void Pushing(EPushingDirection _ePushingDirection) //壓縮動作，只需將 "方向" 帶入
    {
        List<List<BlockData>> listPushing = this.GetPushingList(_ePushingDirection);
        foreach(List<BlockData> listPushingLine in listPushing) //挑出行線群的其中一條
        {
            int iLineLength = listPushingLine.Count;
            for (int i = 0; i < iLineLength; i++)//再針對該行線中的每個block做處理
            {
                for (int j = i + 1; j < iLineLength; j++)
                {

                }
            }
        }
    }

    public int GetBoardSize()
    {
        return this.m_iBoardSize;
    }
    public List<BlockData> GetBoardData()
    {
        return this.m_listBoard.GetRange(0, m_listBoard.Count); //回傳list本身的複製品，因為要避免別人透過Get來直接更動list中的值
    }
    public int GetScore()
    {
        return this.m_iScore;
    }
}

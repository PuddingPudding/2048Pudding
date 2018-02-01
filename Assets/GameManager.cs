using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int iLength = 4; //棋盤邊長
    public GameObject blockPrefab; //每個格子的遊戲物件原型
    public Vector2 v2BlockInterval = new Vector2(60, 60); //每個格子物件間的間隔
    public Canvas cUICanvas;
    public Transform tInitPosition; //第一個生成格子物件的位子

    private List<int> iListCheckBoard; //用來紀錄整個2048棋盤中每一格數字的串列
    private List<BlockScript> listBlockScript;
    // Use this for initialization
    void Start()
    {
        InitGame();
        UpdateGame();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow) )
        {
            this.PushLeft();
            this.SpawnBlock(1);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            this.PushRight();
            this.SpawnBlock(1);
        }
        UpdateGame();
    }

    public void InitGame()
    {
        iListCheckBoard = new List<int>();
        listBlockScript = new List<BlockScript>();
        for (int i = 0; i < (iLength * iLength); i++)
        {
            iListCheckBoard.Add(0);

            Vector3 initPlace = tInitPosition.transform.localPosition;
            initPlace.x += v2BlockInterval.x * (i % iLength);
            initPlace.y -= v2BlockInterval.y * (i / iLength);
            GameObject blockTemp = Instantiate(blockPrefab, initPlace, blockPrefab.transform.rotation);
            blockTemp.transform.parent = cUICanvas.transform;
            blockTemp.transform.localPosition = initPlace;
            BlockScript BSTemp = blockTemp.GetComponent<BlockScript>();
            listBlockScript.Add(BSTemp);
        }
        Debug.Log(iListCheckBoard.Count);
        SpawnBlock(2);
    }

    public void SpawnBlock(int iSpawnNum)
    {
        for (int i = 0; i < iSpawnNum; i++)
        {
            int iBlockNum = Random.Range(1, 3) * 2;
            int iBlockChose = Random.Range(0, iListCheckBoard.Count);
            bool canSpawn = false;
            for (int j = 0; j < iListCheckBoard.Count && !canSpawn; j++)
            {
                if (iListCheckBoard[j] == 0)
                {
                    canSpawn = true; //如果陣列裡還有空值(0)的話，就設定為可以生成
                }
            }
            while (iListCheckBoard[iBlockChose] != 0 && canSpawn) //一直跑回圈檢察，直到隨機挑出一個還未有數字(0)的空格為止
            {
                iBlockChose = Random.Range(0, iListCheckBoard.Count);
            }
            if(canSpawn)
            {
                iListCheckBoard[iBlockChose] = iBlockNum;
            }            
        }
    }

    public void UpdateGame()
    {
        for (int i = 0; i < iListCheckBoard.Count; i++)
        {
            listBlockScript[i].SetText(iListCheckBoard[i]);
        }
    }

    public void PushLeft()
    {
        int checkingBlock; //正在受檢查的格子索引值
        int nextBlock; //在他之後的其他格子之索引值        
        for (int i = 0; i<iLength;i++)
        {
            for(int j = 0; j<iLength-1;j++)
            {
                checkingBlock = (i * iLength) + j; //先選定要開始受檢查的基準點
                nextBlock = checkingBlock + 1;
                while(iListCheckBoard[nextBlock] == 0 && (nextBlock)< ( (i+1) * iLength -1)) //( (i+1) * iLength -1)為現今檢察之行線的最後一位
                {
                    nextBlock++;//一直往旁邊找，直到找到一個非空(不是0)的格子，或著找到底(下一個會超出該行)了
                }
                if(iListCheckBoard[nextBlock] != 0) //找到的那一個不為0
                {
                    if (iListCheckBoard[nextBlock] == iListCheckBoard[checkingBlock] || iListCheckBoard[checkingBlock] == 0)//那檢察是否兩數字一樣，或著現在的這一個沒有數字(0)
                    {
                        iListCheckBoard[checkingBlock] += iListCheckBoard[nextBlock];
                        iListCheckBoard[nextBlock] = 0;
                    }
                    else if(iListCheckBoard[nextBlock] != iListCheckBoard[checkingBlock])//若不一樣，把後面那個拉到身旁
                    {
                        iListCheckBoard[checkingBlock+1] = iListCheckBoard[nextBlock];
                        if(nextBlock != checkingBlock+1) //在一般情況下，假如有拉過來就會把原先的格子設為0，但如果他本來就在我旁邊，那就不需要
                        {
                            iListCheckBoard[nextBlock] = 0;
                        }
                    }
                }
            }            
        }
    }

    public void PushRight()
    {
        int checkingBlock; //正在受檢查的格子索引值
        int nextBlock; //在他之後的其他格子之索引值        
        for (int i = 0; i < iLength; i++)
        {
            for (int j = 0; j < iLength - 1; j++)
            {
                checkingBlock = ((i + 1) * iLength - 1) - j; //先選定要開始受檢查的基準點
                nextBlock = checkingBlock - 1;
                while (iListCheckBoard[nextBlock] == 0 && (nextBlock) > (i * iLength)) //(i * iLength)為現今檢察之行線的最後一位
                {
                    nextBlock--;//一直往旁邊找，直到找到一個非空(不是0)的格子，或著找到底(在往左一個會超出該行)了
                }
                if (iListCheckBoard[nextBlock] != 0) //找到的那一個不為0
                {
                    if (iListCheckBoard[nextBlock] == iListCheckBoard[checkingBlock] || iListCheckBoard[checkingBlock] == 0)//那檢察是否兩數字一樣，或著現在的這一個沒有數字(0)
                    {
                        iListCheckBoard[checkingBlock] += iListCheckBoard[nextBlock];
                        iListCheckBoard[nextBlock] = 0;
                    }
                    else if (iListCheckBoard[nextBlock] != iListCheckBoard[checkingBlock])//若不一樣，把後面那個拉到身旁
                    {
                        iListCheckBoard[checkingBlock - 1] = iListCheckBoard[nextBlock];
                        if (nextBlock != checkingBlock - 1) //在一般情況下，假如有拉過來就會把原先的格子設為0，但如果他本來就在我旁邊，那就不需要
                        {
                            iListCheckBoard[nextBlock] = 0;
                        }
                    }
                }
            }
        }
    }
}

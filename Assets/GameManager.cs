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
        if (Input.GetKeyDown(KeyCode.LeftArrow))
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
            if (canSpawn)
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
        int iComparing; //正在受比對的格子索引值
        int iPolling; //輪尋格子的索引值
        bool pollFinish = false; //這一波輪尋是否已結束
        for (int i = 0; i < iLength; i++)
        {
            for (int j = 0; j < iLength; j++)
            {
                pollFinish = false;
                iComparing = (i * iLength) + j;
                iPolling = iComparing + 1;
                while (!pollFinish)
                {
                    if (iPolling >= (i + 1) * iLength)//若輪尋格子超出了目前的這條行線
                    {
                        pollFinish = true;
                    }
                    else if (iListCheckBoard[iPolling] != 0)//若輪尋格子有東西(不為0)
                    {
                        if (iListCheckBoard[iComparing] == 0)//但受比對的格子是空的(0)的話，那就直接把數字搬過來
                        {
                            iListCheckBoard[iComparing] = iListCheckBoard[iPolling];
                            iListCheckBoard[iPolling] = 0;
                        }
                        else //其餘狀況，也就是當受比對的格子有東西(不為0)
                        {
                            if (iListCheckBoard[iComparing] == iListCheckBoard[iPolling])//若比對者與輪尋者皆同，兩者合併
                            {
                                iListCheckBoard[iComparing] += iListCheckBoard[iPolling];
                                iListCheckBoard[iPolling] = 0;
                            }
                            else if(iListCheckBoard[iComparing+1] == 0)//若比對者的隔壁沒東西(0)，那就把輪尋到的那個數字直接搬過來
                            {
                                iListCheckBoard[iComparing + 1] += iListCheckBoard[iPolling];
                                iListCheckBoard[iPolling] = 0;
                            }
                            pollFinish = true;
                        }
                    }
                    else//其餘狀況，也就是當輪尋格子沒東西(0)，且輪尋值也還未到底時
                    {
                        iPolling++;
                    }
                }
            }
        }
    }

    public void PushRight()
    {
        int iComparing; //正在受比對的格子索引值
        int iPolling; //輪尋格子的索引值
        bool pollFinish = false; //這一波輪尋是否已結束
        for (int i = 0; i < iLength; i++)
        {
            for (int j = 0; j < iLength; j++)
            {
                pollFinish = false;
                iComparing = ( (i+1) * iLength -1) - j;
                iPolling = iComparing - 1;
                while (!pollFinish)
                {
                    if (iPolling < (i * iLength) )//若輪尋格子超出了目前的這條行線
                    {
                        pollFinish = true;
                    }
                    else if (iListCheckBoard[iPolling] != 0)//若輪尋格子有東西(不為0)
                    {
                        if (iListCheckBoard[iComparing] == 0)//但受比對的格子是空的(0)的話，那就直接把數字搬過來
                        {
                            iListCheckBoard[iComparing] = iListCheckBoard[iPolling];
                            iListCheckBoard[iPolling] = 0;
                        }
                        else //其餘狀況，也就是當受比對的格子有東西(不為0)
                        {
                            if (iListCheckBoard[iComparing] == iListCheckBoard[iPolling])//若比對者與輪尋者皆同，兩者合併
                            {
                                iListCheckBoard[iComparing] += iListCheckBoard[iPolling];
                                iListCheckBoard[iPolling] = 0;
                            }
                            else if (iListCheckBoard[iComparing - 1] == 0)//若比對者的隔壁沒東西(0)，那就把輪尋到的那個數字直接搬過來
                            {
                                iListCheckBoard[iComparing - 1] += iListCheckBoard[iPolling];
                                iListCheckBoard[iPolling] = 0;
                            }
                            pollFinish = true;
                        }
                    }
                    else//其餘狀況，也就是當輪尋格子沒東西(0)，且輪尋值也還未到底時
                    {
                        iPolling--;
                    }
                }
            }
        }
    }
}

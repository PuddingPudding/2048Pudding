using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewControll : MonoBehaviour
{
    public enum EGamingMode
    {
        Customize, //自訂
        Standard, //標準模式，4*4
        Small, //小格局，3*3
        Big, //大格局，5*5
        BigAndObstruct, //大而阻塞，5*5，並且放入4個障礙物，分別在6 8 16 18 的位子
        BigAndNone, //大而空洞，將上面那個模式的障礙物改為可越過的空洞
        Insane //瘋狂模式，5*5並且會隨機生成5個障礙物
    }

    [SerializeField] private Transform m_transformInitPosition; //第一個生成格子物件的位子
    [SerializeField] private GameObject m_gameObjBlockPrefab; //每個格子的遊戲物件原型
    [SerializeField] private Vector2 m_vec2BlockInterval = new Vector2(60, 60); //每個格子物件間的間隔
    [SerializeField] private Text m_textScore;//計分表
    [SerializeField] private int m_iLengthX = 4, m_iLengthY = 4; //棋盤邊長
    [SerializeField] private EGamingMode m_eGameMode = EGamingMode.Customize;
    private List<BlockView> m_listBlockView; //用來抓到每個棋盤內格子物件的程式碼
    private GamingLogic2048 m_game; //遊戲物件，負責真正的管理遊戲邏輯與資料
    private int[] m_iArrSpecialBlockPos = new int[] { 6, 8, 16, 18 };

    // Use this for initialization
    void Start()
    {
        m_listBlockView = new List<BlockView>();
        InitGame(m_eGameMode);
        UpdateGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.Keypad4))
        {
            if (m_game.GetPushAble(GamingLogic2048.EPushingDirection.Left))
            {
                m_game.Pushing(GamingLogic2048.EPushingDirection.Left);
                m_game.SpawnRandomBlock(1);
                UpdateGame();
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.Keypad6))
        {
            if (m_game.GetPushAble(GamingLogic2048.EPushingDirection.Right))
            {
                m_game.Pushing(GamingLogic2048.EPushingDirection.Right);
                m_game.SpawnRandomBlock(1);
                UpdateGame();
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Keypad8))
        {
            if (m_game.GetPushAble(GamingLogic2048.EPushingDirection.Up))
            {
                m_game.Pushing(GamingLogic2048.EPushingDirection.Up);
                m_game.SpawnRandomBlock(1);
                UpdateGame();
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            if (m_game.GetPushAble(GamingLogic2048.EPushingDirection.Down))
            {
                m_game.Pushing(GamingLogic2048.EPushingDirection.Down);
                m_game.SpawnRandomBlock(1);
                UpdateGame();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            if (m_game.GetPushAble(GamingLogic2048.EPushingDirection.TopLeft))
            {
                m_game.Pushing(GamingLogic2048.EPushingDirection.TopLeft);
                m_game.SpawnRandomBlock(1);
                UpdateGame();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            if (m_game.GetPushAble(GamingLogic2048.EPushingDirection.BottomLeft))
            {
                m_game.Pushing(GamingLogic2048.EPushingDirection.BottomLeft);
                m_game.SpawnRandomBlock(1);
                UpdateGame();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            if (m_game.GetPushAble(GamingLogic2048.EPushingDirection.TopRight))
            {
                m_game.Pushing(GamingLogic2048.EPushingDirection.TopRight);
                m_game.SpawnRandomBlock(1);
                UpdateGame();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            if (m_game.GetPushAble(GamingLogic2048.EPushingDirection.BottomRight))
            {
                m_game.Pushing(GamingLogic2048.EPushingDirection.BottomRight);
                m_game.SpawnRandomBlock(1);
                UpdateGame();
            }
        }
    }

    private void BuildBoard(int _iLengthX , int _iLengthY) //單純的把棋盤建出來，裡頭都先不要設定數字或障礙
    {
        m_game = new GamingLogic2048(_iLengthX, _iLengthY, 0);
        int iBoardSize = m_game.GetBoardSize();
        for (int i = 0; i < iBoardSize; i++)
        {
            Vector3 initPlace = Vector3.zero;
            initPlace.x += m_vec2BlockInterval.x * (i % _iLengthX);
            initPlace.y -= m_vec2BlockInterval.y * (i / _iLengthX);
            GameObject blockTemp = Instantiate(m_gameObjBlockPrefab, initPlace, m_gameObjBlockPrefab.transform.rotation);
            blockTemp.transform.SetParent(m_transformInitPosition.transform);
            blockTemp.transform.localPosition = initPlace;
            BlockView BVTemp = blockTemp.GetComponent<BlockView>();
            m_listBlockView.Add(BVTemp);
        }
    }
    private void InitGame(EGamingMode _eGameMode)
    {
        switch(_eGameMode)
        {
            case EGamingMode.Customize:
                BuildBoard(m_iLengthX, m_iLengthY);
                m_game.SpawnRandomBlock(2);
                break;
            case EGamingMode.Standard:
                BuildBoard(4, 4);
                m_game.SpawnRandomBlock(2);
                break;
            case EGamingMode.Small:
                BuildBoard(3, 3);
                m_game.SpawnRandomBlock(2);
                break;
            case EGamingMode.Big:
                BuildBoard(5 , 5);
                m_game.SpawnRandomBlock(2);
                break;
            case EGamingMode.BigAndObstruct:
                BuildBoard(5, 5);
                m_game.SpawnRandomBlock(2);
                for(int i = 0; i< m_iArrSpecialBlockPos.Length; i++)
                {
                    //Blo
                }
                break;
        }
    }
    public void UpdateGame()
    {
        int iBoardSize = m_game.GetBoardSize();
        List<BlockData> listBoardData = m_game.GetBoardData();
        for (int i = 0; i < iBoardSize; i++)
        {
            m_listBlockView[i].SetBlock(listBoardData[i]);
        }
        m_textScore.text = "score: " + m_game.GetScore();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewControll : MonoBehaviour
{
    [SerializeField] private Transform m_transformInitPosition; //第一個生成格子物件的位子
    [SerializeField] private GameObject m_gameObjBlockPrefab; //每個格子的遊戲物件原型
    [SerializeField] private Vector2 m_vec2BlockInterval = new Vector2(60, 60); //每個格子物件間的間隔
    [SerializeField] private Text m_textScore;//計分表
    [SerializeField] private int m_iLengthX = 4, m_iLengthY = 4; //棋盤邊長
    private List<BlockView> m_listBlockView; //用來抓到每個棋盤內格子物件的程式碼
    private GamingLogic2048 m_game; //遊戲物件，負責真正的管理遊戲邏輯與資料

    // Use this for initialization
    void Start()
    {
        m_game = new GamingLogic2048(m_iLengthX, m_iLengthY, 0);
        m_listBlockView = new List<BlockView>();
        InitGame();
        UpdateGame();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void InitGame()
    {
        int iBoardSize = m_game.GetBoardSize();
        for (int i = 0; i < iBoardSize; i++)
        {
            Vector3 initPlace = Vector3.zero;
            initPlace.x += m_vec2BlockInterval.x * (i % m_iLengthX);
            initPlace.y -= m_vec2BlockInterval.y * (i / m_iLengthX);
            GameObject blockTemp = Instantiate(m_gameObjBlockPrefab, initPlace, m_gameObjBlockPrefab.transform.rotation);
            blockTemp.transform.SetParent(m_transformInitPosition.transform);
            blockTemp.transform.localPosition = initPlace;
            BlockView BVTemp = blockTemp.GetComponent<BlockView>();
            m_listBlockView.Add(BVTemp);
        }
    }
    public void UpdateGame()
    {
        int iBoardSize = m_game.GetBoardSize();
        List<BlockData> listBoardData = m_game.GetBoardData();
        for (int i = 0; i < iBoardSize; i++)
        {
            m_listBlockView[i].SetText(listBoardData[i]);
        }
        m_textScore.text = "score: " + m_game.GetScore();
    }

}

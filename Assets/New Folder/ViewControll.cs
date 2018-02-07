using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewControll : MonoBehaviour
{
    [SerializeField] private Transform m_transformInitPosition; //第一個生成格子物件的位子
    [SerializeField] private GameObject m_gameObjBlockPrefab; //每個格子的遊戲物件原型
    [SerializeField] private Vector2 m_vec2BlockInterval = new Vector2(60, 60); //每個格子物件間的間隔
    [SerializeField] private Text m_textScore;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

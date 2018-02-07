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
}

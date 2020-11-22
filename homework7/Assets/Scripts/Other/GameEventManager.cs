using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventManager : MonoBehaviour{
    public delegate void ScoreEvent();
    public static event ScoreEvent ScoreChange;

    public delegate void GameoverEvent();
    public static event GameoverEvent GameoverChange;

    public void PlayerEscape(){
        //逃脱之后分数增加
        if (ScoreChange != null){
            ScoreChange();
        }
    }

    public void PlayerGameover(){
        //碰撞之后游戏结束
        if (GameoverChange != null){
            GameoverChange();
        }
    }
}

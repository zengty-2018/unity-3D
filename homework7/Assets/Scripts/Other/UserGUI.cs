using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour {

    private IUserAction action;
    private GUIStyle score_style = new GUIStyle();
    private GUIStyle style = new GUIStyle();
    private GUIStyle over_style = new GUIStyle();

    void Start ()
    {
        action = SSDirector.GetInstance().CurrentScenceController as IUserAction;
        style.normal.textColor = Color.black;
        style.fontSize = 20;
        score_style.fontSize = 20;
        over_style.fontSize = 25;
    }

    void Update(){
        //读取键盘输入
        float translationX = Input.GetAxis("Horizontal");
        float translationZ = Input.GetAxis("Vertical");
        action.MovePlayer(translationX, translationZ);
    }

    private void OnGUI(){
        GUI.Label(new Rect(10, 5, 200, 50), "Score:", style);
        GUI.Label(new Rect(70, 5, 200, 50), action.GetScore().ToString(), style);
        if(action.GetScore() == 10){
            GUI.Label(new Rect(Screen.width / 2 - 30, Screen.width / 2 - 220, 100, 100), "Pass!", over_style);
            if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.width / 2 - 150, 100, 50), "Restart")){
                action.Restart();
                return;
            }
        }   
        else if(action.GetGameover() && action.GetScore() != 20){
            GUI.Label(new Rect(Screen.width / 2 - 70, Screen.width / 2 - 220, 100, 100), "Game Over!", over_style);
            if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.width / 2 - 150, 100, 50), "Restart")){
                action.Restart();
                return;
            }
        }
    }
}

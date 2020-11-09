using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI  : MonoBehaviour {
    private IUserAction action;
    private bool started = false;
    GUIStyle text_style;
    GUIStyle header_style;

    void Start () {
        action = SSDirector.GetInstance().CurrentScenceController as IUserAction;

        header_style = new GUIStyle();
        header_style.fontSize = 40;
        text_style = new GUIStyle();
        text_style.fontSize = 20;
    }

	void OnGUI () {
        if (action.isEnd()) {
            string mes;
            if(action.GetHP() <= 0){
                mes = "You lost!";
            }
            else{
                mes = "You win!";
            }
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 100, 100, 100), mes, header_style);
            GUI.Label(new Rect(Screen.width / 2 - 30, Screen.height / 2 - 50, 50, 50), "Your score:" + action.GetScore().ToString(), text_style);
            if (GUI.Button(new Rect(Screen.width / 2 - 20, Screen.height / 2, 100, 50), "Restart")) {
                action.Restart();
                return;
            }
            action.GameOver();
        }
        else if (started) {
            GUI.Label(new Rect(10, 5, 50, 50), "Round:" + action.GetRound().ToString(), header_style);
            GUI.Label(new Rect(10, 55, 50, 50), "Trial:" + action.GetTrial().ToString(), header_style);
            GUI.Label(new Rect(10, 105, 50, 50), "Score:"+ action.GetScore().ToString(), header_style);
            GUI.Label(new Rect(10, 155, 50, 50), "HP   :"+ action.GetHP().ToString(), header_style);
        }
        else {
            GUI.Label(new Rect(Screen.width / 2 - 70, 100, 100, 100), "Hit UFO", header_style);
            GUI.Label(new Rect(Screen.width /2 - 150, 143, 100, 100), "Rule: use the mouse to hit UFO.", text_style);
            GUI.Label(new Rect(Screen.width /2 - 100, 166, 100, 100), "if you miss 5 UFO, you will lost.", text_style);
            if (GUI.Button(new Rect(Screen.width / 2 - 50, 200, 100, 50), "Start")) {
                started = true;
                action.Restart();
            }
        }
    }
   
}

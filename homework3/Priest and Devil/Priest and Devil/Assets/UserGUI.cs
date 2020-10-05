using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interface;

public class UserGUI : MonoBehaviour {

	private IUserAction action;
	public int sign = 0;

	void Start()
	{
		action = SSDirector.GetInstance().CurrentScenceController as IUserAction;
	}
	void OnGUI(){
		if (sign == 1){
			GUI.Label(new Rect(Screen.width / 2-20, Screen.height/2-60, 100, 50), "Lost!");
			if (GUI.Button(new Rect(Screen.width/2-50, Screen.height/2+30, 100, 50), "Restart")){
				action.Restart();
				sign = 0;
			}
		}
		else if (sign == 2){
			GUI.Label(new Rect(Screen.width/2-20, Screen.height/2-60, 100, 50), "Win!");
			if (GUI.Button(new Rect(Screen.width/2-50, Screen.height/2+30, 100, 50), "Restart")){
				action.Restart();
				sign = 0;
			}
		}
	}	
}

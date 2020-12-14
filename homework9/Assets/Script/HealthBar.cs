using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour{
    public float health = 0.5f;

    private float resulthealth;
    private Transform father;

    private Rect healthbar;
    private Rect Add;
    private Rect Minus;

    void Start(){
        resulthealth = health;
        father = this.transform.parent.transform;
    }

    void OnGUI(){
        healthbar = new Rect(Screen.width / 2 + father.position.x * father.localScale.x * 30 - father.localScale.x * 50, 
        Screen.height / 2 + (father.position.z - father.localScale.y * 7) * father.localScale.z * 10, 
        father.localScale.x * 100, father.localScale.z * 10);
         
        Add = new Rect(Screen.width / 2 + father.position.x * father.localScale.x * 30 + father.localScale.x * 55, 
        Screen.height / 2 + (father.position.z - father.localScale.y * 7) * father.localScale.z * 9.5f, 
        father.localScale.x * 20, father.localScale.z * 10);
        
        Minus = new Rect(Screen.width / 2 + father.position.x * father.localScale.x * 30 - father.localScale.x * 75, 
        Screen.height / 2 + (father.position.z - father.localScale.y * 7) * father.localScale.z * 9.5f, 
        father.localScale.x * 20, father.localScale.z * 10);

        if (GUI.Button(Add, "+")){
            resulthealth = resulthealth + 0.1f > 1.0f ? 1.0f : resulthealth + 0.1f;
        }
        if (GUI.Button(Minus, "-")){
            resulthealth = resulthealth - 0.1f < 0.0f ? 0.0f : resulthealth - 0.1f;
        }

        health = Mathf.Lerp(health, resulthealth, 0.05f);
        GUI.HorizontalScrollbar(healthbar, 0, health, 0.0f, 1.0f);
    }
}

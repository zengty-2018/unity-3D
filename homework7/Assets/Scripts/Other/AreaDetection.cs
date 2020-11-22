using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDetection : MonoBehaviour{
    FirstSceneController sceneController;
    public int sign = 0;

    private void Start(){
        sceneController = SSDirector.GetInstance().CurrentScenceController as FirstSceneController;
    }

    void OnTriggerEnter(Collider collider){
        if (collider.gameObject.tag == "Player"){
            sceneController.wall_sign = sign;
        }
    }
}

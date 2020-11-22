using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInDetection : MonoBehaviour{
    void OnTriggerEnter(Collider collider){
        //玩家进入巡逻兵范围
        if (collider.gameObject.tag == "Player"){
            this.gameObject.transform.parent.GetComponent<PatrolData>().follow_player = true;
            this.gameObject.transform.parent.GetComponent<PatrolData>().player = collider.gameObject;
            
            //巡逻兵追踪玩家
            this.gameObject.transform.parent.GetComponent<Animator>().SetTrigger("shock");
        }
    }
    void OnTriggerExit(Collider collider){
        //玩家离开巡逻兵范围，停止追踪
        if (collider.gameObject.tag == "Player"){
            this.gameObject.transform.parent.GetComponent<PatrolData>().follow_player = false;
            this.gameObject.transform.parent.GetComponent<PatrolData>().player = null;
        }
    }
}

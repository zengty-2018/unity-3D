using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollideDetection : MonoBehaviour {
    void OnCollisionEnter(Collision other){
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Animator>().SetBool("death",true);
            this.GetComponent<Animator>().SetTrigger("shoot");
            Singleton<GameEventManager>.Instance.PlayerGameover();
        }
    }
}

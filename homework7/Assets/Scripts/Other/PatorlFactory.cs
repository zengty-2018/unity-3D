using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatorlFactory : MonoBehaviour {
    private GameObject player = null;
    private GameObject patrol = null;
    private List<GameObject> patrolList = new List<GameObject>();
    private Vector3[] vec = new Vector3[9];

    public GameObject LoadPlayer(){
        player = Instantiate(Resources.Load("Prefabs/Player"), new Vector3(0, 9, 0), Quaternion.identity) as GameObject;
        return player;
    }

    public List<GameObject> LoadPatrol(){
        int[] pos_x = { -6, 4, 13 };
        int[] pos_z = { -4, 6, -13 };
        int index = 0;
        for(int i=0;i < 3;i++){
            for(int j=0;j < 3;j++){
                vec[index] = new Vector3(pos_x[i], 0, pos_z[j]);
                index++;
            }
        }
        for(int i=0; i < 9; i++){
            patrol = Instantiate(Resources.Load<GameObject>("Prefabs/Patrol1"));
            patrol.transform.position = vec[i];
            patrol.GetComponent<PatrolData>().sign = i + 1;
            patrol.GetComponent<PatrolData>().start_position = vec[i];
            patrolList.Add(patrol);
        }   
        return patrolList;
    }

    public void StopPatrol(){
        //在游戏结束后调用，停止巡逻。
        for (int i = 0; i < patrolList.Count; i++){
            patrolList[i].gameObject.GetComponent<Animator>().SetBool("run", false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskFactory : MonoBehaviour
{
    public GameObject diskPrefab = null;
    private List<DiskProperty> used = new List<DiskProperty>();
    private List<DiskProperty> free = new List<DiskProperty>();

    public GameObject GetDisk(int round){
        float startY = -10f;
        string tag;
        diskPrefab = null;

        if(round == 1){
            tag = "easy";
        }
        else if(round == 2){
            tag = "middle";
        }
        else if(round == 3){
            tag = "hard";
        }

        for(int i= 0; i < free.Count; i++){
            if(free[i].tag == tag){
                diskPrefab = free[i].gameObject;
                free.Remove(free[i]);
                break;
            }
        }

        if(diskPrefab == null){
            if(tag == "easy"){
                diskPrefab = Instantiate(Resources.Load<GameObject>("Prefabs/easy"), new Vector3(0, start_y, 0), Quaternion.identity);
            }
            else if(tag == "middle"){
                diskPrefab = Instantiate(Resources.Load<GameObject>("Prefabs/middle"), new Vector3(0, start_y, 0), Quaternion.identity);
            }
            else if(tag == "hard"){
                diskPrefab = Instantiate(Resources.Load<GameObject>("Prefabs/hard"), new Vector3(0, start_y, 0), Quaternion.identity);
            }
        }

        used.Add(diskPrefab.GetComponent<DiskProperty>());
        return diskPrefab;
    }

    public void FreeDisk(GameObject disk){
        for(int i = 0; i < used.Count; i++){
            if(disk.GetInstanceID() == used[i].gameObject.GetInstanceID()){
                used[i].gameObject.SetActive(false);
                free.Add(used[i]);
                used.Remove(used[i]);
                break;
            }
        }
    }
}

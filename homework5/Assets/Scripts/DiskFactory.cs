using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskFactory : MonoBehaviour{
    public static DiskFactory DiskFact;
    private List<Disk> BusyDisks = new List<Disk>();
    private List<Disk> FreeDisks = new List<Disk>();

    private DiskFactory(){}

    public GameObject GetDisk(int type) {
        GameObject disk_prefab = null;
        
        if (FreeDisks.Count > 0) {
            for(int i = 0; i < FreeDisks.Count; i++) {
                if (FreeDisks[i].type == type) {
                    disk_prefab = FreeDisks[i].gameObject;
                    FreeDisks.Remove(FreeDisks[i]);
                    break;
                }
            }     
        }
        
        if(disk_prefab == null) {
            if(type == 1) {
                disk_prefab = GameObject.Instantiate(
                Resources.Load<GameObject>("Prefabs/disk1"),
                new Vector3(0, -10f, 0), Quaternion.identity);
            }
            else if (type == 2) {
                disk_prefab = GameObject.Instantiate(
                Resources.Load<GameObject>("Prefabs/disk2"),
                new Vector3(0, -10f, 0), Quaternion.identity);
            }
            else {
                disk_prefab = GameObject.Instantiate(
                Resources.Load<GameObject>("Prefabs/disk3"),
                new Vector3(0, -10f, 0), Quaternion.identity);
            }
            disk_prefab.GetComponent<Renderer>().material.color = disk_prefab.GetComponent<Disk>().color;
        }

        BusyDisks.Add(disk_prefab.GetComponent<Disk>());
        disk_prefab.SetActive(true);
        return disk_prefab;
    }

    public bool FreeUsedDisks() {
        bool ret = true;
        for(int i = 0; i < BusyDisks.Count; i++) {
            if (BusyDisks[i].gameObject.transform.position.y + 10f > -0.2f && BusyDisks[i].gameObject.transform.position.y + 10f < 0.2f){
                Debug.Log("1");
                BusyDisks[i].gameObject.transform.position = new Vector3(0, -20f, 0);
                ret =false;
            }
            else if (BusyDisks[i].gameObject.transform.position.y <= -15f) {
                FreeDisks.Add(BusyDisks[i]);
                BusyDisks.Remove(BusyDisks[i]);
            }
        }
        return ret;
    }

    public void Reset() {
        FreeUsedDisks();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adapter : SSActionManager {
    public DiskFlyAction fly;
    public PhysisFlyAction fly_;

    public void DiskFly(GameObject disk, int mode, mes information) {
        int leftOrRight = 1;//from left is 1, from right is -1
        if (disk.transform.position.x > 0){
            leftOrRight = -1;
        }

        if(mode == 2){
            fly = DiskFlyAction.GetSSAction(leftOrRight, information.angle, information.speed);
            this.StartAction(disk, fly);
        }
        else{
            fly_ = PhysisFlyAction.GetSSAction(leftOrRight, information.speed);
            this.StartAction(disk, fly_);
        }
    }
}

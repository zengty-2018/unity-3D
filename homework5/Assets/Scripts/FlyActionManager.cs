using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyActionManager : SSActionManager {
    public DiskFlyAction fly;  
    public void DiskFly(GameObject disk, float angle, float speed) {
        int leftOrRight = 1;//from left is 1, from right is -1
        if (disk.transform.position.x > 0) leftOrRight = -1;
        fly = DiskFlyAction.GetSSAction(leftOrRight, angle, speed);
        this.StartAction(disk, fly);
    }
}

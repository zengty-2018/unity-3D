using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskFlyAction : SSAction {
    public float gravity = -0.2f;
    private Vector3 start_velocity;
    private Vector3 delta_velocity = Vector3.zero;
    private Vector3 current_angle = Vector3.zero;
    private float time;

    private DiskFlyAction() { }
    public static DiskFlyAction GetSSAction(int lor, float angle, float speed) {
        DiskFlyAction action = CreateInstance<DiskFlyAction>();
        if (lor == -1) {
            action.start_velocity = Quaternion.Euler(new Vector3(0, 0, -angle)) * Vector3.left * speed;
        }
        else {
            action.start_velocity = Quaternion.Euler(new Vector3(0, 0, angle)) * Vector3.right * speed;
        }
        return action;
    }

    public override void Update() {
        time += Time.fixedDeltaTime;
        delta_velocity.y = gravity * time;

        gameobject.GetComponent<Rigidbody>().useGravity = false;

        transform.position += (start_velocity + delta_velocity) * Time.fixedDeltaTime;
        current_angle.z = Mathf.Atan((start_velocity.y + delta_velocity.y) / start_velocity.x) * Mathf.Rad2Deg;
        transform.eulerAngles = current_angle;

        if (this.transform.position.y < -10) {
            this.enable = false;  
        }
    }

    public override void FixedUpdate() {
        
    }

    public override void Start() { 
        
    }
}

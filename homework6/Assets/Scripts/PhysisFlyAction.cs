using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysisFlyAction : SSAction
{
    private bool start_position;
    public float force;

    public static PhysisFlyAction GetSSAction(int pos, float force_){
        PhysisFlyAction action = CreateInstance<PhysisFlyAction>();
        if(pos == 1){
            action.start_position = true;
        }
        else{
            action.start_position = false;
        }
        action.force = force_;
        
        return action;
    }
    // Start is called before the first frame update
    public override void Start()
    {
        Rigidbody disk = gameobject.GetComponent<Rigidbody>();
        
        gameobject.GetComponent<Rigidbody>().useGravity = true;
        if(gameobject.GetComponent<Rigidbody>().position.y <= 3){
            if(start_position){
                gameobject.GetComponent<Rigidbody>().AddForce(new Vector3(0.4f,0.2f,0)*force*15f, ForceMode.Impulse);
            }
            else{
                gameobject.GetComponent<Rigidbody>().AddForce(new Vector3(-0.4f,0.2f,0)*force*15f, ForceMode.Impulse);
            }
        }
    }

    // Update is called once per frame
    public override void Update()
    {
        
    }

    public override void FixedUpdate() {
        if (transform.position.y <= -10f) {
            Debug.Log(transform.position.y);
            gameobject.GetComponent<Rigidbody>().useGravity = false;
            gameobject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            this.enable = false;
        }
    }
}

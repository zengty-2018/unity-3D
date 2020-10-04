using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move2 : MonoBehaviour
{
    int speedx;
    int speedy;
    // Start is called before the first frame update
    void Start()
    {
        speedx = 1;
        speedy = -2;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += Vector3.right*speedx*Time.deltaTime;
        this.transform.position += Vector3.up*speedy*Time.deltaTime;
        speedy += 1;
    }
}

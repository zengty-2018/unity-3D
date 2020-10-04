using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move1 : MonoBehaviour
{
    int speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = 1;
    }

    // Update is called once per frame
    void Update()
    {
        float x = this.transform.position.x;
        float newX = x + speed*Time.deltaTime;

        this.transform.position = new Vector3(newX, newX*(newX-4));
    }
}

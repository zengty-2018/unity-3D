using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move3 : MonoBehaviour
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
        Vector3 move = new Vector3(Time.deltaTime*speedx, Time.deltaTime*speedy, 0);
        speedy++;
        transform.Translate(move);
    }
}

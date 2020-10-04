using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class revolution : MonoBehaviour
{
    public Transform center;
    public float speed;
    public float x, y, z;
    // Start is called before the first frame update
    void Start()
    {
        x = Random.Range(10, 60);
        y = Random.Range(60, 100);
        z = Random.Range(20, 100);
        speed = Random.Range(8, 25);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.RotateAround(center.position, new Vector3(x,y,0), speed*Time.deltaTime);
    }
}

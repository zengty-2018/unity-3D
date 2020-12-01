
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class ParticleRing : MonoBehaviour {
    public ParticleSystem particleSystem;

    private int particleNum = 1500;

    private float minRadius = 7f;
    private float maxRadius = 10.0f;

    private ParticleSystem.Particle[] particleRing;
    private float[] particleAngle;
    private float[] particleR;

    private int level = 5;
    private float speed = 0.02f;

    public Gradient Color;

    void Start () {
        particleAngle= new float[particleNum];
 
        particleR = new float[particleNum];     
        particleRing = new ParticleSystem.Particle[particleNum];
        particleSystem.maxParticles = particleNum;
        particleSystem.Emit(particleNum);
        particleSystem.GetParticles(particleRing);

        for (int i = 0; i < particleNum; i++){
            particleRing[i].color = Color.Evaluate(Mathf.Abs(Mathf.Cos(Time.time)));

            float midR = (maxRadius + minRadius) / 2;
            float temp1 = Random.Range(minRadius, midR);
            float temp2 = Random.Range(midR, maxRadius);
            float r = Random.Range(temp1, temp2);
            
            //float r = Random.Range(minRadius, maxRadius);
            float angle = Random.Range(0.0f, 360.0f);
 
            particleAngle[i] = angle;
            particleR[i] = r;
        }
    }
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < particleNum; i++){
            particleRing[i].color = Color.Evaluate(Mathf.Abs(Mathf.Cos(Time.time)));
            
            if (i % 2 == 0){
                particleAngle[i] += (i % level + 1) * speed;
            }
            else{
                particleAngle[i] -= (i % level + 1) * speed;
            }
            particleAngle[i] = particleAngle[i] % 360;
            float rad = particleAngle[i] / 180 * Mathf.PI;
            particleRing[i].position = new Vector3(particleR[i] * Mathf.Cos(rad), particleR[i] * Mathf.Sin(rad), 0f);
        }
        particleSystem.SetParticles(particleRing, particleNum);
    }
}
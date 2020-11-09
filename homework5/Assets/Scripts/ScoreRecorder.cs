using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreRecorder : MonoBehaviour {
    public static ScoreRecorder scoreR;
    private float score;

    private ScoreRecorder(){}
    
    void Start () {
        score = 0;
    }

    public void RecordScore(GameObject disk) {
        score += disk.GetComponent<Disk>().score;
    }

    public float GetScore() {
        return score;
    }
    
    public void Reset() {
        score = 0;
    }
}

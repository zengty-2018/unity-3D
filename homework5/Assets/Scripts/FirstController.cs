using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstController : MonoBehaviour, ISceneController, IUserAction {
    public DiskFactory disk_factory;
    public FlyActionManager action_manager;
    public ScoreRecorder score_recorder;
    public UserGUI user_gui;
    private int round = 1;                                                  
    private int trial = 0;
    int count = 0;                                         
    private bool running = false;
    private int HP=5;

    void Start () {  
        SSDirector director = SSDirector.GetInstance();     
        director.CurrentScenceController = this;
        disk_factory = Singleton<DiskFactory>.Instance;
        score_recorder = Singleton<ScoreRecorder>.Instance;
        action_manager = gameObject.AddComponent<FlyActionManager>() as FlyActionManager;
        user_gui = gameObject.AddComponent<UserGUI>() as UserGUI;
    }

	void Update () {
        if(running) {
            count++;

            if (Input.GetButtonDown("Fire1")) {
                Vector3 pos = Input.mousePosition;
                Hit(pos);
            }
            switch (round) {
                case 1: {
                    if (count >= 600) {
                        count = 0;
                        SendDisk(trial % 2 + 1);
                        if(Random.Range(1,3) == 1)
                            SendDisk(1);
                        
                        if (trial >= 10) {
                            round += 1;
                            trial = 0;
                        }
                        trial += 1;
                    }
                    break;
                }
                case 2: {
                    if (count >= 600) {
                        if (trial == 11) {
                            running = false;
                        }
                        count = 0;
                        if(trial <= 9){
                            SendDisk(Random.Range(1,3));
                        }
                        if(Random.Range(1,2) == 1){
                            SendDisk(Random.Range(1,3));
                        }
                        if(Random.Range(1,3) == 1){
                            SendDisk(Random.Range(1,3));
                        }
                        trial += 1;
                    }
                    break;
                }
                default:
                    break;
            } 

            bool ret = disk_factory.FreeUsedDisks();
            if(!ret){
                HP--;
                if(HP == 0){
                    GameOver();
                }
            }
        }
    }

    public void LoadResources() {}

    private void SendDisk(int type) {
        GameObject disk = disk_factory.GetDisk(type);

        float disk_y = Random.Range(0f, 3f);
        float disk_x = Random.Range(-1f, 1f) < 0 ? -1 : 1;
 
        float speed = 0;
        float angle = Random.Range(15f, 25f);
        if (type == 1) {
            speed = Random.Range(1f, 1.5f);
        }
        else if (type == 2) {
            speed = Random.Range(1.5f, 2f);
        }
        
        disk.transform.position = new Vector3(disk_x * 14f, disk_y, 0);
        action_manager.DiskFly(disk, angle, speed);
    }

    public void Hit(Vector3 pos) {
       // Debug.Log("2");
        Ray ray = Camera.main.ScreenPointToRay(pos);
        RaycastHit[] hits;
        hits = Physics.RaycastAll(ray);
        for (int i = 0; i < hits.Length; i++) {
            RaycastHit hit = hits[i];
            if (hit.collider.gameObject.GetComponent<Disk>() != null) {
                score_recorder.RecordScore(hit.collider.gameObject);
                hit.collider.gameObject.transform.position = new Vector3(0, -15, 0);
            }
        }
    }

    public void Restart() {
        running = true;
        score_recorder.Reset();
        disk_factory.Reset();
        round = 1;
        trial = 0;
        HP = 5;
    }

    public void GameOver() {
        running = false;
    }

    public int GetHP(){
        return HP;
    }
    public float GetScore() {
        return score_recorder.GetScore();
    }
    public int GetRound() {
        return round;
    }
    public int GetTrial() {
        return trial;
    }
    public bool isEnd(){
        return ((round == 2 && trial == 11) || HP <= 0);
    }
}

    

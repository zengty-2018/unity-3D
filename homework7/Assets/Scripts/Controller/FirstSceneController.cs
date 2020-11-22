using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FirstSceneController : MonoBehaviour, IUserAction, ISceneController
{
    public PatorlFactory _PatorlFactory; 
    public ScoreRecorder _ScoreRecorder;
    public PatrolActionManager _PatrolActionManager;
 
    public GameObject player;
    private List<GameObject> patrols;

    public int wall_sign = -1;
    private float player_speed = 5;
    private bool game_over = false;

    void Update(){
        for (int i = 0; i < patrols.Count; i++){
            patrols[i].gameObject.GetComponent<PatrolData>().wall_sign = wall_sign;
        }
        
        if(_ScoreRecorder.score == 10){
            Gameover();
        }
    }

    void Start(){
        SSDirector director = SSDirector.GetInstance();
        director.CurrentScenceController = this;
        _PatorlFactory = Singleton<PatorlFactory>.Instance;
        _PatrolActionManager = Singleton<PatrolActionManager>.Instance;

        LoadResources();
        _ScoreRecorder = Singleton<ScoreRecorder>.Instance;
    }

    public void LoadResources(){
        player = _PatorlFactory.LoadPlayer();

        patrols = _PatorlFactory.LoadPatrol();
        for (int i = 0; i < patrols.Count; i++){
            _PatrolActionManager.GoPatrol(patrols[i]);
        }

        GameObject.Instantiate(Resources.Load("Prefabs/Plane"), new Vector3(0, 0, 0), Quaternion.identity);
    }

    public void MovePlayer(float translationX, float translationZ){
        if(!game_over){
            if (translationX != 0 || translationZ != 0){
                //run动画
                player.GetComponent<Animator>().SetBool("run", true);
            }
            else
            {
                player.GetComponent<Animator>().SetBool("run", false);
            }
            //通过键盘输入移动玩家。
            player.transform.Translate(translationX * player_speed * Time.deltaTime, 0, translationZ * player_speed * Time.deltaTime);

            if (player.transform.position.y != 0){
                player.transform.position = new Vector3(player.transform.position.x, 0, player.transform.position.z);
            }     
        }
    }

    public int GetScore()
    {
        return _ScoreRecorder.score;
    }
    public bool GetGameover()
    {
        return game_over;
    }
    public void Restart()
    {
        SceneManager.LoadScene("Scenes/mySence");
    }

    //发布与订阅模式
    void OnEnable()
    {
        GameEventManager.ScoreChange += AddScore;
        GameEventManager.GameoverChange += Gameover;
    }
    void OnDisable()
    {
        GameEventManager.ScoreChange -= AddScore;
        GameEventManager.GameoverChange -= Gameover;
    }
    
    void AddScore()
    {
        _ScoreRecorder.AddScore();
    }
    void Gameover()
    {
        game_over = true;
        _PatorlFactory.StopPatrol();
        _PatrolActionManager.DestroyAllAction();
    }
}

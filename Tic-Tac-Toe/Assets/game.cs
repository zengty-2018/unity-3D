using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class game : MonoBehaviour
{
    int empty = 9;    //表示空余的位置
    int turn = 1;   //O先手
    int [,]chess = new int [3,3];   //表示棋盘
    // Start is called before the first frame update
    void Start(){
        reset();    //重置整个棋盘
    }

    void reset(){   //重置游戏
        empty = 9;
        turn = 1;
        for(int i = 0; i < 3; i++){
            for(int j = 0; j < 3; j++){
                chess[i,j] = 0;
            }
        }
    }

    void OnGUI(){
        GUI.skin.label.fontSize = 30;

		if(GUI.Button(new Rect(290,302,95,50), "Reset")){//重新开始游戏
			reset();
		}

		int result = check();   //获得当前结果

        //生成界面
        GUI.Label(new Rect(90, 10, 50, 50), "o");
        GUI.Label(new Rect(560, 10, 50, 50), "x");

        //判断输、赢、平局
		if(result == 1){
			GUI.Label (new Rect(70, 100, 200, 50), "win");

            GUI.Label (new Rect(545, 100, 200, 50), "lost");
		} 
		else if(result == 2){
			GUI.Label (new Rect(70, 100, 200, 50), "lost");
            GUI.Label (new Rect(545, 100, 200, 50), "win");
		} 
		else if(result == -1){
            GUI.Label (new Rect(55, 100, 200, 50), "draw");
            GUI.Label (new Rect(535, 100, 200, 50), "draw");   
		} 
		else{  //判断是轮到谁走
			if(turn == 1){
				GUI.Label (new Rect (15, 100, 400, 50), "It's your turn");
			}
			else if(turn == 2){
				GUI.Label (new Rect (495, 100, 400, 50), "It's your turn");
			}
		}

        for(int i = 0; i < 3; i++){
            for(int j = 0; j < 3; j++){//生成棋盘
                if(chess[i,j] == 1){
                    GUI.Button(new Rect(i*100+190, j*100, 95, 95), "O");
                }
                else if(chess[i,j] == 2){
                    GUI.Button(new Rect(i*100+190, j*100, 95, 95), "X");
                }
                else{
                    if(GUI.Button(new Rect(i*100+190, j*100, 95, 95), "")){
                        if(result == 0){//点了按钮之后的事件
                            --empty;
                            if(turn == 1){
                                chess[i,j] = 1;
                                turn = 2;
                            }
                            else{
                                chess[i,j] = 2;
                                turn = 1;
                            }

                        }
                    }
                }
            }
        }
    }

    int check(){//判断输赢
        if(empty == 0){ //没有空位，平局
            return -1;
        }

        int temp;
        for(int i = 0; i < 3; i++){//判断横向
            temp = chess[i,0];
            for(int j = 0; j < 3; j++){
                if(temp != chess[i,j]){
                    break;
                }
                else if(j == 2){
                    return temp;
                }
            }
        }

        for(int i = 0; i < 3; i++){//判断纵向
            temp = chess[0,i];
            for(int j = 0; j < 3; j++){
                if(temp != chess[j,i]){
                    break;
                }
                else if(j == 2){
                    return temp;
                }
            }
        }

        temp = chess[1,1];
        //判断对角
        if(temp == chess[0,0] && temp == chess[2,2]){
            return temp;
        }

        if(temp == chess[2,0] && temp == chess[0,2]){
            return temp;
        }

        return 0;//如果都没有，则游戏继续
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

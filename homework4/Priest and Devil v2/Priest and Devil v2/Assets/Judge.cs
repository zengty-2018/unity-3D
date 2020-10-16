using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ModelController;

public class Judge
{
    LandModel start;
    LandModel end;
    BoatModel boat;

    public Judge(LandModel start_, LandModel end_, BoatModel boat_){
        start = start_;
        end = end_;
        boat = boat_;
    }

    public int Check(){
		int[] priest_num = new int[2]{(start.GetRoleNum())[0], (end.GetRoleNum())[0]};
		int[] devil_num = new int[2]{(start.GetRoleNum())[1], (end.GetRoleNum())[1]};

		if (priest_num[1] + devil_num[1] == 6)
			return 2;

		int[] boat_role_num = boat.GetRoleNumber();
		if (boat.GetBoatSign() == 1){
			priest_num[0] += boat_role_num[0];
			devil_num[0] += boat_role_num[1];
		}
		else{
			priest_num[1] += boat_role_num[0];
			devil_num[1] += boat_role_num[1];
		}
		if (priest_num[0] > 0 && priest_num[0] < devil_num[0]) {      
			return 1;
		}
		if (priest_num[1] > 0 && priest_num[1] < devil_num[1]){
			return 1;
		}
		return 0;
	}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

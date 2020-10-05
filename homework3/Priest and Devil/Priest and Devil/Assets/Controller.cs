using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ModelController;
using Interface;

public class Controller : MonoBehaviour, ISceneController, IUserAction
{
	public LandModel start_land;
	public LandModel end_land;
	public BoatModel boat;
	private RoleModel[] role;
	UserGUI gui;
	
	void Start (){
		SSDirector director = SSDirector.GetInstance();
		director.CurrentScenceController = this;
		gui = gameObject.AddComponent<UserGUI>() as UserGUI;
		LoadResources();
	}

	public void LoadResources(){
		Debug.Log("DD");
		GameObject water = Instantiate(Resources.Load("Prefabs/Water", typeof(GameObject)), new Vector3(0,-1,0), Quaternion.identity) as GameObject;
		water.name = "water";
		start_land = new LandModel("start");
		end_land = new LandModel("end");
		boat = new BoatModel();
		role = new RoleModel[6];

		for (int i = 0; i < 3; i++){
			RoleModel r = new RoleModel("priest");
			r.SetName("priest" + i);
			r.SetPosition(start_land.GetEmptyPosition());
			r.GoLand(start_land);
			start_land.AddRole(r);
			role[i] = r;
		}

		for (int i = 3; i < 6; i++){
			RoleModel r = new RoleModel("devil");
			r.SetName("devil" + i);
			r.SetPosition(start_land.GetEmptyPosition());
			r.GoLand(start_land);
			start_land.AddRole(r);
			role[i] = r;
		}
	}

	public void MoveBoat(){
		if (boat.IsEmpty() || gui.sign != 0){
			return;
		} 
		boat.BoatMove();
		gui.sign = Check();
	}

	public void MoveRole(RoleModel r){
		if (gui.sign != 0) {
			return;
		}
		if (r.IsOnBoat()){
			LandModel land;
			if (boat.GetBoatSign() == -1){
				land = end_land;
			}
			else{
				land = start_land;
			}
			boat.DeleteRoleByName(r.GetName());
			r.Move(land.GetEmptyPosition());
			r.GoLand(land);
			land.AddRole(r);
		}
		else{
			LandModel land = r.GetLandModel();
			if (boat.GetEmptyNumber() == -1 || land.GetLandSign() != boat.GetBoatSign()) {
				return;
			}   
			land.DeleteRoleByName(r.GetName());
			r.Move(boat.GetEmptyPosition());
			r.GoBoat(boat);
			boat.AddRole(r);
		}
		gui.sign = Check();
	}

	public void Restart(){
		SceneManager.LoadScene(0);
	}

	public int Check(){
		int[] priest_num = new int[2]{(start_land.GetRoleNum())[0], (end_land.GetRoleNum())[0]};
		int[] devil_num = new int[2]{(start_land.GetRoleNum())[1], (end_land.GetRoleNum())[1]};

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
}
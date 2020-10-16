using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ModelController;
using Interface;
using action;

namespace  controller{
	public class Controller : MonoBehaviour, ISceneController, IUserAction{
		public LandModel start_land;
		public LandModel end_land;
		public BoatModel boat;
		private RoleModel[] role;
		UserGUI gui;
		public MyActionManager manager;
		Judge judge;
		
		void Start (){
			SSDirector director = SSDirector.GetInstance();
			director.CurrentScenceController = this;
			gui = gameObject.AddComponent<UserGUI>() as UserGUI;
			LoadResources();
			manager = gameObject.AddComponent<MyActionManager>() as MyActionManager;
			judge = new Judge(start_land, end_land, boat);
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
			//boat.BoatMove();
			manager.moveBoat(boat.GetGameObject(), boat.BoatMove(), boat.speed);
			gui.sign = judge.Check();
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
				//r.Move(land.GetEmptyPosition());
				manager.moveRole(r.GetGameObject(), new Vector3(r.GetGameObject().transform.position.x, land.GetEmptyPosition().y, land.GetEmptyPosition().z), land.GetEmptyPosition(), r.speed);
				r.GoLand(land);
				land.AddRole(r);
			}
			else{
				LandModel land = r.GetLandModel();
				if (boat.GetEmptyNumber() == -1 || land.GetLandSign() != boat.GetBoatSign()) {
					return;
				}   
				land.DeleteRoleByName(r.GetName());
				//r.Move(boat.GetEmptyPosition());
				manager.moveRole(r.GetGameObject(), new Vector3(boat.GetEmptyPosition().x, r.GetGameObject().transform.position.y, boat.GetEmptyPosition().z), boat.GetEmptyPosition(), r.speed);
				r.GoBoat(boat);
				boat.AddRole(r);
			}
			gui.sign = judge.Check();
		}

		public void Restart(){
			SceneManager.LoadScene(0);
		}
	}
}

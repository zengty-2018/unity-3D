using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModelController{
	public class RoleModel{
		GameObject role;
		int roleSign;             
		Click click;
		bool on_boat;              
		Move move;
		LandModel land_model = (SSDirector.GetInstance().CurrentScenceController as Controller).start_land;

		public RoleModel(string role_name){
			if (role_name == "priest"){
				role = Object.Instantiate(Resources.Load("Prefabs/Priest", typeof(GameObject)), Vector3.zero, Quaternion.Euler(0, -90, 0)) as GameObject;
				roleSign = 0;
			}
			else{
				role = Object.Instantiate(Resources.Load("Prefabs/Devil", typeof(GameObject)), Vector3.zero, Quaternion.Euler(0, -90, 0)) as GameObject;
				roleSign = 1;
			}
			move = role.AddComponent(typeof(Move)) as Move;
			click = role.AddComponent(typeof(Click)) as Click;
			click.SetRole(this);
		}
		public int GetSign() { 
			return roleSign;
		}
		public LandModel GetLandModel(){
			return land_model;
		}
		public string GetName() { 
			return role.name; 
		}
		public bool IsOnBoat() { 
			return on_boat; 
		}
		public void SetName(string name) { 
			role.name = name; 
		}
		public void SetPosition(Vector3 pos) { 
			role.transform.position = pos; 
		}
		public void Move(Vector3 vec){
			move.MovePosition(vec);
		}
		public void GoLand(LandModel land){  
			role.transform.parent = null;
			land_model = land;
			on_boat = false;
		}
		public void GoBoat(BoatModel boat){
			role.transform.parent = boat.GetBoat().transform;
			land_model = null;          
			on_boat = true;
		}
	}

	public class BoatModel{
		GameObject boat;                                          
		Vector3[] start_empty_pos;                                    
		Vector3[] end_empty_pos;                                      
		Move move;                                                    
		Click click;
		int boat_sign = 1;                                                     
		RoleModel[] roles = new RoleModel[2];                                  

		public BoatModel(){
			boat = Object.Instantiate(Resources.Load("Prefabs/Boat", typeof(GameObject)), new Vector3(10, 3.5F, 0), Quaternion.identity) as GameObject;

			boat.name = "boat";
			move = boat.AddComponent(typeof(Move)) as Move;
			click = boat.AddComponent(typeof(Click)) as Click;
			click.SetBoat(this);
			start_empty_pos = new Vector3[] { new Vector3(12, 5.5F, 0), new Vector3(8, 5.5F, 0) };
			end_empty_pos = new Vector3[] { new Vector3(-8, 5.5F, 0),  new Vector3(-12, 5.5F, 0)};
		}

		public bool IsEmpty(){
			for (int i = 0; i < roles.Length; i++){
				if (roles[i] != null){
					return false;
				}
			}
			return true;
		}

		public void BoatMove(){
			if (boat_sign == -1){
				move.MovePosition(new Vector3(10, 3.5F, 0));
				boat_sign = 1;
			}
			else{
				move.MovePosition(new Vector3(-10, 3.5F, 0));
				boat_sign = -1;
			}
		}

		public int GetBoatSign(){ 
			return boat_sign;
		}

		public RoleModel DeleteRoleByName(string role_name){
			for (int i = 0; i < roles.Length; i++){
				if (roles[i] != null && roles[i].GetName() == role_name){
					RoleModel role = roles[i];
					roles[i] = null;
					return role;
				}
			}
			return null;
		}

		public int GetEmptyNumber(){
			for (int i = 0; i < roles.Length; i++){
				if (roles[i] == null){
					return i;
				}
			}
			return -1;
		}

		public Vector3 GetEmptyPosition(){
			Vector3 pos;
			if (boat_sign == -1)
				pos = end_empty_pos[GetEmptyNumber()];
			else
				pos = start_empty_pos[GetEmptyNumber()];
			return pos;
		}

		public void AddRole(RoleModel role){
			roles[GetEmptyNumber()] = role;
		}

		public GameObject GetBoat(){ 
			return boat;
		}

		public int[] GetRoleNumber(){
			int[] num = { 0, 0 };
			for (int i = 0; i < roles.Length; i++){
				if (roles[i] == null)
					continue;
				if (roles[i].GetSign() == 0)
					num[0]++;
				else
					num[1]++;
			}
			return num;
		}
	}

	public class LandModel{
		GameObject land;                                
		Vector3[] positions;                            
		int land_sign;                                  
		RoleModel[] roles = new RoleModel[6];

		public LandModel(string land_mark){
			positions = new Vector3[] {new Vector3(39F,6.5F,0), new Vector3(35F,6.5F,0), new Vector3(31F,6.5F,0),
				new Vector3(27F,6.5F,0), new Vector3(23F,6.5F,0), new Vector3(19F,6.5F,0)};
			if (land_mark == "start"){
				land = Object.Instantiate(Resources.Load("Prefabs/Land", typeof(GameObject)), new Vector3(27, 0, 0), Quaternion.identity) as GameObject;
				land_sign = 1;
			}
			else if(land_mark == "end"){
				land = Object.Instantiate(Resources.Load("Prefabs/Land", typeof(GameObject)), new Vector3(-27, 0, 0), Quaternion.identity) as GameObject;
				land_sign = -1;
			}
		}

		public int GetEmptyNumber()                      {
			for (int i = 0; i < roles.Length; i++){
				if (roles[i] == null)
					return i;
			}
			return -1;
		}

		public int GetLandSign() { 
			return land_sign; 
		}

		public Vector3 GetEmptyPosition()               {
			Vector3 pos = positions[GetEmptyNumber()];
			pos.x = land_sign * pos.x;                  
			return pos;
		}

		public void AddRole(RoleModel role)             {
			roles[GetEmptyNumber()] = role;
		}

		public RoleModel DeleteRoleByName(string role_name)      { 
			for (int i = 0; i < roles.Length; i++){
				if (roles[i] != null && roles[i].GetName() == role_name){
					RoleModel role = roles[i];
					roles[i] = null;
					return role;
				}
			}
			return null;
		}

		public int[] GetRoleNum(){
			int[] num = { 0, 0 };                    
			for (int i = 0; i < roles.Length; i++){
				if (roles[i] != null){
					if (roles[i].GetSign() == 0)
						num[0]++;
					else
						num[1]++;
				}
			}
			return num;
		}	
	}

	public class Move : MonoBehaviour{
		float move_speed = 250;                   
		int move_sign = 0;                        
		Vector3 end_pos;
		Vector3 middle_pos;

		void Update(){
			if (move_sign == 1){
				transform.position = Vector3.MoveTowards(transform.position, middle_pos, move_speed * Time.deltaTime);
				if (transform.position == middle_pos)
					move_sign = 2;
			}
			else if (move_sign == 2){
				transform.position = Vector3.MoveTowards(transform.position, end_pos, move_speed * Time.deltaTime);
				if (transform.position == end_pos)
					move_sign = 0;           
			}
		}
		public void MovePosition(Vector3 position){
			end_pos = position;
			if (position.y == transform.position.y)         {  
				move_sign = 2;
			}
			else if (position.y < transform.position.y)      {
				middle_pos = new Vector3(position.x, transform.position.y, position.z);
			}
			else                                          {
				middle_pos = new Vector3(transform.position.x, position.y, position.z);
			}
			move_sign = 1;
		}
	}
}
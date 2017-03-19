using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBaseInfo :RoleInfo {
	private Animator ani;
	// Use this for initialization
	void Start () {
		BaseStart ();
		ani = GetComponent <Animator> ();
		colRole = GetComponent <Collider> ();
	}
	void Update(){
		if (Hp <=0) {
			Hp = 0;
			ani.SetTrigger ("CanDeath");
			colRole.enabled = false;

		}
		
	}	
	public override void GetHurt (int hurt_Physic, int hurt_Magic,Type_Allrole role)
	{
		if (!IsDeath) {
			Hp = Hp -hurt_Physic + hurt_Physic * DefensePhysical / (DefensePhysical + 100) -hurt_Magic + hurt_Magic * DefenseMagic / (DefenseMagic + 100);
			if (Hp <=0) {
				Hp = 0;
				IsDeath = true;
				ani.SetTrigger ("CanDeath");
				colRole.enabled = false;
			}
		}
	}
	public override void GetHurt(int hurt_Physic, int hurt_Magic,Role_Main roleMain)                    
	{ 
		if (IsDeath ==false) {
			Hp = Hp - hurt_Physic + hurt_Physic * DefensePhysical / (DefensePhysical + 100) - hurt_Magic + hurt_Magic * DefenseMagic / (DefenseMagic + 100);
			if (Hp <=0) {
				roleMain.ReceiveExpAndGold (worthExp, worthMoney);
				IsDeath = true;
				ani.SetTrigger ("CanDeath");
				colRole.enabled = false;
			}
		}
	}
}

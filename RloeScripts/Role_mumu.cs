using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine .AI ;
public class Role_mumu :Role_Main {
	public override IEnumerator Start(){
		StartCoroutine(base.Start());
		StartCoroutine (base.Start ());
		type_Range = Type_Range.Near;
		yield break;
	}

	public override void Akt_normal ()
	{
		if (target !=null ) {
			if (Vector3 .Distance (transform .position ,target .position )>=0.5f) {
				SetMoveTarget(target.position );
			}
			ani.SetTrigger ("Akt");
			target.GetComponent <Role_Main> ().GetHurt (attack_Physical, 0,Type_Allrole.role);
		}
	}
	public override void SetSkill_Q ()
	{
		ani.SetTrigger ("CanSkill_Q");
	}
	public override void  SetSkill_W ()
	{
		ani.SetTrigger ("CanSkill_W");

	}
	public override void  SetSkill_E ()
	{
		ani.SetTrigger ("CanSkill_E");
	}
	public override void SetSkill_D ()
	{
	}
	public override void SetSkill_F ()
	{
	}
	public override void DoPasssive ()
	{
	}

}

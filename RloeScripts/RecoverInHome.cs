using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoverInHome : MonoBehaviour {
	public Role_Camp camp;
	public float radius;
	Collider[] col;
	// Use this for initialization
	void Start () {
	}
	void OnDrawGizmos(){
		Gizmos.color = new Color(1, 0, 0, 0.2f);
		Gizmos.DrawSphere (transform.position, radius);
	}
	// Update is called once per frame
	void Update () {
		col = Physics.OverlapSphere(transform.position,radius,LayerMask.GetMask(GetOtherCamp(camp)));
		if (col != null) {
			for (int i = 0; i < col.Length; i++) {
				if (col [i].gameObject.GetComponent<RoleInfo> () != null && col [i].gameObject.GetComponent<RoleInfo> ().type_Allrole == Type_Allrole.role) {
					while (col [i].gameObject.GetComponent<RoleInfo> ().Hp < col [i].gameObject.GetComponent<RoleInfo> ().HpMax && col [i].gameObject.GetComponent<RoleInfo> () != null) {
						col [i].gameObject.GetComponent<RoleInfo> ().Hp += 1;
					}
					while (col [i].gameObject.GetComponent<RoleInfo> ().Mp < col [i].gameObject.GetComponent<RoleInfo> ().MpMax && col [i].gameObject.GetComponent<RoleInfo> () != null) {
						col [i].gameObject.GetComponent<RoleInfo> ().Mp += 1;
					}
				}
			}
		}
	}

	string GetOtherCamp(Role_Camp cam){
		if (cam == Role_Camp.Blue)
			return "Blue";
		else
			return "Red";
	}
}

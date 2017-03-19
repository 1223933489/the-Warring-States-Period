using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WuNiangSkillWControl : MonoBehaviour {
	public Role_Camp camp;
	public float radius;
	public Collider [] col;
	public Role_Main owner;
	// Use this for initialization
	void Start () {
		StartCoroutine ("DestoryIt");
		radius = 4f;
	}
	
	// Update is called once per frame
	void Update () {
		col = Physics.OverlapSphere(transform.position,radius,owner.EnemyLayer);
		if (col != null) {
			for (int i = 0; i < col.Length; i++) {
				if (col [i].gameObject.GetComponent<RoleInfo> () != null && col [i].gameObject.GetComponent<RoleInfo> ().type_Allrole != Type_Allrole.tower && !col[i].gameObject.GetComponent<RoleInfo>().IsDeath) {
					col [i].gameObject.GetComponent<RoleInfo> ().transform.position = transform.position;
				}
			}
		}
	}

	IEnumerator DestoryIt(){
		yield return new WaitForSeconds (3f);
		Destroy (this.gameObject);
	}

	string GetOtherCamp(Role_Camp cam){
		if (cam == Role_Camp.Blue)
			return "Red";
		else
			return "Blue";
	}

	void OnDrawGizmos(){
		Gizmos.color = new Color(1, 0, 0, 1);
		Gizmos.DrawSphere (transform.position, radius);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WuNiangSkillQControl : MonoBehaviour {
	public Role_Camp camp;
	public Role_Main owner;
	// Use this for initialization
	void Start () {
		StartCoroutine ("DestoryIt");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider col){
		if (col != null && col.gameObject.GetComponent<RoleInfo> () != null && !col.gameObject.GetComponent<RoleInfo>().IsDeath) {
			if (camp != col.gameObject.GetComponent<RoleInfo> ().roleCamp) {
				//				col.gameObject.GetComponent<RoleInfo> ().Hp -= 199;
				col.gameObject.GetComponent<RoleInfo>().GetHurt(50,50,owner);
			} 
		}
	}

	IEnumerator DestoryIt(){
		yield return new WaitForSeconds (3f);
		Destroy (this.gameObject);
	}
}

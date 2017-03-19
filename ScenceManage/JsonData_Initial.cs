using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonData_Initial : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
		Data_Rloe role_JianMo = GetRoleJson ("JianMo");
		Data_Rloe role_WuNiang = GetRoleJson ("WuNiang");
		Data_Rloe role_XiaoYaoZi = GetRoleJson ("XiaoYaoZi");
		Data_Rloe role_YangJian = GetRoleJson ("YangJian");
		Data_Rloe role_ChangE = GetRoleJson ("ChangE");
		Data_Rloe role_Ahri = GetRoleJson("Ahri");
		ChangeRoleData (ref role_JianMo, 5);
		ChangeRoleData (ref role_WuNiang, 5);
		ChangeRoleData (ref role_XiaoYaoZi, 3);
		ChangeRoleData (ref role_YangJian, 1);
		ChangeRoleData (ref role_ChangE, 5);
		ChangeRoleData (ref role_Ahri, 5);
		SaveJsonData (role_JianMo, "JianMo");
		SaveJsonData (role_WuNiang, "WuNiang");
		SaveJsonData (role_XiaoYaoZi, "XiaoYaoZi");
		SaveJsonData (role_YangJian, "YangJian");
		SaveJsonData (role_ChangE, "ChangE");
		SaveJsonData (role_Ahri, "Ahri");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void ChangeRoleData(ref Data_Rloe name, int atk_r){
		name.moveSpeed = 3;
		name.HpMax = 550;
		name.attack_Radius = atk_r;
	}

	Data_Rloe GetRoleJson(string name){
		Data_Rloe hero = JsonUti.JsonstreamToObject<Data_Rloe>(Application.dataPath + "/InitializeInfo/HeroData/"+ name + ".text");
		return hero;
	}

	void SaveJsonData(Data_Rloe role, string name){
		string path=Application.dataPath + "/InitializeInfo/HeroData/"+name + ".text" ;
		JsonUti.ObjectToJsonStream <Data_Rloe> (path, role);
	}
}

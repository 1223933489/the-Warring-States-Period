using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HeroAi : MonoBehaviour {

	public RoleInfo roleInfo;
	public Role_Main roleMain;
	public TargetOfAI targetOfAI;

	public float atkTimeCount;
	public float Q_skillTimeCount;
	public float W_skillTimeCount;
	public float E_skillTimeCount;
	bool isGet;

	public Animator ani;

	public UIManager uimanager;
	public SkillManager skillmanager;

	public AIOfHeroState aiOfHeroState;

	private int roadLine;

	NavMeshAgent agent;

	void Awake(){
	}

	void Start () {
		roleInfo = transform.GetComponent<RoleInfo> ();
		roleMain = transform.GetComponent<Role_Main> ();
		ani = transform.GetComponent<Animator> ();
		agent = GetComponent <NavMeshAgent> ();
		uimanager = GameObject.Find ("UI").GetComponent<UIManager> ();
		skillmanager = transform.GetComponent<SkillManager> ();
		roadLine = roleMain.roadLine;
			
		aiOfHeroState = AIOfHeroState.patrol;
		isGet = false;
		if (uimanager.mapSelect == MapSelect.oneVSone) {
			if (roleInfo.roleCamp == Role_Camp.Blue) {
				targetOfAI.target01 = GameObject.Find ("Tower_Red_01").transform;
				targetOfAI.target02 = GameObject.Find ("RebirthPos_Blue").transform;
				targetOfAI.target03 = GameObject.Find ("shuiJingRedRoot").transform;		
			} else {
				targetOfAI.target01 = GameObject.Find ("Tower_Blue_01").transform;
				targetOfAI.target02 = GameObject.Find ("RebirthPos_Red").transform;
				targetOfAI.target03 = GameObject.Find ("shuiJingBlueRoot").transform;
			}
		} else if (uimanager.mapSelect == MapSelect.threeVSthree) {
			if (roleInfo.roleCamp == Role_Camp.Blue) {
				if (roadLine == 1) {
					targetOfAI.target_Roadpoint = GameObject.Find ("PathSolider/Path (3)").transform;
					targetOfAI.target01 = GameObject.Find ("Environment/Building/A_Towers/A_Tower_X02").transform;
				} else if (roadLine == 2) {
					targetOfAI.target_Roadpoint = GameObject.Find ("PathSolider_z/Path_z (2)").transform;
					targetOfAI.target01 = GameObject.Find ("Environment/Building/A_Towers/A_Tower_Z02").transform;
				}else if (roadLine == 3) {
					targetOfAI.target_Roadpoint = GameObject.Find ("PathSolider_s/Path_s (4)").transform;
					targetOfAI.target01 = GameObject.Find ("Environment/Building/A_Towers/A_Tower_S02").transform;
				}
				targetOfAI.target02 = GameObject.Find ("RebirthPos_Blue").transform;
					targetOfAI.target03 = GameObject.Find ("shuiJingRedRoot").transform;						
				
			} else {
				if (roadLine == 1) {
					targetOfAI.target_Roadpoint = GameObject.Find ("PathSolider_s/Path_s (4)").transform;
					targetOfAI.target01 = GameObject.Find ("Environment/Building/B_Towers/B_Tower_X02").transform;
				} else if (roadLine == 2) {
					targetOfAI.target_Roadpoint = GameObject.Find ("PathSolider_z/Path_z (2)").transform;
					targetOfAI.target01 = GameObject.Find ("Environment/Building/B_Towers/B_Tower_Z02").transform;
				}else if (roadLine == 3) {
					targetOfAI.target_Roadpoint = GameObject.Find ("PathSolider/Path (3)").transform;
					targetOfAI.target01 = GameObject.Find ("Environment/Building/B_Towers/B_Tower_S02").transform;
				}
				targetOfAI.target02 = GameObject.Find ("RebirthPos_Red").transform;
				targetOfAI.target03 = GameObject.Find ("shuiJingBlueRoot").transform;
			}
		}
	}
	

	void Update () {
		if (roleInfo.IsDeath) {
			agent.Stop ();
			aiOfHeroState = AIOfHeroState.death;
		}
		if (skillmanager.canAddSkill_Q) {
			skillmanager.AddSkill_Q ();
			skillmanager.CanShowAdd ();
		}
		if (skillmanager.canAddSkill_W) {
			skillmanager.AddSkill_W ();
			skillmanager.CanShowAdd ();
		}
		if (skillmanager.canAddSkill_E) {
			skillmanager.AddSkill_E ();
			skillmanager.CanShowAdd ();
		}
		if (roleInfo.Hp > 0) {
		
			
			Collider[] col = Physics.OverlapSphere (transform.position, 10.0f, roleInfo.EnemyLayer);
			
			//进入残血状态
			if (roleInfo.Hp <= (roleInfo.HpMax / 2)  &&roleInfo .Hp >=0) {
				aiOfHeroState = AIOfHeroState.halfBlood;
			}
			//进入遇敌状态
			if (col.Length > 0 && aiOfHeroState != AIOfHeroState.halfBlood && roleInfo.Hp > 0) {
				aiOfHeroState = AIOfHeroState.meetEnemy;													
			}
			//进入巡逻状态
			if (aiOfHeroState != AIOfHeroState.halfBlood && col.Length == 0 && roleInfo.Hp > 0) {
				aiOfHeroState = AIOfHeroState.patrol;
			}
			
			
			//残血状态
			if (aiOfHeroState == AIOfHeroState.halfBlood) {
				roleMain.SetMoveTarget (targetOfAI.target02.position);
				if (roleInfo.Hp == roleInfo.HpMax) {
					aiOfHeroState = AIOfHeroState.patrol;
				}
			}


			//巡逻状态
			if (aiOfHeroState == AIOfHeroState.patrol && col.Length == 0) {
				Transform target = targetOfAI.target02;
				if (uimanager.mapSelect == MapSelect.oneVSone) {
					if (targetOfAI.target01 == null) {
						target = targetOfAI.target03;
						roleMain.SetMoveTarget(target.position);
					} else {
						target = targetOfAI.target01;
						roleMain.SetMoveTarget(target.position);
					}
				} else if (uimanager.mapSelect == MapSelect.threeVSthree) {
					if (!isGet) {
						roleMain.SetMoveTarget(targetOfAI.target_Roadpoint.position);
					} else {
						if (targetOfAI.target01 == null) {
							target = targetOfAI.target03;
							roleMain.SetMoveTarget (target.position);
						} else {
							target = targetOfAI.target01;
							roleMain.SetMoveTarget(target.position);
						}			
					}
				}
			}
			//遇敌状态
			if (aiOfHeroState == AIOfHeroState.meetEnemy && roleInfo.Hp>0) {
				atkTimeCount += Time.deltaTime;
				Q_skillTimeCount += Time.deltaTime;
				W_skillTimeCount += Time.deltaTime;
				E_skillTimeCount += Time.deltaTime;
				//获取范围内敌人信息，跟随最近敌人
				float minDistance = Vector3.Distance (transform.position, col [0].transform.position);
				int chioce = 0;
				for (int i = 0; i < col.Length; i++) {
					float currentDis = Vector3.Distance (transform.position, col [i].transform.position);
					if (currentDis < minDistance) {
						chioce = i;
					}
				}
				if (col [chioce].transform != null) {
					roleMain.SetMoveTarget (col [chioce].transform.position);
				}
				//在一定范围内攻击
				if (Vector3.Distance (transform.position, col [chioce].transform.position) <= roleInfo.attack_Radius) {			
//					if (atkTimeCount >= (1 / roleInfo.attack_Speed)) {
//						//英雄普通攻击
//						roleMain.Akt_normal ();
//						atkTimeCount = 0;
//					}
					if (Q_skillTimeCount >= 3f) {
						//英雄技能Q
						roleMain.SetSkill_Q ();
						Q_skillTimeCount = 0;
					}
					if (W_skillTimeCount >= 5f) {
						//英雄技能W
						roleMain.SetSkill_W ();
						W_skillTimeCount = 0;
					}
					if (E_skillTimeCount >= 7f) {
						//英雄技能E
						roleMain.SetSkill_W ();
						E_skillTimeCount = 0;
					}
				}
				if (col.Length == 0) {
					aiOfHeroState = AIOfHeroState.patrol;
				}
			}
		}
	}
}



public enum AIOfHeroState
{
	patrol,
	meetEnemy,
	halfBlood,
	death
}
[System .Serializable]
public struct TargetOfAI
{
	public Transform target01;
	public Transform target02;
	public Transform target03;
	public Transform target_Roadpoint;
}

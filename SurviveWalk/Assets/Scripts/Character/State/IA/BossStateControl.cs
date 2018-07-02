using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateControl : EnemyStateControl {


    protected override void InitAwake()
    {
        base.InitAwake();
        //enemyStatus = GetComponentInChildren<EnemyController>();
        //gameObject.name = transform.GetInstanceID() + "-" + gameObject.name;
        AniControl = GetComponentInChildren<EnemyAnimationControl>();
        //moveControl = GetComponentInChildren<EnemyMoveControl>();
        //boxCol = GetComponentInChildren<BoxCollider>();

        //if (aniControl != null)
        //    aniControl.Weapon = Weapon;
        //else
        //    Debug.Log("[EnemyStateControl][InitAwake]: EnemyAnimationControl is null!");
    }

    // Use this for initialization
    void Start () {
		
	}

    protected override void InitStart()
    {
        base.InitStart();
        #region WayPoints
        //path = GetComponent<Path>();
        //if (path != null)
        //{
        //    bool isError = false;
        //    listWayPoints = path.WayPoints;
        //    if (listWayPoints == null)
        //    {
        //        isError = true;
        //        Debug.Log("[EnemyState][" + gameObject.name + "]: listWayPoins nula.");
        //    }
        //    else
        //    {
        //        if (listWayPoints.Count == 0)
        //        {
        //            Debug.Log("[EnemyState][" + gameObject.name + "]: listWayPoins vazia.");
        //            isError = true;
        //        }
        //        else
        //        {
        //            for (int i = 0; i < listWayPoints.Count; i++)
        //            {
        //                if (listWayPoints[i] == null)
        //                {
        //                    listWayPoints.Clear();
        //                    listWayPoints.Add(transform);
        //                    Debug.Log("[EnemyState][" + gameObject.name + "]: A posição (" + i + ") da listWayPoins está nula.");
        //                    isError = true;
        //                    break;
        //                }
        //            }
        //        }
        //    }

        //    if (isError)
        //    {
        //        listWayPoints = new List<Transform>();
        //        listWayPoints.Add(transform);
        //    }

        //}
        //monitoringPoint = listWayPoints[actualWp].position;
        #endregion

        //moveControl.BodyMiniMap.gameObject.SetActive(false);
        //playerTarget = IcarusPlayerController.GetInstance();
        //playerStatus = playerTarget.gameObject.GetComponent<CharacterStatus>();

        //if (state != TypeStateCharacter.FakeDead)
        //{
        //    if (state == TypeStateCharacter.Patrol)
        //        isPatrolStart = true;
        //    EnterState(TypeStateCharacter.Rise);
        //}
        //else
        //    EnterState(state);
    }

    // Update is called once per frame
    void Update () {
        UpdateState();
        AniControl.ExecuteAnimations();
    }


}

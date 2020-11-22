using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolActionManager : SSActionManager, ISSActionCallback{
    public void GoPatrol(GameObject patrol)
    {
        GoPatrolAction go_patrol = GoPatrolAction.GetSSAction(patrol.transform.position);
        this.RunAction(patrol, go_patrol, this);
    }

    public void DestroyAllAction(){
        DestroyAll();
    }

    public void SSActionEvent(SSAction source, int intParam = 0, GameObject objectParam = null){
        if (intParam == 0){
            PatrolFollowAction follow = PatrolFollowAction.GetSSAction(objectParam.gameObject.GetComponent<PatrolData>().player);
            this.RunAction(objectParam, follow, this);
        }
        else{
            GoPatrolAction move = GoPatrolAction.GetSSAction(objectParam.gameObject.GetComponent<PatrolData>().start_position);
            this.RunAction(objectParam, move, this);
            
            Singleton<GameEventManager>.Instance.PlayerEscape();
        }
    }
}

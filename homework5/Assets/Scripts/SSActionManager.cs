using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSActionManager : MonoBehaviour {
    private Dictionary<int, SSAction> actionsRunning = new Dictionary<int, SSAction>();
    private List<SSAction> actionToRun = new List<SSAction>();
    private List<int> actionToDel = new List<int>();   

    protected void Update() {
        foreach (SSAction action in actionToRun) {
            actionsRunning[action.GetInstanceID()] = action;
        }
        actionToRun.Clear();

        foreach (KeyValuePair<int, SSAction> actionKV in actionsRunning) {
            SSAction action = actionKV.Value;
            if (action.enable) {
                action.Update();
            } 
            else {
                actionToDel.Add(action.GetInstanceID());
            }
        }

        foreach (int key in actionToDel) {
            SSAction action = actionsRunning[key];
            actionsRunning.Remove(key);
            Object.Destroy(action);
        }
        actionToDel.Clear();
    }

    public void StartAction(GameObject gameobject, SSAction action) {
        action.gameobject = gameobject;
        action.transform = gameobject.transform;
        actionToRun.Add(action);
        action.Start();
    }

}
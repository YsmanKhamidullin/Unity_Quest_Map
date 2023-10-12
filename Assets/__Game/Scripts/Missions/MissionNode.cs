using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MissionNode : MonoBehaviour
{
    public Mission TargetMission => targetMission;
    [SerializeField] protected Mission targetMission;
    [SerializeField] protected TextMeshProUGUI missionNumberLabel;

    private void OnDrawGizmos()
    {
        UpdateScreenPos();
    }
    private void UpdateScreenPos()
    {
        if (targetMission == null)
        {
            return;
        }

        var screenResolution = Screen.currentResolution;
        var screenSize = new Vector2(screenResolution.width, screenResolution.height);
        var missionScreenPos = targetMission.ScreenPos;

        var max = 100f;
        var missionXPercent = Mathf.Clamp(missionScreenPos.x, 0f, max) / max;
        var missionYPercent = Mathf.Clamp(missionScreenPos.y, 0f, max) / max;
        var x = Mathf.Lerp(0, screenSize.x, missionXPercent);
        var y = Mathf.Lerp(0, screenSize.y, missionYPercent);
        transform.position = new Vector3(x, y, 0);
    }

    public virtual void SetMission(Mission mission, string missionNumber)
    {
        targetMission = mission;
        missionNumberLabel.text = missionNumber;
    }
}
using TMPro;
using UnityEngine;

public class DoubleMissionNode : BaseMissionNode
{
    public Mission SecondTargetMission => secondTargetMission;
    [SerializeField] protected Mission secondTargetMission;
    [SerializeField] protected TextMeshProUGUI secondMissionNumberLabel;

    public override void SetMission(Mission mission, string missionNumber)
    {
        base.SetMission(mission, missionNumber + "-1");
        secondTargetMission = mission.SecondMission;
        secondMissionNumberLabel.text = missionNumber + "-2";
    }

    public override void CalculateCurrentState()
    {
        gameObject.SetActive(true);
        nodeButton.interactable = false;

        MissionState firstMissionState = targetMission.MissionState;
        MissionState secondMissionState = secondTargetMission.MissionState;
        bool isAllBlocked = firstMissionState == MissionState.Blocked && secondMissionState == MissionState.Blocked;
        if (isAllBlocked)
        {
            gameObject.SetActive(false);
            return;
        }

        bool isAnyActive = firstMissionState == MissionState.Active || secondMissionState == MissionState.Active;
        if (isAnyActive)
        {
            nodeButton.interactable = true;
        }
    }
}
using TMPro;
using UnityEngine;

public class DoubleMissionNode : MissionNode
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
}
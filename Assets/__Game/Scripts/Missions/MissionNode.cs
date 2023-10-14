using System;

public class MissionNode : BaseMissionNode
{
    public override void CalculateCurrentState()
    {
        gameObject.SetActive(true);
        switch (TargetMission.MissionState)
        {
            case MissionState.Active:
                nodeButton.interactable = true;
                break;
            case MissionState.Blocked:
                gameObject.SetActive(false);
                break;
            case MissionState.TemporarilyBlocked:
                nodeButton.interactable = false;
                break;
            case MissionState.Completed:
                nodeButton.interactable = false;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
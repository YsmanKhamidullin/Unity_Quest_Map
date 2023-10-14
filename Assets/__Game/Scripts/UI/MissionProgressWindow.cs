using TMPro;
using UnityEngine;

public class MissionProgressWindow : BaseMissionInfoWindow
{
    [SerializeField] private TextMeshProUGUI playerSideLabel;
    [SerializeField] private TextMeshProUGUI enemyLabel;

    public override void Init(Mission mission)
    {
        base.Init(mission);
        descriptionLabel.text = mission.InMissionDescription;
        playerSideLabel.text = mission.PlayerSide;
        enemyLabel.text = mission.EnemySide;
    }
}

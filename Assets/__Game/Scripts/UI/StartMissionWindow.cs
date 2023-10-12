using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartMissionWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI missionNameLabel;
    [SerializeField] private TextMeshProUGUI beforeMissionDescriptionLabel;
    [SerializeField] private Image missionImage;

    public void Init(Mission mission)
    {
        missionNameLabel.text = mission.MissionName;
        beforeMissionDescriptionLabel.text = mission.BeforeMissionDescription;
    }
}
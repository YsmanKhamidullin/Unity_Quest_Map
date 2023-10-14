public class StartMissionWindow : BaseMissionInfoWindow
{
    public override void Init(Mission mission)
    {
        base.Init(mission);
        descriptionLabel.text = mission.BeforeMissionDescription;
    }
}
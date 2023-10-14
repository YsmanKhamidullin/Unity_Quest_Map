using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create MissionMap", fileName = "MissionMap", order = 0)]
public class MissionMap : ScriptableObject
{
    public List<Mission> Missions => missions;
    [SerializeField] private List<Mission> missions;

    public MissionMap GetCopy()
    {
        var newMap = Instantiate(this);
        
        newMap.missions = new List<Mission>();
        foreach (var sourceMission in missions)
        {
            var copiedMission = sourceMission.GetCopy();
            newMap.missions.Add(copiedMission);
        }
        return newMap;
    }

    public void AttachHeroesRefs(List<Hero> heroes)
    {
        foreach (var mission in missions)
        {
            mission.SetHeroRefs(heroes);
        }
    }

    public void AttachMapRefs(List<Mission> newMission)
    {
        foreach (var mission in missions)
        {
            mission.SetMapRefs(newMission);
        }
    }
}
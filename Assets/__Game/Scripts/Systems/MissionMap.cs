using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create MissionMap", fileName = "MissionMap", order = 0)]
public class MissionMap : ScriptableObject
{
    public List<Mission> Missions => missions;

    [SerializeField] private List<Mission> missions;
}
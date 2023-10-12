using System.Collections.Generic;
using NaughtyAttributes;
using UnityEditor;
using UnityEngine;

public class MissionMapWindow : MonoBehaviour
{
    [SerializeField] private MissionMap missions;
    [SerializeField] private MissionNode soloMissionPrefab;
    [SerializeField] private MissionNode doubleMissionPrefab;
    [SerializeField] private Transform missionsParent;
    [SerializeField, ReadOnly] private List<MissionNode> nodes;


    [Button]
    public void GenerateMap()
    {
        ClearParent();

       // DoubleMissionNode target = null;
        for (var i = 0; i < missions.Missions.Count; i++)
        {
            var mission = missions.Missions[i];
            var targetPrefab = mission.IsDoubleMission ? doubleMissionPrefab : soloMissionPrefab;
            MissionNode missionNode;
#if UNITY_EDITOR
            missionNode = PrefabUtility.InstantiatePrefab(targetPrefab, missionsParent) as MissionNode;
#else
            missionNode = Instantiate(targetPrefab, missionsParent);
#endif
            missionNode.SetMission(mission, (i + 1).ToString());
            // if (mission.IsDoubleMission)
            // {
            //     target = (DoubleMissionNode)missionNode;
            // }

            nodes.Add(missionNode);
        }

        //var result = GetLoosedNodeFromUnPassedTarget(target);
    }

    private Mission GetLoosedNodeFromUnPassedTarget(DoubleMissionNode target)
    {
        var secondBlockedMissions = target.SecondTargetMission.NextMissions;
        HashSet<Mission> hashSet = new HashSet<Mission> { target.SecondTargetMission };
        Queue<Mission> queue = new Queue<Mission>(secondBlockedMissions);
        queue.Enqueue(secondBlockedMissions[0]);
        Mission curMission = null;
        while (queue.Count > 0)
        {
            curMission = queue.Dequeue();
            int countOfCompleted = 0;
            foreach (var previousMission in curMission.PreviousMissions)
            {
                bool isInHashSet = hashSet.Contains(previousMission);
                if (isInHashSet)
                {
                    countOfCompleted++;
                }
            }

            if (countOfCompleted == curMission.PreviousMissions.Count)
            {
                hashSet.Add(curMission);
                curMission.NextMissions.ForEach(m => queue.Enqueue(m));
            }
        }

        Debug.Log($"Loose node: {curMission}");
        return curMission;
    }

    private void ClearParent()
    {
        var childCount = missionsParent.childCount;
        for (int i = 0; i < childCount; i++)
        {
            var target = missionsParent.GetChild(0).gameObject;
            if (Application.isPlaying)
            {
                Destroy(target);
            }
            else
            {
                DestroyImmediate(target);
            }
        }
    }

    [Button]
    public void RandomizeMissionPos()
    {
        foreach (var mission in missions.Missions)
        {
            mission.RandomizeMissionPos();
        }
    }
}
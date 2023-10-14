using System.Collections.Generic;
using __Game.Scripts.UI;
using NaughtyAttributes;
using UnityEditor;
using UnityEngine;

public class MissionMapScreen : MonoBehaviour
{
    [SerializeField] private MissionNode soloMissionPrefab;
    [SerializeField] private DoubleMissionNode doubleMissionPrefab;
    [SerializeField] private Transform missionsParent;
    [SerializeField] private List<BaseMissionNode> nodes;
    [SerializeField] private List<StartMissionWindow> startMissionWindows;
    [SerializeField] private MissionProgressWindow missionProgressWindow;
    [SerializeField] private HeroPanelsController heroPanelsController;

    private Hero _curSelectedHero;
    private Mission _prevSelectedMission;

    public void Init(List<Mission> missionMapMissions, List<Hero> heroesListHeroes)
    {
        heroPanelsController.Init(heroesListHeroes);
        GenerateMap(missionMapMissions);
        foreach (var missionNode in nodes)
        {
            missionNode.OnNodeSelect += HandleSelectNode;
        }

        heroPanelsController.OnSelect += SetCurrentSelectedHero;
    }

    private void SetCurrentSelectedHero(Hero hero)
    {
        _curSelectedHero = hero;
        Debug.Log($"Hero {_curSelectedHero.HeroName} Selected");
    }

    public void GenerateMap(List<Mission> missionMapMissions)
    {
        ClearParent();

        // DoubleMissionNode target = null;
        int missionNumber = 0;
        for (var i = 0; i < missionMapMissions.Count; i++)
        {
            missionNumber++;
            var mission = missionMapMissions[i];
            if (mission.IsSecondMission)
            {
                missionNumber--;
                continue;
            }

            BaseMissionNode targetPrefab = mission.IsDoubleMission ? doubleMissionPrefab : soloMissionPrefab;
            BaseMissionNode missionNode;
#if UNITY_EDITOR
            missionNode = PrefabUtility.InstantiatePrefab(targetPrefab, missionsParent) as BaseMissionNode;
#else
            missionNode = Instantiate(targetPrefab, missionsParent);
#endif
            missionNode.SetMission(mission, missionNumber.ToString());
            missionNode.UpdateScreenPos();
            nodes.Add(missionNode);
        }
    }

    #region MissionWindows

    // Select mission Node -> Open preview -> Open complete -> Complete Mission

    private void HandleSelectNode(Mission selectedMission)
    {
        foreach (var startMissionWindow in startMissionWindows)
        {
            startMissionWindow.UnInit();
        }

        if (_prevSelectedMission != null && _prevSelectedMission == selectedMission)
        {
            _prevSelectedMission = null;
            return;
        }

        _prevSelectedMission = selectedMission;

        startMissionWindows[0].Init(selectedMission);
        startMissionWindows[0].OnCompleteWindow += HandleStartMission;
        if (selectedMission.IsDoubleMission)
        {
            startMissionWindows[1].Init(selectedMission.SecondMission);
            startMissionWindows[1].OnCompleteWindow += HandleStartMission;
        }
    }

    /// <summary>
    /// Require hero select
    /// </summary>
    /// <param name="selectedMission"></param>
    private void HandleStartMission(Mission selectedMission)
    {
        if (_curSelectedHero == null)
        {
            ShowSelectHeroNotify();
            return;
        }

        foreach (var startMissionWindow in startMissionWindows)
        {
            startMissionWindow.UnInit();
        }

        missionProgressWindow.Init(selectedMission);
        missionProgressWindow.OnCompleteWindow += HandleCompleteMission;
    }

    private void ShowSelectHeroNotify()
    {
        Debug.Log("You need hero for mission.");
    }

    private void HandleCompleteMission(Mission completedMission)
    {
        missionProgressWindow.UnInit();
        completedMission.Complete();
        _curSelectedHero.AddScore(completedMission.AttachedHeroScore);
        Debug.Log($"{completedMission.MissionName} completed");
        foreach (var node in nodes)
        {
            node.CalculateCurrentState();
        }

        if (completedMission.IsSecondMission || completedMission.IsDoubleMission)
        {
            Mission ignoredMission = completedMission.IsDoubleMission
                ? completedMission.SecondMission
                : completedMission.ParentMission;
            FreeNodeFromIgnored(ignoredMission);
        }

        heroPanelsController.UnSelect();
        _curSelectedHero = null;
    }

    #endregion

    private void FreeNodeFromIgnored(Mission notCompletedMission)
    {
        var secondBlockedMissions = notCompletedMission.NextMissions;
        HashSet<Mission> hashSet = new HashSet<Mission> { notCompletedMission };
        Queue<Mission> queue = new Queue<Mission>(secondBlockedMissions);
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
            curMission.RemoveRequiredMissions(hashSet);
        }
    }


    private void ClearParent()
    {
        foreach (var node in nodes)
        {
            var target = node.gameObject;
            if (Application.isPlaying)
            {
                Destroy(target);
            }
            else
            {
                DestroyImmediate(target);
            }
        }

        nodes.Clear();
    }
}
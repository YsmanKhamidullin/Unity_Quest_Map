using System;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "CustomSO/Create Mission", fileName = "Mission", order = 0)]
public class Mission : ScriptableObject, IId
{
    #region PublicProperties

    public int ID => id;
    public string MissionName => missionName;
    public string BeforeMissionDescription => beforeMissionDescription;
    public string InMissionDescription => inMissionDescription;
    public string PlayerSide => playerSide;
    public string EnemySide => enemySide;
    public Vector2 ScreenPos => screenPos;
    public bool IsDoubleMission => isDoubleMission;
    public bool IsSecondMission => isSecondMission;
    public Mission SecondMission => secondMission;
    public Mission ParentMission => parentMission;

    public MissionState MissionState => missionState;

    public List<Mission> PreviousMissions => previousMissions;

    public List<Hero> UnlockingHeroes => unlockingHeroes;

    public List<Mission> NextMissions => nextMissions;

    public int AttachedHeroScore => attachedHeroScore;

    public List<HeroScorePair> GainingScoreHeroes => gainingScoreHeroes;

    #endregion

    #region Labels Descriptions

    [Header("Label Descriptions")] [SerializeField]
    protected string missionName;

    [SerializeField] protected string beforeMissionDescription;
    [SerializeField] protected string inMissionDescription;
    [SerializeField] protected string playerSide;
    [SerializeField] protected string enemySide;

    #endregion

    #region Logic

    [Header("Logic")] [SerializeField] private int id;
    [SerializeField] protected Vector2 screenPos;
    [SerializeField] protected bool isDoubleMission;
    [SerializeField] protected bool isSecondMission;

    [ShowIf("isDoubleMission")] [SerializeField]
    protected Mission secondMission;

    [ShowIf("isSecondMission")] [SerializeField]
    protected Mission parentMission;

    [Tooltip("- Active — i.e. available for passing\n" +
             "- Blocked — unavailable for passing, hidden for the player\n" +
             "- Temporarily blocked — the mission is unavailable for passing, but not hidden for the player. Only an Activated mission can be temporarily blocked.\n" +
             "- Completed — already completed mission continues to be displayed on the map. It is impossible to re-pass already completed missions.")]
    [SerializeField]
    protected MissionState missionState;

    [Tooltip("Should be passed to open this mission")] [SerializeField]
    protected List<Mission> previousMissions;

    [Tooltip("List of heroes that will be unlocked after mission pass")] [SerializeField]
    protected List<Hero> unlockingHeroes;

    [FormerlySerializedAs("blockedMissions")]
    [Tooltip("When mission is not active they Blocked.\n" +
             "When mission is active they Temporarily blocked.\n" +
             "When mission is Completed they Active")]
    [SerializeField]
    protected List<Mission> nextMissions;

    [Tooltip("Heroes that complete mission will gain score?")] [SerializeField]
    protected int attachedHeroScore;

    [SerializeField] protected List<HeroScorePair> gainingScoreHeroes;

    #endregion

    public void Complete()
    {
        missionState = MissionState.Completed;
        if (isDoubleMission)
        {
            secondMission.missionState = MissionState.Blocked;
        }

        if (isSecondMission)
        {
            parentMission.missionState = MissionState.Blocked;
        }

        foreach (var hero in unlockingHeroes)
        {
            hero.Unlock();
        }

        foreach (var heroScorePair in gainingScoreHeroes)
        {
            heroScorePair.hero.AddScore(heroScorePair.addableScore);
        }

        foreach (var mission in nextMissions)
        {
            mission.TryUnlock();
        }
    }

    [Button]
    public void GenerateId()
    {
        id = missionName.GetHashCode();
        if (isDoubleMission)
        {
            id++;
        }
    }

    public Mission GetCopy()
    {
        var copy = Instantiate(this);
        return copy;
    }

    public void SetHeroRefs(List<Hero> heroes)
    {
        SetRefsFromIntersection(ref unlockingHeroes, heroes);
        foreach (var heroScorePair in gainingScoreHeroes)
        {
            var matchHero = heroes.FirstOrDefault(h => h.ID == heroScorePair.hero.ID);
            if (matchHero != null)
            {
                heroScorePair.hero = matchHero;
            }
        }
    }

    public void SetMapRefs(List<Mission> newMission)
    {
        if (isDoubleMission)
        {
            var match = newMission.FirstOrDefault(m => m.ID == secondMission.ID);
            if (match != null)
            {
                secondMission = match;
            }
        }

        if (isSecondMission)
        {
            var match = newMission.FirstOrDefault(m => m.ID == parentMission.ID);
            if (match != null)
            {
                parentMission = match;
            }
        }

        SetRefsFromIntersection(ref nextMissions, newMission);
        SetRefsFromIntersection(ref previousMissions, newMission);
    }

    public void RemoveRequiredMissions(HashSet<Mission> hashSet)
    {
        for (int i = previousMissions.Count - 1; i >= 0; i--)
        {
            if (hashSet.Contains(previousMissions[i]))
            {
                previousMissions.RemoveAt(i);
            }
        }
    }


    private void TryUnlock()
    {
        if (previousMissions.TrueForAll(p => p.missionState == MissionState.Completed))
        {
            missionState = MissionState.Active;
        }
        else
        {
            missionState = MissionState.TemporarilyBlocked;
        }
    }

    private void SetRefsFromIntersection<T>(ref List<T> targetList, List<T> fromList) where T : IId
    {
        for (var index = 0; index < targetList.Count; index++)
        {
            var target = targetList[index];
            var match = fromList.FirstOrDefault(from => from.ID == target.ID);
            if (match != null)
            {
                targetList[index] = match;
            }
        }
    }


    [Serializable]
    public class HeroScorePair
    {
        public Hero hero;
        public int addableScore;
    }
}
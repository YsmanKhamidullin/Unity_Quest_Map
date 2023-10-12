using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "CustomSO/Create Mission", fileName = "Mission", order = 0)]
public class Mission : ScriptableObject
{
    #region PublicProperties
    
    public string MissionName => missionName;
    public string BeforeMissionDescription => beforeMissionDescription;
    public string InMissionDescription => inMissionDescription;
    public string PlayerSide => playerSide;
    public string EnemySide => enemySide;
    public Vector2 ScreenPos => screenPos;
    public bool IsDoubleMission => isDoubleMission;
    public Mission SecondMission => secondMission;

    public MissionState MissionState => missionState;

    public List<Mission> PreviousMissions => previousMissions;

    public List<Hero> UnlockingHeroes => unlockingHeroes;

    public List<Mission> NextMissions => nextMissions;

    public int AttachedHeroScore => attachedHeroScore;

    public List<HeroScorePair> GainingScoreHeroes => gainingScoreHeroes;

    #endregion


    #region Labels Descriptions
    
    [Header("Label Descriptions")] 
    [SerializeField] protected string missionName;
    [SerializeField] protected string beforeMissionDescription;
    [SerializeField] protected string inMissionDescription;
    [SerializeField] protected string playerSide;
    [SerializeField] protected string enemySide;

    #endregion

    #region Logic

    [Header("Logic")] 
    
    [SerializeField] protected Vector2 screenPos;
    [SerializeField] protected bool isDoubleMission;
    [ShowIf("isDoubleMission")] 
    [SerializeField] protected Mission secondMission;
    
    [Tooltip("- Active — i.e. available for passing\n" +
             "- Blocked — unavailable for passing, hidden for the player\n" +
             "- Temporarily blocked — the mission is unavailable for passing, but not hidden for the player. Only an Activated mission can be temporarily blocked.\n" +
             "- Completed — already completed mission continues to be displayed on the map. It is impossible to re-pass already completed missions.")]
    [SerializeField] protected MissionState missionState;
    
    [Tooltip("Should be passed to open this mission")] 
    [SerializeField] protected List<Mission> previousMissions;
    
    [Tooltip("List of heroes that will be unlocked after mission pass")] 
    [SerializeField] protected List<Hero> unlockingHeroes;

    [FormerlySerializedAs("blockedMissions")]
    [Tooltip("When mission is not active they Blocked.\n" +
             "When mission is active they Temporarily blocked.\n" +
             "When mission is Completed they Active")]
    [SerializeField] protected List<Mission> nextMissions;

    [Tooltip("Heroes that complete mission will gain score?")] 
    [SerializeField] protected int attachedHeroScore;
    
    [SerializeField] protected List<HeroScorePair> gainingScoreHeroes;
    
    #endregion

    [Button]
    public void RandomizeMissionPos()
    {
        screenPos = new Vector2(Random.Range(0, 101), Random.Range(0, 101));
    }
}

[Serializable]
public class HeroScorePair
{
    public Hero hero;
    public int addableScore;
}
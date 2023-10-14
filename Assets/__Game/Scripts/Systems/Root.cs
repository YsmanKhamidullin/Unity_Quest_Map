using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;

public class Root : MonoBehaviour
{
    [SerializeField] private MissionMapScreen missionMapScreen;
    [SerializeField] private HeroesList heroesList;
    [SerializeField] private MissionMap missionMap;

    private void Awake()
    {
        //Copy can be easily replaced with load

        heroesList = heroesList.GetCopy();
        missionMap = missionMap.GetCopy();

        missionMap.AttachHeroesRefs(heroesList.Heroes);
        missionMap.AttachMapRefs(missionMap.Missions);
        missionMapScreen.Init(missionMap.Missions, heroesList.Heroes);
    }

    [Button]
    public void TestGenerate()
    {
        missionMapScreen.GenerateMap(missionMap.Missions);
    }
}
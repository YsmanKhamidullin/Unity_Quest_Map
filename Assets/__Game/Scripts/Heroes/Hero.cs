using System;
using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(menuName = "CustomSO/Create Hero", fileName = "Hero", order = 0)]
public class Hero : ScriptableObject, IId
{
    public int ID => id;

    public event Action OnUnlock;
    public event Action<int> OnScoreChange;
    public string HeroName => heroName;
    public int HeroScore => heroScore;
    public bool IsUnlocked => isUnlocked;


    [SerializeField] private int id;
    [SerializeField] private string heroName;
    [SerializeField] private int heroScore;
    [SerializeField] private bool isUnlocked;

    public void Unlock()
    {
        isUnlocked = true;
        OnUnlock?.Invoke();
    }

    public void AddScore(int addableScore)
    {
        heroScore += addableScore;
        OnScoreChange?.Invoke(heroScore);
    }

    [Button]
    public void GenerateId()
    {
        id = heroName.GetHashCode();
    }
}

public interface IId
{
    public int ID { get; }

    public void GenerateId();
}
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CustomSO/Create Heroes list", fileName = "Heroes", order = 0)]
public class HeroesList : ScriptableObject
{
    public List<Hero> Heroes => heroes;
    [SerializeField] private List<Hero> heroes;
    
    public HeroesList GetCopy()
    {
        var newHeroes = Instantiate(this);
        newHeroes.heroes = new List<Hero>();
        foreach (var hero in heroes)
        {
            newHeroes.heroes.Add(Instantiate(hero));
        }

        return newHeroes;
    }
}
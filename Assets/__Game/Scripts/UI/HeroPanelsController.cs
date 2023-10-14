using System;
using System.Collections.Generic;
using UnityEngine;

namespace __Game.Scripts.UI
{
    public class HeroPanelsController : MonoBehaviour
    {
        public event Action<Hero> OnSelect;
        [SerializeField] private Color selectedColor = Color.green;
        [SerializeField] private Color notSelectedColor;
        [SerializeField] private HeroPanel heroPanelPrefab;
        [SerializeField] private List<HeroPanel> heroPanels;
        [SerializeField] private Transform heroPanelsParent;


        public void Init(List<Hero> heroesListHeroes)
        {
            ClearCurrentHeroPanels();
            foreach (var hero in heroesListHeroes)
            {
                var newHeroPanel = Instantiate(heroPanelPrefab, heroPanelsParent);
                newHeroPanel.Init(hero);
                newHeroPanel.SetSelectVisualColor(notSelectedColor);
                newHeroPanel.OnSelect += HandleSelectHero;
                heroPanels.Add(newHeroPanel);
            }
        }

        public void UnSelect()
        {
            foreach (var heroPanel in heroPanels)
            {
                heroPanel.SetSelectVisualColor(notSelectedColor);
            }
        }

        private void ClearCurrentHeroPanels()
        {
            foreach (var heroPanel in heroPanels)
            {
                if (Application.isPlaying)
                {
                    Destroy(heroPanel.gameObject);
                }
                else
                {
                    DestroyImmediate(heroPanel.gameObject);
                }
            }
            heroPanels.Clear();
        }

        private void HandleSelectHero(HeroPanel selectedPanel)
        {
            OnSelect?.Invoke(selectedPanel.ContainingHero);
            foreach (var heroPanel in heroPanels)
            {
                heroPanel.SetSelectVisualColor(notSelectedColor);
            }

            selectedPanel.SetSelectVisualColor(selectedColor);
        }
    }
}
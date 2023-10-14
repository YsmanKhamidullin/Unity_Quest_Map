using System;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace __Game.Scripts.UI
{
    public class HeroPanel : MonoBehaviour
    {
        public event Action<HeroPanel> OnSelect;

        public Hero ContainingHero => containingHero;

        [SerializeField] private TextMeshProUGUI nameLabel;
        [SerializeField] private TextMeshProUGUI scoreLabel;
        [SerializeField] private Button selectHeroButton;
        [SerializeField] private Image heroImage;
        [SerializeField, ReadOnly] private Hero containingHero;

        public void Init(Hero initHero)
        {
            containingHero = initHero;
            containingHero.OnScoreChange += UpdateScore;
            selectHeroButton.onClick.AddListener(CallOnSelect);
            if (containingHero.IsUnlocked == false)
            {
                containingHero.OnUnlock += HandleContainingHeroUnlock;
                gameObject.SetActive(false);
                return;
            }

            UpdateVisual();
        }

        private void UpdateVisual()
        {
            nameLabel.text = containingHero.HeroName;
            UpdateScore(containingHero.HeroScore);
        }

        /// <summary>
        /// Notify player about current selected hero
        /// </summary>
        /// <param name="color"></param>
        public void SetSelectVisualColor(Color color)
        {
            heroImage.color = color;
        }

        private void UpdateScore(int value)
        {
            scoreLabel.text = value.ToString();
        }

        private void CallOnSelect()
        {
            OnSelect?.Invoke(this);
        }

        private void HandleContainingHeroUnlock()
        {
            containingHero.OnUnlock -= HandleContainingHeroUnlock;
            gameObject.SetActive(true);
            UpdateVisual();
        }
    }
}
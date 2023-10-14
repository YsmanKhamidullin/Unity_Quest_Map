using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
public abstract class BaseMissionInfoWindow : MonoBehaviour
{
    public event Action<Mission> OnCompleteWindow;
    [SerializeField] private TextMeshProUGUI missionNameLabel;
    [SerializeField] protected TextMeshProUGUI descriptionLabel;
    [SerializeField] private Button completeButton;
    [SerializeField] private Image missionImage;
    
    private Mission _mission;
    
    private void Awake()
    {
        completeButton.onClick.AddListener(CallCompleteWindow);
    }

    private void CallCompleteWindow()
    {
        OnCompleteWindow?.Invoke(_mission);
    }
    
    public virtual void Init(Mission mission)
    {
        gameObject.SetActive(true);
        _mission = mission;
        missionNameLabel.text = mission.MissionName;
    }
    
    public void UnInit()
    {
        OnCompleteWindow = null;
        gameObject.SetActive(false);
    }
}
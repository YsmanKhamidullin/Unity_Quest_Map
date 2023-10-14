using System;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseMissionNode : MonoBehaviour
{
    public event Action<Mission> OnNodeSelect;
    public Mission TargetMission => targetMission;
    [SerializeField, ReadOnly] protected Mission targetMission;
    [SerializeField] protected TextMeshProUGUI missionNumberLabel;
    [SerializeField] protected Button nodeButton;
    protected virtual void Awake()
    {
        nodeButton.onClick.AddListener(CallOnNodeSelect);
    }

    private void Start()
    {
        CalculateCurrentState();
    }

    private void CallOnNodeSelect()
    {
        OnNodeSelect?.Invoke(targetMission);
    }
    public abstract void CalculateCurrentState();

    private void OnDrawGizmos()
    {
        if (Application.isPlaying == false)
        {
            UpdateScreenPos();
        }
    }

    public void UpdateScreenPos()
    {
        if (targetMission == null)
        {
            return;
        }

        var screenResolution = Screen.currentResolution;
        var screenSize = new Vector2(screenResolution.width, screenResolution.height);
        var missionScreenPos = targetMission.ScreenPos;

        var max = 100f;
        var missionXPercent = Mathf.Clamp(missionScreenPos.x, 0f, max) / max;
        var missionYPercent = Mathf.Clamp(missionScreenPos.y, 0f, max) / max;
        var x = Mathf.Lerp(0, screenSize.x, missionXPercent);
        var y = Mathf.Lerp(0, screenSize.y, missionYPercent);
        transform.position = new Vector3(x, y, 0);
    }

    public virtual void SetMission(Mission mission, string missionNumber)
    {
        targetMission = mission;
        missionNumberLabel.text = missionNumber;
    }
}
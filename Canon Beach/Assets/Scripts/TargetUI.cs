using TMPro;
using UnityEngine;

public class TargetUI : MonoBehaviour
{
    public static TargetUI Instance;

    [SerializeField] private TMP_Text remainingText;

    private int targetAmount;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        targetAmount = GameObject.FindGameObjectsWithTag("Target").Length;
        RefreshUI();
    }

    public void HandleTargetHit()
    {
        targetAmount--;
        targetAmount = (int)Mathf.Clamp(targetAmount, 0, Mathf.Infinity);
        RefreshUI();
    }

    private void RefreshUI()
    {
        remainingText.text = targetAmount.ToString();
    }
}

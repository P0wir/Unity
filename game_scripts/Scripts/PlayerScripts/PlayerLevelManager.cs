using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerLevelManager : MonoBehaviour
{
    public PlayerScriptableObject playerData;
    public Slider expBar; 
    public TextMeshProUGUI levelUpText; 

    private float currentExp;
    private int currentLevel;
    private float expToNextLevel;
    private float levelUpExpMultiplier;
    private float currentDamage;
    private float maxHp;

    void Start()
    {
        currentExp = playerData.exp;
        currentLevel = 1;
        expToNextLevel = playerData.expToNextLevel;
        levelUpExpMultiplier = playerData.levelUpExpMultiplier;
        currentDamage = playerData.damage;
        maxHp = playerData.maxHp;

        if (expBar != null)
        {
            expBar.maxValue = expToNextLevel;
            expBar.value = currentExp;
        }

        if (levelUpText != null)
        {
            levelUpText.gameObject.SetActive(false);
        }

        Debug.Log($"Initialized Player: Level {currentLevel}, Damage {currentDamage}, Max HP {maxHp}, EXP to Next Level {expToNextLevel}");
    }

    public void AddExp(float exp)
    {
        currentExp += exp;
        Debug.Log($"Gained {exp} EXP! Current EXP: {currentExp}/{expToNextLevel}");

        if (expBar != null)
        {
            expBar.value = currentExp;
        }

        while (currentExp >= expToNextLevel)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        currentExp -= expToNextLevel; 
        currentLevel++;
        expToNextLevel *= levelUpExpMultiplier;

        currentDamage += playerData.damage * 0.1f;
        maxHp += playerData.maxHp * 0.1f;

        Debug.Log($"Leveled up! Current Level: {currentLevel}, New Damage: {currentDamage}, New Max HP: {maxHp}, Next Level EXP: {expToNextLevel}");

        if (expBar != null)
        {
            expBar.maxValue = expToNextLevel;
            expBar.value = currentExp;
        }

        ShowLevelUpNotification();
    }

    private void ShowLevelUpNotification()
    {
        if (levelUpText != null)
        {
            levelUpText.gameObject.SetActive(true);
            levelUpText.text = $" Level {currentLevel}! Stats increased by 10%";

            Invoke("HideLevelUpNotification", 2f);
        }
    }

    private void HideLevelUpNotification()
    {
        if (levelUpText != null)
        {
            levelUpText.gameObject.SetActive(false);
        }
    }

    public float GetDamage()
    {
        return currentDamage;
    }

    public float GetMaxHp()
    {
        return maxHp; 
    }
}

using UnityEngine;

public class EnemyScaleManager : MonoBehaviour
{
    private static float globalScalingTimer = 30f;
    private static float globalTimeSinceLastScaling = 0f;
    private static float globalDifficultyMultiplier = 1f; 
    private static float difficultyIncrease = 1.1f; 
    public static float GlobalDifficultyMultiplier => globalDifficultyMultiplier;

    void Update()
    {
        globalTimeSinceLastScaling += Time.deltaTime;

        if (globalTimeSinceLastScaling >= globalScalingTimer)
        {
            globalTimeSinceLastScaling = 0f;
            globalDifficultyMultiplier *= difficultyIncrease;
        }
    }
}

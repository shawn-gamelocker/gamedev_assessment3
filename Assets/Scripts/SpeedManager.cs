using UnityEngine;

public class SpeedManager
{
    // Enum for game speed
    public enum GameSpeed
    {
        Slow = 1,
        Fast = 3
    }

    // Private static float member variable
    private static float speedModifier = 1.0f;

    // Private static GameSpeed member variable
    private static GameSpeed currentSpeedState = GameSpeed.Slow;

    // Public static float property (getter only)
    public static float SpeedModifier
    {
        get { return speedModifier; }
    }

    // Public static GameSpeed property with getter and setter
    public static GameSpeed CurrentSpeedState
    {
        get { return currentSpeedState; }
        set
        {
            currentSpeedState = value;
            speedModifier = (float)value;
        }
    }
}


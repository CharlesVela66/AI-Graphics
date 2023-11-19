using UnityEngine;

public class TimeManager : MonoBehaviour
{
    // Variables to set how slow is the game going to be
    [SerializeField]
    float slowDownFactor = 0.4f;

    /// <summary>
    /// Method to slow the game down when called
    /// </summary>
    public void DoSlowMotion()
    {
        Time.timeScale = slowDownFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }
}

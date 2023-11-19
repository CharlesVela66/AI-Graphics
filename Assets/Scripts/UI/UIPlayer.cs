using TMPro;
using UnityEngine;

/// <summary>
/// Script to manipulate the UI for the game
/// </summary>
public class UIPlayer : MonoBehaviour
{
    // Texts for the UI
    [SerializeField]
    TextMeshProUGUI lifeCounter;
    [SerializeField]
    TextMeshProUGUI bulletCounter;
    [SerializeField]
    TextMeshProUGUI enemiesLeft;
    [SerializeField]
    TextMeshProUGUI bossHealthText;
    [SerializeField]
    TextMeshProUGUI matchResult;

    // Variables to store the health of the player and boss
    PlayerHealth playerHealth;
    BossHealth bossHealth;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();

    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // Variables to obtain the quantity of bullets, enemies and the health of the player
        int bulletCount = CountBullets();
        int enemyCount = CountEnemies();
        int playerHealth = GetPlayerHealth();

        // Set the text to the quantities obtained
        lifeCounter.text = "Lives left: " + playerHealth;
        bulletCounter.text = "Bullet counter: " + bulletCount;
        enemiesLeft.text = "Enemies left: " + enemyCount;

        // If there are no more enemies left, destroy the text of the enemies and instantiate the boss
        if (enemyCount == 0 || enemiesLeft == null)
        {
            Destroy(enemiesLeft);

            // If the boss has more than 0 of health, then instantiate it
            if (bossHealthText.text != "Boss health: 0")
            {
                GameObject boss = GameObject.FindGameObjectWithTag("Boss");
                bossHealth = boss.GetComponent<BossHealth>();
                int bHealth = GetBossHealth();
                bossHealthText.text = "Boss health: " + bHealth;
            }
            // Else, show the winning screen
            else
            {
                matchResult.text = "YOU WON";
                Destroy(lifeCounter);
                Destroy(bulletCounter);
                Destroy(bossHealthText);
                return;
            }
        }
        // Check if the player has lost
        if (playerHealth == 0)
        {
            matchResult.text = "YOU LOST";
            Destroy(lifeCounter);
            Destroy(bulletCounter);
            Destroy(bossHealthText);
            return;
        }
    }

    /// <summary>
    /// Method to count the bullets in the scene
    /// </summary>
    /// <returns>How many bullets are in the scene</returns>
    int CountBullets()
    {
        // Look for all the objects with tag "BulletHell" or "Bullet"
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("BulletHell");
        GameObject[] bulletsAdditional = GameObject.FindGameObjectsWithTag("Bullet");

        return bullets.Length + bulletsAdditional.Length;
    }

    /// <summary>
    /// Method to count the enemies in the scene
    /// </summary>
    /// <returns></returns>
    int CountEnemies()
    {
        return GameObject.FindGameObjectsWithTag("Enemy").Length;
    }
    
    /// <summary>
    /// Method to obtain the current health of the player
    /// </summary>
    /// <returns></returns>
    int GetPlayerHealth()
    {
        return playerHealth.GetHealth();
    }

    /// <summary>
    /// Method to obtain the current health of the boss
    /// </summary>
    /// <returns></returns>
    int GetBossHealth()
    {
        return bossHealth.GetHealth();
    }
}

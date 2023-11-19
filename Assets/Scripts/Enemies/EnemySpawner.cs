using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Variables to obtain the enemies prefab
    [SerializeField]
    GameObject enemyPrefab;
    [SerializeField]
    GameObject bossPrefab;

    // Variable to manipulate the boss
    GameObject boss;
    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // Instantiate the boss and disabeling it from the scene
        boss = Instantiate(bossPrefab);
        boss.SetActive(false);
        // Insantiate 10 enemies
        for (int i = 0; i < 10; i++)
        {
            Vector3 position = new Vector3(Random.Range(-350f, 350f), Random.Range(-350f, 350f), Random.Range(-350f, 350f));
            GameObject gameObj = Instantiate(enemyPrefab, position, Quaternion.Euler(0,0,0)) as GameObject;
        }   
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // If there are no more enemies and the health of the boss is greater than 0, then habilitate the boss
        if (boss != null && GameObject.FindGameObjectsWithTag("Enemy").Length <= 0 && boss.GetComponent<BossHealth>().GetHealth() > 0)
        {
           boss.SetActive(true);
        }
    }
}

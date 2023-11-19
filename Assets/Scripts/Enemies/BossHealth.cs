using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [SerializeField]
    int health = 100;

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // If health is zero, then destroy the boss
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    /// <summary>
    /// Method to check if the gameObject has collided with a bullet
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            health--;
        }
    }

    /// <summary>
    /// Method to obtain the current health of the boss
    /// </summary>
    /// <returns></returns>
    public int GetHealth()
    {
        return health;
    }
}

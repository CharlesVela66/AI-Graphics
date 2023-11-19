using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Set the health of the player
    [SerializeField]
    int health = 50;

    private PlayerController controller;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        controller = GetComponent<PlayerController>();
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        if (health <= 0)
        {
            health = 0;
            DisablePlayerComponents();
        }
    }
    /// <summary>
    /// Method to check if the player has collided with an enemy bullet or with the boss
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("BulletHell"))
        {
            health--;
        }
        if (collision.gameObject.CompareTag("Boss"))
        {
            health -= health;
        }
    }

    /// <summary>
    /// Method to obtain the health of the player
    /// </summary>
    /// <returns></returns>
    public int GetHealth()
    {
        return health;
    }

    /// <summary>
    /// Method to deactivate player components
    /// </summary>
    private void DisablePlayerComponents()
    {
        // Deactivate MeshRenderers
        MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer renderer in meshRenderers)
        {
            renderer.enabled = false;
        }

        // Deactivate Rigidbody
        Destroy(GetComponent<Rigidbody>());

        // Deactivate Collider
        Destroy(GetComponent<CapsuleCollider>());

        // Stop receiving inputs from the player
        if (controller != null)
        {
            controller.enabled = false;
        }
    }
}

using UnityEngine;

public class AttackPlayer : MonoBehaviour
{ 
    [SerializeField]
    GameObject shotPrefab;

    [SerializeField]
    Transform muzzle;

    [SerializeField]
    float detectionDistance = 20f;

    [SerializeField]
    float shootingInterval = 3f;

    private AudioSource shootingSound;

    private float timeSinceLastShot;

    private Transform player;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        shootingSound = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // Calculate the distance between the player and the enemy
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // If the player is in range
        if (distanceToPlayer <= detectionDistance)
        {
            // Look directly to the player
            transform.LookAt(player);

            // Check if its time to shoot again
            if (Time.time - timeSinceLastShot >= shootingInterval)
            {
                // Play the fire sound
                shootingSound.Play();
                // Method to shoot the bullet to the player
                Shoot();
                // Update the timer
                timeSinceLastShot = Time.time;
            }
        }
    }

    private void Shoot()
    {
        // Set the offset of the shot
        Vector3 offset = new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), Random.Range(-5, 5));
        // Instantiate the bullet
        GameObject bullet = Instantiate(shotPrefab, muzzle.position + offset, muzzle.rotation);

        // Destroy the bullet after 3 seconds
        Destroy(bullet, 3f);
    }
}

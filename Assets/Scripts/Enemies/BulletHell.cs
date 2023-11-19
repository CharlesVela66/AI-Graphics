using System.Collections;
using UnityEngine;

public class BulletHell : MonoBehaviour
{
    // Set the detection distance
    [SerializeField]
    float detectionDistance = 1000f;

    // Obtain the prefab of the bullet
    [SerializeField]
    GameObject bulletPrefab;

    // Set the amount of bullets
    [SerializeField]
    int bulletCount = 80;

    // Interval between each burst
    [SerializeField]
    float burstInterval = 10f;
    
    // Set the speed of the bullets
    [SerializeField]
    float bulletSpeed = 150f;

    // Get the position where the bullets will be instantiated
    [SerializeField]
    Transform muzzle;

    private int bossHealth;
    private Transform player;
    private float burstTimer;
    private AudioSource burstSound;
    private AudioSource fireSound;

    BossHealth bHealth;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // Find the player in the hierarchy
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        
        // Set the burst timer to 0
        burstTimer = 0;

        // Extract the sound sources
        AudioSource[] allMyAudioSources = GetComponents<AudioSource>();
        burstSound = allMyAudioSources[0];
        fireSound = allMyAudioSources[1];

        // Get the component for the BossHealth
        bHealth = GetComponent<BossHealth>();
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // Get the current boss health
        bossHealth = bHealth.GetHealth();
        // Calculate the distance to the player to check if it's in range
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        // Add the time frame to the timer
        burstTimer += Time.deltaTime;

        // If the player it's in range then look at it
        if (distanceToPlayer <= detectionDistance)
        {
            transform.LookAt(player);
        }
        // If it's time to shoot
        if (burstTimer >=  burstInterval)
        {
            // If the boss has between 70 and 100 health, then shoot the first coreography
            if (bossHealth >= 70 && bossHealth <= 100)
            {
                FirstCoreography();
            }
            // If the boss has between 40 and 70 health, then shoot the second coreography
            else if (bossHealth >= 40 && bossHealth < 70)
            {
                SecondCoreography();
            }
            // If the boss has between 10 and 40 health, then shoot the third coreography
            else if (bossHealth >= 10 && bossHealth < 40)
            {
                ThirdCoreography();
            }
            // Else, shoot all the coreographies
            else
            {
                FirstCoreography();
                SecondCoreography();
                ThirdCoreography();
            }
            // Reset the burst timer
            burstTimer = 0;
        }
    }

    /// <summary>
    /// Method to shoot the first coreography
    /// </summary>
    void FirstCoreography()
    {
        // Instantiate all the bullets
        for (int i = 0; i < bulletCount; i++)
        {
            // Add a small random rotation to the muzzle
            Quaternion randomRotation = Quaternion.Euler(Random.Range(-7f, 7f), Random.Range(-7f, 7f), Random.Range(-7f, 7f));
            Quaternion bulletRotation = muzzle.rotation * randomRotation;

            // Instantiate the muzzle with the random rotation
            GameObject bullet = Instantiate(bulletPrefab, muzzle.position, bulletRotation);
            bullet.GetComponent<ShotBehavior>().velocity = bulletSpeed;
        }
        // Play the fire sound
        burstSound.Play();

        // Start the direct fire shots to the player
        StartCoroutine(FireAtPlayer());
    }

    /// <summary>
    /// Method to shoot the second coreography
    /// </summary>
    void SecondCoreography()
    {
        // Instantiate 5 waves of bullets
        for (int j = 0; j < 5; j++)
        {
            // Random position on the y axis
            float offsetY = Random.Range(-30f, 30f); 
            // Initial position in the coreography
            Vector3 startPosition = muzzle.position + new Vector3(0, offsetY, 0);
            // Variables for the calculation of the angle of each bullet
            float angleStep = 360f / (bulletCount / 3);
            float angle = 0;

            // For each wave, instantia bulletCount / 3
            for (int i = 0; i < bulletCount / 3; i++)
            {
                Quaternion bulletRotation = Quaternion.Euler(0, angle, 0);

                GameObject bullet = Instantiate(bulletPrefab, startPosition, muzzle.rotation * bulletRotation);
                bullet.GetComponent<ShotBehavior>().velocity = bulletSpeed;

                angle += angleStep;
            }
        }
        // Play the fire sound
        burstSound.Play();

        // Start the direct fire shots to the player
        StartCoroutine(FireAtPlayer());
    }
    /// <summary>
    /// Method to shoot the third coreography
    /// </summary>
    void ThirdCoreography()
    {
        // Start the spiral shot
        StartCoroutine(SpiralShot());
    }

    /// <summary>
    /// Fires directly to the player burst of bullets in spiral formation
    /// </summary>
    /// <returns></returns>
    IEnumerator SpiralShot()
    {
        // Angle between each bullet
        float angleStep = 10f;
        float currentAngle = 0f;

        // Play the fire sound
        burstSound.Play();
        // Fire directly at the player
        StartCoroutine(FireAtPlayer());

        for (int i = 0; i < bulletCount * 2; i++)
        {
            // Set the change in angle in the z-axis
            Quaternion bulletRotation = Quaternion.Euler(0, 0, currentAngle);

            // Set the radius of the spiral
            float spiralRadius = 15f;
            // Set the position of the bullet with its angle
            Vector3 bulletPosition = new Vector3(
                muzzle.position.x + spiralRadius * Mathf.Cos(Mathf.Deg2Rad * currentAngle),
                muzzle.position.y + spiralRadius * Mathf.Sin(Mathf.Deg2Rad * currentAngle),
                muzzle.position.z
            );
            // Instantiate the bullet and set the speed of the bullet
            GameObject bullet = Instantiate(bulletPrefab, bulletPosition, muzzle.rotation * bulletRotation);
            bullet.GetComponent<ShotBehavior>().velocity = bulletSpeed * 2;

            // Change the angle for the next bullet
            currentAngle += angleStep;

            // Wait 0.1 seconds to instantiate the next bullet
            yield return new WaitForSeconds(0.1f);
        }
    }

    /// <summary>
    /// Couroutine to fire directly at the player
    /// </summary>
    /// <returns></returns>
    IEnumerator FireAtPlayer()
    {
        // Fire n / 10 bullets
        for (int i = 0; i < bulletCount / 10; i++)
        {
            // Instantiate the bullet and set the velocity
            GameObject bullet = Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);

            bullet.GetComponent<ShotBehavior>().velocity = bulletSpeed + 150f;

            // Play the fire sound
            fireSound.Play();

            // Waith 0.5 seconds to instantiate the next bullet
            yield return new WaitForSeconds(0.5f);
        }
    }
}

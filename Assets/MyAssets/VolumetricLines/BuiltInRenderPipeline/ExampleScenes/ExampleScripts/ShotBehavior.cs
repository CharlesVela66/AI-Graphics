using UnityEngine;

public class ShotBehavior : MonoBehaviour
{
    [SerializeField]
    float lifetime = 5f;
    public float velocity = 1000f;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        Destroy(gameObject, lifetime);
    }
    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * velocity;
    }
    /// <summary>
    /// Method to check if the gameObject has collided with an enemy
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}

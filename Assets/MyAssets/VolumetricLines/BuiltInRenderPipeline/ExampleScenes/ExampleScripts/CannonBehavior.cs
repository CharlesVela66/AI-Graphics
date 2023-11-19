using UnityEngine;
using System.Collections;

public class CannonBehavior : MonoBehaviour {
	
	[SerializeField]
	Transform m_muzzle;

	[SerializeField]
	GameObject m_shotPrefab;

	private float timeSinceLastShot;

	private AudioSource shootingSound;

    void Start()
    {
        shootingSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update () 
	{
		if (Input.GetKey(KeyCode.F) && Time.time - timeSinceLastShot >= 0.25f)
		{
			shootingSound.Play();
			GameObject gameObj = GameObject.Instantiate(m_shotPrefab, m_muzzle.position, m_muzzle.rotation) as GameObject;
			GameObject.Destroy(gameObj, 3f);

			timeSinceLastShot = Time.time;
		}
	}
}

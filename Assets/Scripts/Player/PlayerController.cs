using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float elevationPower;

    [SerializeField]
    float rollPower;

    [SerializeField]
    float yawPower;

    [SerializeField]
    float enginePower;

    [SerializeField]
    TimeManager timeManager;

    private float activeRoll, activeYaw, activeElevation;

    // Update is called once per frame
    void Update()
    {
        // If the space key is pressed, activate the thruster
        if (Input.GetKey(KeyCode.Space))
        {
            // Move forward
            transform.position += transform.forward * enginePower * Time.deltaTime;

            // Rotation section
            // Vertical rotation
            activeElevation = Input.GetAxis("Vertical") * elevationPower * Time.deltaTime;
            // Horizontal rotation
            activeYaw = Input.GetAxis("Horizontal") * rollPower * Time.deltaTime;
            // Z-axis rotation
            activeRoll = Input.GetAxis("Yaw") * yawPower * Time.deltaTime;

            transform.Rotate(activeElevation * elevationPower * Time.deltaTime,
                activeYaw * yawPower * Time.deltaTime,
                -activeRoll * rollPower * Time.deltaTime,
                Space.Self);
        }
        // Else, move normally
        else
        {
            transform.position += transform.forward * enginePower / 5 * Time.deltaTime;
            // Rotation section
            // Vertical rotation
            activeElevation = Input.GetAxis("Vertical") * elevationPower / 2 * Time.deltaTime;
            // Horizontal rotation
            activeYaw = Input.GetAxis("Horizontal") * rollPower / 2 * Time.deltaTime;
            // Z-axis rotation
            activeRoll = Input.GetAxis("Yaw") * yawPower / 2 * Time.deltaTime;

            transform.Rotate(activeElevation * elevationPower * Time.deltaTime,
                activeYaw * yawPower * Time.deltaTime,
                -activeRoll * rollPower * Time.deltaTime,
                Space.Self);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            timeManager.DoSlowMotion();
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}

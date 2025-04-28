using System.Collections;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    [SerializeField] private Transform barrel;
    [SerializeField] private Transform barrelSpawnPoint;
    [SerializeField] private Transform wheelFrontRight;
    [SerializeField] private Transform wheelFrontLeft;
    [SerializeField] private Transform wheelRearRight;
    [SerializeField] private Transform wheelRearLeft;

    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileForce = 30f;
    [SerializeField] private float fireCooldown = 1;

    [SerializeField] private float aimSpeed = 10;
    [SerializeField] private float aimRotationMin = -10;
    [SerializeField] private float aimRotationMax = 10;

    [SerializeField] private float turnSpeed = 30;
    [SerializeField] private float turnRotationMin = -30;
    [SerializeField] private float turnRotationMax = 30;

    private SimpleControls simpleControls;
    private Vector2 aimRotation = Vector2.zero;
    private Vector2 turnRotation;
    private Vector2 wheelRotation = Vector2.zero;
    private float currentFireTimer;
    private bool canFire;

    private void Awake()
    {
        simpleControls = new SimpleControls();
    }

    private void Start()
    {
        turnRotation = transform.localEulerAngles;
        turnRotationMin += turnRotation.y;
        turnRotationMax += turnRotation.y;

        currentFireTimer = fireCooldown;
    }

    private void OnEnable()
    {
        simpleControls.Enable();
        simpleControls.gameplay.fire.performed += _ => { Fire(); };
    }

    void OnDisable()
    {
        simpleControls.Disable();
    }

    private void Update()
    {
        Vector2 move = simpleControls.gameplay.move.ReadValue<Vector2>();
        Aim(move.y);
        Turn(move.x);

        if (canFire) return;

        currentFireTimer -= Time.deltaTime;
        if (currentFireTimer <= 0)
        {
            canFire = true;
        }
    }

    private void Aim(float aimDirection)
    {
        float scaledAimSpeed = aimSpeed * Time.deltaTime;
        float aimAmount = aimDirection * scaledAimSpeed;

        aimRotation.x = Mathf.Clamp(aimRotation.x + aimAmount, aimRotationMin, aimRotationMax);
        barrel.localEulerAngles = aimRotation;

    }

    private void Turn(float turnDirection)
    {
        float scaledRotateSpeed = turnSpeed * Time.deltaTime;
        float turnAmount = turnDirection * scaledRotateSpeed;

        turnRotation.y = Mathf.Clamp(turnRotation.y + turnAmount, turnRotationMin, turnRotationMax);
        transform.localEulerAngles = turnRotation;

        wheelRotation.x = turnRotation.y + turnAmount;

        wheelFrontRight.localEulerAngles = wheelRotation;
        wheelRearRight.localEulerAngles = wheelRotation;
        wheelFrontLeft.localEulerAngles = -wheelRotation;
        wheelRearLeft.localEulerAngles = -wheelRotation;
    }

    private void Fire()
    {
        if (canFire)
        {
            var cannonBall = Instantiate(projectilePrefab, barrelSpawnPoint.position, barrelSpawnPoint.rotation);
            Rigidbody rigidBody = cannonBall.GetComponent<Rigidbody>();
            rigidBody.AddTorque(Random.insideUnitSphere.normalized * projectileForce, ForceMode.Impulse);
            rigidBody.AddForce(cannonBall.transform.forward * projectileForce, ForceMode.Impulse);

            currentFireTimer = fireCooldown;
            canFire = false;
        }
    }
}

using UnityEngine;

public class CannonController : MonoBehaviour
{
    [SerializeField] private Transform barrel;
    [SerializeField] private Transform wheelFrontRight;
    [SerializeField] private Transform wheelFrontLeft;
    [SerializeField] private Transform wheelRearRight;
    [SerializeField] private Transform wheelRearLeft;

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

    private void Awake()
    {
        simpleControls = new SimpleControls();
    }

    private void Start()
    {
        turnRotation = transform.localEulerAngles;
        turnRotationMin += turnRotation.y;
        turnRotationMax += turnRotation.y;
    }

    private void OnEnable()
    {
        simpleControls.Enable();
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
}

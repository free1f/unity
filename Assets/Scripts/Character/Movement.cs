using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using Cinemachine;
using UnityEngine;

public class Movement : MonoBehaviour, IInteract
{
    private CharacterController characterController;
    [SerializeField]
    private CinemachineVirtualCamera virtualCamera;
    public Camera playerCamera;
    public GameObject CameraTarget;
    private Animator animator;
    public float Speed;
    public float RotationSpeed;
    private Vector3 _direction = Vector3.zero;
    private Quaternion _rotation = Quaternion.identity;
    private bool isAnimationPaused = false;
    private float CinemachinePitch;
    private float CinemachineYaw;
    public bool LockCameraPosition = false;
    public float CameraTopLimit = 90f;
    public float CameraBottomLimit = -90f;
    public float CinemachineOverride;
    public float CinemachineVerticalSpeed = 20f;
    public float CinemachineHorizontalSpeed = 20f;
    private float _rotationVelocity;
    public Transform handPoint;

    // Start is called before the first frame update
    void Start()
    {
        characterController = gameObject.GetComponent<CharacterController>();
        animator = gameObject.GetComponent<Animator>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        CinemachineYaw = CameraTarget.transform.rotation.eulerAngles.y;
        playerCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        CalculateInput();
        CalculateMovement();
        // CalculateRotation();
        if (!isAnimationPaused) AnimateMotion();
        PressOne();
    }

    void LateUpdate()
    {
        CalculateCameraRotation();
    }

    private void CalculateInput()
    {
        // No optimal way: var direction = new Vector3(Input.GetAxis("Horizontal"), Vector3.zero.y, Input.GetAxis("Vertical"));
        _direction.Set(Input.GetAxis("Horizontal"), Vector3.zero.y, Input.GetAxis("Vertical"));
    }

    private void CalculateMovement()
    {
        float movementSpeed = Speed;
        if (_direction == Vector3.zero) 
        {
            movementSpeed = 0f;
        }
        if (_direction.magnitude >= 0.1f) 
        {
            var targetRotation = CalculateRotation();

            _direction = Quaternion.Euler(0f, targetRotation, 0f) * Vector3.forward;
            characterController.Move(_direction.normalized*(movementSpeed*Time.deltaTime));

        }
    }

    private void CalculateCameraRotation()
    {
        float mouseHorizontal = Input.GetAxis("Mouse X");
        float mouseVertical = Input.GetAxis("Mouse Y");
        float mouseMagnitude = Mathf.Sqrt(mouseHorizontal*mouseHorizontal + mouseVertical*mouseVertical);
    
        if(mouseMagnitude >= 0.01f && !LockCameraPosition)
        {
            float deltaTime = Time.deltaTime;
            CinemachinePitch += mouseVertical * CinemachineVerticalSpeed * deltaTime;
            CinemachineYaw += mouseHorizontal * CinemachineHorizontalSpeed * deltaTime;
        }

        CinemachinePitch = ClampCamera(CinemachinePitch, CameraBottomLimit, CameraTopLimit);
        CinemachineYaw = ClampCamera(CinemachineYaw, float.MinValue, float.MaxValue);
        CameraTarget.transform.rotation = Quaternion.Euler(CinemachinePitch + CinemachineOverride, CinemachineYaw, 0f);
    }

    private float ClampCamera(float angle, float min, float max)
    {
        if (angle < -360f) angle += 360f;
        if (angle > 360f) angle -= 360f;
        return Mathf.Clamp(angle, min, max);
    }

    private float CalculateRotation()
    {
        float targetRotation = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg + playerCamera.transform.eulerAngles.y;
        transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref _rotationVelocity, RotationSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0f, targetRotation, 0f);
        return targetRotation;
    }

    private void AnimateMotion() {
        if(_direction.z != 0 || _direction.x != 0) {
            animator.Play("Walk");
        }
        else 
        {
            animator.Play("Idle");
        }
    }

    private IEnumerator WaitForAnimation(string animationName)
    {
        isAnimationPaused =  true;
        // Play the animation
        animator.Play(animationName);

        // Wait for the current animation's duration
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        // Animation completed, resume here
        isAnimationPaused = false;
    }

    private void PressOne() {
        if(Input.GetKeyDown(KeyCode.Alpha1)) {
            StartCoroutine(WaitForAnimation("Faint"));
        }
        if(Input.GetKeyDown(KeyCode.Alpha2)) {
            StartCoroutine(WaitForAnimation("Head hit"));
        }
        if(Input.GetKeyDown(KeyCode.Alpha3)) {
            StartCoroutine(WaitForAnimation("Picking up"));
        }
        if(Input.GetKeyDown(KeyCode.Space)) {
            StartCoroutine(WaitForAnimation("Jump"));
        }
    }

    public void Interact(GameObject target)
    {
        StartCoroutine(WaitForAnimation("Picking up"));
        target.transform.SetParent(handPoint);
        target.transform.localPosition = Vector3.zero;
        target.transform.localRotation = Quaternion.identity;
        target.GetComponent<Rigidbody>().isKinematic = true;
    }
}

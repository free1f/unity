using UnityEngine;

public class CharacterJump : MonoBehaviour
{
    private CharacterController _characterController;

    public float JumpHeight = 2.0f;
    public float Gravity = -9.81f;
    public float MaxJumpPressTime = 0.5f;

    private bool _isJumping;
    private float _jumpPressTimeCounter;
    private Vector3 _jumpVelocity;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _jumpPressTimeCounter = MaxJumpPressTime;
    }

    public void CheckGround() 
    {
        if (_characterController.isGrounded && _jumpVelocity.y < 0)
        {
            _jumpVelocity.y = 0f;
        }
    }

    public void CalculateJump()
    {
        _jumpVelocity.y += Gravity * Time.deltaTime;
        _characterController.Move(_jumpVelocity * Time.deltaTime);
    }

    public void CalculateInput()
    {
        if (_characterController.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            _isJumping = true;
            _jumpPressTimeCounter = MaxJumpPressTime;
            _jumpVelocity.y += Mathf.Sqrt(JumpHeight * -3.0f * Gravity);
        }

        if (Input.GetKey(KeyCode.Space) && _isJumping)
        {
            if (_jumpPressTimeCounter > 0)
            {
                _jumpVelocity.y += Mathf.Sqrt(JumpHeight * -3.0f * Gravity) * (Time.deltaTime / MaxJumpPressTime);
                _jumpPressTimeCounter -= Time.deltaTime;
            }
            else
            {
                _isJumping = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            _isJumping = false;
        }
    }
}
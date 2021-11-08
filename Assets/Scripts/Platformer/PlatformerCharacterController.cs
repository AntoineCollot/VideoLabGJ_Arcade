using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum Direction { Top, Right, Bottom, Left, None }

public class PlatformerCharacterController : MonoBehaviour
{
    [Header("Movements")]
    [SerializeField] protected float moveSpeed = 1;
    [SerializeField, Tooltip("How much smooth to apply on the ground")] protected float groundMovementSmooth = 0.05f;
    [SerializeField, Tooltip("How much smooth to apply in the air (increase for more inertia)")] protected float airMovementSmooth = 0.3f;
    protected new Rigidbody2D rigidbody;
    protected Vector2 refVelocity;


    [Header("Jump")]
    [SerializeField, Tooltip("How high the jump can go")] protected float jumpHeight = 3;
    [SerializeField, Tooltip("How much time it takes to reach the apex of a normal jump")] protected float timeToJumpApex = 0.4f;
    [SerializeField, Tooltip("How much the gravity should increase when releasing the jump button early")] protected float shortHopGravityMultiplier = 1.5f;
    [SerializeField, Tooltip("How much the gravity should increase when the character is going down")] protected float fallingGravityMultiplier = 2f;
    protected float defaultGravity;
    protected float currentGravity;

    //Grounded
    bool isGrounded;
    public Transform groundedRef = null;
    public LayerMask groundLayers;

    //Facing
    [HideInInspector] public Direction facingDirection;

    //Anims
    protected Animator anim;

    [HideInInspector] public float lastFallingSpeed;
    [HideInInspector] public Vector3 lastPosition;

    public Vector2 Position
    {
        get
        {
            return transform.position;
        }
    }

    public Vector2 Forward
    {
        get
        {
            switch (facingDirection)
            {
                case Direction.Left:
                    return Vector2.left;
                case Direction.Right:
                default:
                    return Vector2.right;
            }
        }
    }

    // Start is called before the first frame update
    protected void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    protected void Start()
    {

    }

    // Update is called once per frame
    protected void Update()
    {
        if (!PlatformerManager.Instance.gameIsPlaying)
            return;

        isGrounded = false;
        if(Physics2D.Raycast(groundedRef.position, Vector2.down, groundedRef.localPosition.y*2, groundLayers))
        {
            isGrounded = true;
        }

        if (Input.GetButtonDown("Action"))
            Jump();
        UpdateGravityValue();

        UpdateFacing();
        Move();
    }

    private void LateUpdate()
    {
        lastFallingSpeed = rigidbody.velocity.y;
        lastPosition = transform.position;
    }

    /// <summary>
    /// Update the direction the character should be looking in
    /// </summary>
    public void UpdateFacingDirection()
    {
        if (Input.GetAxis("Horizontal") > 0.1f)
        {
            facingDirection = Direction.Right;
        }
        else if (Input.GetAxis("Horizontal") < -0.1f)
        {
            facingDirection = Direction.Left;
        }
    }

    /// <summary>
    /// Called in update
    /// </summary>
    protected virtual void UpdateFacing()
    {
        //Get the current facing direction base don the directional inputs if allowed and grounded (do not turn in air with simple directional inputs)
        if (isGrounded)
        {
            UpdateFacingDirection();
        }


        //Apply the direction
        switch (facingDirection)
        {
            case Direction.Right:
                transform.localScale = new Vector3(1, 1, 1);
                break;
            case Direction.Left:
                transform.localScale = new Vector3(-1, 1, 1);
                break;
        }
    }

    public virtual void Move()
    {
        Vector2 targetVelocity = rigidbody.velocity;
        targetVelocity.x = moveSpeed * Input.GetAxis("Horizontal");

        switch (isGrounded)
        {
            case true:
                rigidbody.velocity = Vector2.SmoothDamp(rigidbody.velocity, targetVelocity, ref refVelocity, groundMovementSmooth);
                break;
            case false:
                rigidbody.velocity = Vector2.SmoothDamp(rigidbody.velocity, targetVelocity, ref refVelocity, airMovementSmooth);
                break;
        }

        anim.SetFloat("InputSpeed", Mathf.Abs(Input.GetAxis("Horizontal")));
    }

    protected void UpdateGravityValue()
    {
        //The default gravity is based on the jump height
        //jumpHeight = (gravity * TimeToJumpApex²) / 2
        defaultGravity = (2 * jumpHeight) / (timeToJumpApex * timeToJumpApex);
        currentGravity = defaultGravity;

        //If we are going up
        if (rigidbody.velocity.y > 0.01f)
        {
            //If the jump input is not pressed while going up (we released early into the jump)
            if (!Input.GetButton("Action"))
            {
                currentGravity *= shortHopGravityMultiplier;
            }
        }
        //If we are falling (going down)
        else if (rigidbody.velocity.y < -0.01f && !isGrounded)
        {
            currentGravity *= fallingGravityMultiplier;
        }

        //play with the rigidbody gravity multiplier to result in the right gravity
        rigidbody.gravityScale = currentGravity / -Physics2D.gravity.y;
    }

    public void Jump()
    {
        //Check if a jump is available.
        if (!isGrounded)
            return;

        //Count the jump
        //Do it before doing the actualy jump cause after that we don't know if we were grounded or not anymore
        if (!isGrounded)
        {
            UpdateFacingDirection();
        }

        ForceJump();
    }

    public void ForceJump()
    {
        anim.SetTrigger("Jump");

        //finalVelocity = initialVelocity + acceleration * time
        //Solving for the apex, so at timeToJumpApex where final velocity = 0
        float jumpVelocity = defaultGravity * timeToJumpApex;

        //Apply the jump velocity
        Vector2 velocity = rigidbody.velocity;
        velocity.y = jumpVelocity;
        rigidbody.velocity = velocity;

        AudioManager.PlaySound(SFX.PlatformerJump);
    }

    public void JumpAtHeight(float height, bool playAnim)
    {
        if (playAnim)
            anim.SetTrigger("Jump");

        //jumpHeight = (gravity * TimeToJumpApex²) / 2
        //Solve for time to apex which is the thing we want to adjust
        float targeTimeToJumpApex = Mathf.Sqrt((2 * height) / (defaultGravity * shortHopGravityMultiplier));

        //finalVelocity = initialVelocity + acceleration * time
        //Solving for the apex, so at timeToJumpApex where final velocity = 0
        float jumpVelocity = (defaultGravity * shortHopGravityMultiplier) * targeTimeToJumpApex;

        //Apply the jump velocity
        Vector2 velocity = rigidbody.velocity;
        velocity.y = jumpVelocity;
        rigidbody.velocity = velocity;
    }
}

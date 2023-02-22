using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    public StateMachine stateMachine;
    [SerializeField]
    private float maxSpeed;
    private float originalSpeed;
    [SerializeField]
    private float maxFallingSpeed;
    [SerializeField]
    private float jumpPower;
    [SerializeField]
    private int jumpMaxCount;
    [SerializeField]
    private float jumpMaxTime;
    [SerializeField]
    private float coyoteTime;
    [SerializeField]
    private float wallJumpTime;
    [SerializeField]
    private float wallJumpForce;
    [SerializeField]
    private float wallJumpInputBuffer;
    [SerializeField]
    private float climbingSpeed;
    public float JumpTime { get; set; } = 0f;
    public int JumpCount { get; set; }
    public bool IsJumping { get; set; }
    public bool IsClimbingLeft { get; set; }
    public bool IsClimbingRight { get; set; }
    public bool IsCoyoteTimeEnable { get; set; }
    public bool IsWallJumpEnable { get; set; } = true;
    public bool IsWallJumpInputEnable { get; set; } = true;
    public bool HasSteppedEntity { get; set; } = false;
    public bool IsHeadingRight { get; private set; } = true;

    public SpriteRenderer spriteRenderer { get; set; }
    public Rigidbody2D rigid2d { get; set; }
    public Collider2D coll { get; set; }
    public Animator anim { get; set; }
    public Player player { get; set; }

    public float MaxSpeed { get { return maxSpeed; } set { maxSpeed = value; } }
    public float MaxFallingSpeed => maxFallingSpeed;
    public float ClimbingSpeed => climbingSpeed;
    public float JumpPower => jumpPower;
    public int JumpMaxCount => jumpMaxCount;
    public float JumpMaxTime => jumpMaxTime;
    public float WallJumpForce => wallJumpForce;

    private List<IInteractive> interactionList = new List<IInteractive>();
    public List<IInteractive> InteractionList => interactionList;
    private Dictionary<Tile, int> slowList = new();
    public Dictionary<Tile, int> SlowList => slowList;

    // Initialize states
    private void Awake()
    {
        stateMachine = new StateMachine(new PlayerIdle(this));

        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid2d = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        player = GetComponent<Player>();

        anim.speed = 0.3f;

        originalSpeed = maxSpeed;
    }

    private void Update()
    {
        stateMachine.DoOperateUpdate();

        // Limit Player's Falling Speed
        if (rigid2d.velocity.y < (-1) * maxFallingSpeed)
        {
            rigid2d.velocity = new Vector2(rigid2d.velocity.x, (-1) * maxFallingSpeed);
        }

        // Interaction Test
        if (Input.GetKeyDown(KeyCode.G))
        {
            var interactions = interactionList.Where(e => e.IsAvailable);

            foreach (var interaction in interactions)
            {
                interaction.Interact(player);
            }
        }
    }

    private void FixedUpdate()
    {
        stateMachine.DoOperateFixedUpdate();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var interaction = other.GetComponent<IInteractive>();
        Debug.Log(other);
        Debug.Log(interaction);

        if (interaction != null)
        {
            interactionList.Add(interaction);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var interaction = other.GetComponent<IInteractive>();

        if (interaction != null)
        {
            interactionList.Remove(interaction);
        }
    }

    public void HorizontalMove(float h)
    {
        // Flip Sprite
        if (Input.GetButton("Horizontal"))
        {
            // spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
            IsHeadingRight = Input.GetAxisRaw("Horizontal") == 1;

            if (IsHeadingRight)
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
            else
                transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }

        // Check Side
        var wall = IsThereWall();

        if (wall != 1 && h > 0)
            rigid2d.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (wall != -1 && h < 0)
            rigid2d.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (rigid2d.velocity.x > MaxSpeed)
            rigid2d.velocity = new Vector2(MaxSpeed, rigid2d.velocity.y);

        if (rigid2d.velocity.x < MaxSpeed * (-1))
            rigid2d.velocity = new Vector2(MaxSpeed * (-1), rigid2d.velocity.y);
    }

    public int IsThereWall()
    {
        RaycastHit2D raycastHit2DLeft = Physics2D.Raycast(transform.position, Vector3.left, 0.35f, LayerMask.GetMask("Ground"));
        RaycastHit2D raycastHit2DRight = Physics2D.Raycast(transform.position, Vector3.right, 0.35f, LayerMask.GetMask("Ground"));

        if (raycastHit2DLeft.collider != null)
        {
            return -1;
        }
        else if (raycastHit2DRight.collider != null)
        {
            return 1;
        }

        return 0;
    }


    public bool IsThereLand()
    {
        Vector2 Pos1 = new Vector2(rigid2d.position.x + 0.23f, rigid2d.position.y);
        Vector2 Pos2 = new Vector2(rigid2d.position.x - 0.23f, rigid2d.position.y);

        RaycastHit2D raycastHit2DDown1 = Physics2D.Raycast(Pos1, Vector3.down, 0.6f, LayerMask.GetMask("Ground"));
        RaycastHit2D raycastHit2DDown2 = Physics2D.Raycast(Pos2, Vector3.down, 0.6f, LayerMask.GetMask("Ground"));
        RaycastHit2D raycastHit2DDown3 = Physics2D.Raycast(rigid2d.position, Vector3.down, 0.6f, LayerMask.GetMask("Ground"));

        if (raycastHit2DDown1.collider != null || raycastHit2DDown2.collider != null || raycastHit2DDown3.collider != null)
        {
            return true;
        }
        else
        {
            if (IsCoyoteTimeEnable && IsWallJumpEnable)
            {
                StartCoroutine(DelayCoyoteTime(coyoteTime));
                return true;
            }
            return false;
        }
    }
    // 검증 필요
    public bool IsThereWall(int direction)
    {
        // direction; left : -1, right : 1
        Vector2 Pos = new Vector2(rigid2d.position.x + direction * 1f, rigid2d.position.y);

        RaycastHit2D raycastHit2Dleft = Physics2D.Raycast(rigid2d.position, rigid2d.velocity, 0.6f, LayerMask.GetMask("LeftWall"));
        RaycastHit2D raycastHit2Dright = Physics2D.Raycast(rigid2d.position, rigid2d.velocity, 0.6f, LayerMask.GetMask("RightWall"));

        if (direction == 1 && raycastHit2Dright.collider != null)
        {
            return true;
        }
        else if (direction == -1 && raycastHit2Dleft.collider != null)
        {
            return true;
        }
        return false;
    }

    public void Step()
    {
        Vector2 Pos1 = new Vector2(rigid2d.position.x + 0.23f, rigid2d.position.y);
        Vector2 Pos2 = new Vector2(rigid2d.position.x - 0.23f, rigid2d.position.y);

        RaycastHit2D raycastHit2DDown1 = Physics2D.Raycast(Pos1, Vector3.down, 0.6f, LayerMask.GetMask("Stepable"));
        RaycastHit2D raycastHit2DDown2 = Physics2D.Raycast(Pos2, Vector3.down, 0.6f, LayerMask.GetMask("Stepable"));

        var stepLeft = raycastHit2DDown1.collider?.GetComponent<IStep>();
        var stepRight = raycastHit2DDown2.collider?.GetComponent<IStep>();

        stepLeft?.OnStep(player, true);

        if (stepLeft != stepRight)
            stepRight?.OnStep(player, true);

        if (stepLeft != null)
        {
            HasSteppedEntity = stepLeft is Enemy;
            Debug.Log(HasSteppedEntity);
        }

        if (stepRight != null)
        {
            HasSteppedEntity |= stepRight is Enemy;
            Debug.Log(HasSteppedEntity);
        }

        if (HasSteppedEntity)
        {
            stateMachine.SetState(new PlayerJump(this));
        }
    }

    private IEnumerator DelayCoyoteTime(float time)
    {
        yield return new WaitForSeconds(time);
        IsCoyoteTimeEnable = false;
        yield return null;
    }

    public IEnumerator DelayWallJumpInput()
    {
        yield return new WaitForSeconds(wallJumpInputBuffer);
        stateMachine.SetState(new PlayerJump(this));
        IsWallJumpInputEnable = true;
    }

    public void GetSlowDebuff()
    {
        var maxSlowValue = 0f;

        foreach (var (k, v) in slowList)
        {
            maxSlowValue = Mathf.Min(maxSlowValue, v);
        }

        maxSpeed = originalSpeed + maxSlowValue;
    }
}
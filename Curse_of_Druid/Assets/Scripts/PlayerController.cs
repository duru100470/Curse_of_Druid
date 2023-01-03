using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    public StateMachine stateMachine;
    [SerializeField]
    private float maxSpeed;
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
    public float JumpTime { get; set; } = 0f;
    public int JumpCount { get; set; }
    public bool IsJumping { get; set; }
    public bool IsCoyoteTimeEnable { get; set; }
    public bool IsHeadingRight { get; private set; } = true;

    public SpriteRenderer spriteRenderer { get; set; }
    public Rigidbody2D rigid2d { get; set; }
    public Collider2D coll { get; set; }
    public Animator anim { get; set; }
    public Player player { get; set; }

    public float MaxSpeed => maxSpeed;
    public float MaxFallingSpeed => maxFallingSpeed;
    public float JumpPower => jumpPower;
    public int JumpMaxCount => jumpMaxCount;
    public float JumpMaxTime => jumpMaxTime;

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
    }

    private void Update()
    {
        stateMachine.DoOperateUpdate();

        // Limit Player's Falling Speed
        if (rigid2d.velocity.y < (-1) * maxFallingSpeed)
        {
            rigid2d.velocity = new Vector2(rigid2d.velocity.x, (-1) * maxFallingSpeed);
        }
    }

    private void FixedUpdate()
    {
        stateMachine.DoOperateFixedUpdate();
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
        RaycastHit2D raycastHit2DRight = Physics2D.Raycast(rigid2d.position, Vector3.right, 0.35f, LayerMask.GetMask("Ground"));
        RaycastHit2D raycastHit2DLeft = Physics2D.Raycast(rigid2d.position, Vector3.left, 0.35f, LayerMask.GetMask("Ground"));

        if (raycastHit2DRight.collider == null && h > 0)
            rigid2d.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (raycastHit2DLeft.collider == null && h < 0)
            rigid2d.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (rigid2d.velocity.x > MaxSpeed)
            rigid2d.velocity = new Vector2(MaxSpeed, rigid2d.velocity.y);

        if (rigid2d.velocity.x < MaxSpeed * (-1))
            rigid2d.velocity = new Vector2(MaxSpeed * (-1), rigid2d.velocity.y);
    }

    public bool IsThereLand()
    {
        Vector2 Pos1 = new Vector2(rigid2d.position.x + 0.25f, rigid2d.position.y);
        Vector2 Pos2 = new Vector2(rigid2d.position.x - 0.25f, rigid2d.position.y);

        RaycastHit2D raycastHit2DDown1 = Physics2D.Raycast(Pos1, Vector3.down, 0.6f, LayerMask.GetMask("Ground"));
        RaycastHit2D raycastHit2DDown2 = Physics2D.Raycast(Pos2, Vector3.down, 0.6f, LayerMask.GetMask("Ground"));
        RaycastHit2D raycastHit2DDown3 = Physics2D.Raycast(rigid2d.position, Vector3.down, 0.6f, LayerMask.GetMask("Ground"));

        if (raycastHit2DDown1.collider != null || raycastHit2DDown2.collider != null || raycastHit2DDown3.collider != null)
        {
            return true;
        }
        else
        {
            if (IsCoyoteTimeEnable)
            {
                StartCoroutine(DelayCoyoteTime(coyoteTime));
                return true;
            }
            return false;
        }
    }

    private IEnumerator DelayCoyoteTime(float time)
    {
        yield return new WaitForSeconds(time);
        IsCoyoteTimeEnable = false;
        yield return null;
    }
}
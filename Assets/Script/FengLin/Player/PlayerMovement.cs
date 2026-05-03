using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("移动设置")]
    public float moveSpeed = 5f;

    [Header("跳跃设置")]
    public float jumpForce = 12f;
    public float jumpCutMultiplier = 0.5f;
    public float fallGravityMultiplier = 2.5f;

    [Header("地面检测")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    [Header("穿透平台设置")]
    public float fallIgnoreTime = 0.2f;

    private Rigidbody2D rb;
    private Collider2D playerCol;

    private bool isGrounded;
    private float horizontalInput;
    private float originalGravityScale;

    public static PlayerMovement instance;

    void Start()
    {
        if (instance == null)
            instance = this;

        rb = GetComponent<Rigidbody2D>();
        playerCol = GetComponent<Collider2D>();
        originalGravityScale = rb.gravityScale;
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        horizontalInput = Input.GetAxisRaw("Horizontal");

        // 跳跃
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        //松开空格减弱上升
        //if (Input.GetKeyUp(KeyCode.Space) && rb.velocity.y > 0)
        //{
        //    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * jumpCutMultiplier);
        //}

        // 下落加重重力
        if (rb.velocity.y < 0)
        {
            rb.gravityScale = originalGravityScale * fallGravityMultiplier;
        }
        else
        {
            rb.gravityScale = originalGravityScale;
        }

        // 按S：只在单向平台下落，实心地面不动
        if (Input.GetKeyDown(KeyCode.S) && isGrounded)
        {
            CheckAndFallThrough();
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
    }

    void CheckAndFallThrough()
    {
        // 检测脚下是不是 单向平台(PlatformEffector2D)
        Collider2D hit = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        if (hit == null) return;

        // 如果脚下物体有 PlatformEffector2D，才允许下落
        PlatformEffector2D platform = hit.GetComponent<PlatformEffector2D>();
        if (platform != null)
        {
            StartCoroutine(FallPlatformCoroutine());
        }
    }

    IEnumerator FallPlatformCoroutine()
    {
        // 临时忽略与平台碰撞
        playerCol.enabled = false;
        yield return new WaitForSeconds(fallIgnoreTime);
        playerCol.enabled = true;
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
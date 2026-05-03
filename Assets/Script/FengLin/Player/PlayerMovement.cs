using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("移动设置")]
    public float moveSpeed = 5f;       // 移动速度

    [Header("跳跃设置")]
    public float jumpForce = 12f;      // 起跳初速度（调大一点，跳得更高）
    public float jumpCutMultiplier = 0.5f; // 松开空格时的下落倍率
    public float fallGravityMultiplier = 2.5f; // 下落时额外重力（数值越大掉得越快）

    [Header("地面检测")]
    public Transform groundCheck;     
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;     // 选择地面层级

    [Header("角色组件")]
    private Rigidbody2D rb;
    //private Animator anim;
    //private SpriteRenderer spriteRenderer;

    private bool isGrounded;          // 是否在地面
    private float horizontalInput;    // 水平输入
    private float originalGravityScale;

    public static PlayerMovement instance;

    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        // 获取组件
        rb = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
        //spriteRenderer = GetComponent<SpriteRenderer>();

        // 保存原始重力缩放
        originalGravityScale = rb.gravityScale;
    }

    void Update()
    {
        // 检测是否在地面
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // 获取水平输入（A/D 或 ←→）
        horizontalInput = Input.GetAxisRaw("Horizontal");

        // 跳跃（空格）
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Debug.Log("跳跃");
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // 跳跃松开
        if (Input.GetKeyUp(KeyCode.Space) && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * jumpCutMultiplier);
        }

        // 下落时，额外增加重力，让下降速度更快
        if (rb.velocity.y < 0)
        {
            rb.gravityScale = originalGravityScale * fallGravityMultiplier;
        }
        else
        {
            // 上升或地面时恢复正常重力
            rb.gravityScale = originalGravityScale;
        }

        // 角色翻转
        if (horizontalInput != 0)
        {
            //spriteRenderer.flipX = horizontalInput < 0;
        }

        // 动画
        //anim.SetBool("IsGrounded", isGrounded);
        //anim.SetFloat("Speed", Mathf.Abs(horizontalInput));
    }

    void FixedUpdate()
    {
        // 移动
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
    }

    // 绘制地面检测范围（Scene 视图可见）
    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}

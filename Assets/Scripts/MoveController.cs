using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    private float xInput;

    [Header("Collision Check")]
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayerMask;


    private bool isGround;
    private bool facingRight = true;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
         AnimationControllers();
         CollisionCheck();
         Movement();
         Jump();
         FlipController();
    }

    //hàm xử lý chuyển động animation 
    private void AnimationControllers()
    {
        anim.SetFloat("xVelocity", rb.velocity.x);
        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("isGrounded", isGround);
    }


    //hàm xử lý hành động nhảy
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //nếu như nhân vật chạm đất thì mới cho phép nhảy 
            if (isGround)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }
    }

    //hàm xử lý hành động di chuyển phải trái
    private void Movement()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(xInput * 5, rb.velocity.y);
    }

    //hàm thay đổi góc nhìn phải/trái nhân vật 
    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    //hàm thay đổi góc nhìn phải/trái của nhân vật theo con trỏ chuột
    private void FlipController()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if(mousePosition.x < transform.position.x && facingRight)
        {
            Flip();
        }
        else if(mousePosition.x > transform.position.x && !facingRight)
        {
            Flip();
        }
    }

    //hàm vẽ một hình cầu dây (wire sphere) trong Scene view của Unity để xác định chân nhân vật có chạm đất không
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

    //hàm kiểm tra nếu chân nhân vật chạm đất
    private void CollisionCheck()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayerMask);
    }

}

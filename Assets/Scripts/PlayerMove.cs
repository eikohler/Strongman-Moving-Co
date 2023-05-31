using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private float horizontal;
    private float speed = 6f;
    private float jumpingPower = 25f;
    private bool isFacingRight = true;
    private float gravityBase;

    public float fallingThreshold;

    private float coyoteTime = 0.1f;
    private float coyoteTimeCounter;

    private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private BoxCollider2D boxCollider2D;

    private void Start() {
        gravityBase = rb.gravityScale;
    }
 
    // Update is called once per frame
    private void Update(){
        horizontal = Input.GetAxisRaw("Horizontal");

        if(IsGrounded()){
            coyoteTimeCounter = coyoteTime;
        }else{
            coyoteTimeCounter -= Time.deltaTime;
        }

        if(Input.GetKeyDown(KeyCode.Space)){
            jumpBufferCounter = jumpBufferTime;
        }else{
            jumpBufferCounter -= Time.deltaTime;
        }
        
        if(coyoteTimeCounter > 0f && jumpBufferCounter > 0f){
            rb.velocity = Vector2.up * jumpingPower;
            jumpBufferCounter = 0f;
        }
        if(Input.GetKeyUp(KeyCode.Space) && rb.velocity.y > 0f){
            rb.velocity = Vector2.up * 0.5f;
            coyoteTimeCounter = 0f;
        }

        if(!IsGrounded() && rb.velocity.y < 1f && rb.velocity.y > fallingThreshold){
            rb.gravityScale = gravityBase * 0.5f;                        
        }else if(rb.velocity.y < fallingThreshold){
            rb.gravityScale = gravityBase * 1.5f;
        }else{
            rb.gravityScale = gravityBase;
        }

        Flip();
    }

    private void FixedUpdate() {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded(){
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, 0.1f, groundLayer);
        return raycastHit2d.collider != null;
    }

    private void Flip(){
        if(isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f){
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}

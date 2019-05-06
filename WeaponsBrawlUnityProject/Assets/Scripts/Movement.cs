using UnityEngine;

public class Movement : MonoBehaviour {
    public float jumpForce = 10;
    public float speed = 400f;
    public GameObject GroundCheck;

    private float horizontalMove = 0f;
    private Vector3 m_Velocity = Vector3.zero;
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;
    public bool isGrounded = false;

    private void Start(){
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update () {
        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;
    }

    private void FixedUpdate(){
        Move(horizontalMove * Time.fixedDeltaTime);
        if (Input.GetButtonDown("Jump"))
            Jump();
        //isGrounded = Physics2D.Linecast(transform.position, GroundCheck.transform.position, playerMask);
    }

    public void Move(float move) {
            Vector3 targetVelocity = new Vector2(move, m_Rigidbody2D.velocity.y);
            m_Rigidbody2D.velocity = targetVelocity;

            if (move > 0 && !m_FacingRight) Flip();
            else if (move < 0 && m_FacingRight) Flip();
    }

    public void Jump() {
        if (isGrounded){
            m_Rigidbody2D.velocity += jumpForce * Vector2.up;
            isGrounded = false;
        }  
    }

    private void Flip() {
        m_FacingRight = !m_FacingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}

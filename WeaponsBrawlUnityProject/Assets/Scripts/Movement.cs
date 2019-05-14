using UnityEngine;
using UnityEngine.Networking;

public class Movement : NetworkBehaviour {

    public bool isGrounded = false;
    public float jumpForce = 10;
    public float speed = 400f;

    public GameObject GroundCheck;

    private float horizontalMove = 0f;
    private bool m_FacingRight = true;

    private Vector3 m_Velocity = Vector3.zero;
    private Rigidbody2D m_Rigidbody2D;


    private void Start(){
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update () {
        if (hasAuthority)
            horizontalMove = Input.GetAxisRaw("Horizontal") * speed;
    }

    void FixedUpdate(){
        if (hasAuthority)
        {
            Move(horizontalMove * Time.fixedDeltaTime);
            if (Input.GetButtonDown("Jump"))
                Jump();
        }

    }

    public void Move(float move) {
        Vector2 targetVelocity = new Vector2(move, m_Rigidbody2D.velocity.y);
        m_Rigidbody2D.velocity = targetVelocity;

        if (move > 0 && !m_FacingRight)
            Flip();
        else if (move < 0 && m_FacingRight)
            Flip();
    }

    public void Jump() {
        if (isGrounded){
            m_Rigidbody2D.velocity += jumpForce * Vector2.up;
            isGrounded = false;
        }  
    }

    void Flip() {
        m_FacingRight = !m_FacingRight;
        transform.Rotate(0f, 180f, 0f);
    }


}

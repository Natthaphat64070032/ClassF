using UnityEngine;

public class playermoving : MonoBehaviour
{
    [SerializeField] private float speedplayer;
    [SerializeField] private LayerMask groundLayer;
    private Rigidbody2D body;
    private Animator anim;
    private  bool grounded;
    public float fireRate = 0.2f;
    public float nextFireRate = 0.0f;

    private void Awake() {

        // var rigid and animator
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update() {
        
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speedplayer, body.velocity.y);

        // Flip player
        if (horizontalInput > 0.01f) {
            transform.localScale = new Vector3(5,5,5);
        }
        else if (horizontalInput < -0.01f) {
            transform.localScale = new Vector3(-5,5,5);
        }

        if(Input.GetKey(KeyCode.W) && grounded) {
            Jump();
        }

        // set animator
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", grounded);

        if(Input.GetKey(KeyCode.P)){
            anim.SetBool("Attack",true);
        }else{
            anim.SetBool("Attack",false);
        }

    }

    private void Jump() {

        body.velocity = new Vector2(body.velocity.x, speedplayer);
        anim.SetTrigger("jump");
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Ground") {
            grounded = true;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision) {
          if (collision.gameObject.tag == "Enemy") {
            collision.GetComponent<Health>().TakeDamage(1);}
    }


    
}

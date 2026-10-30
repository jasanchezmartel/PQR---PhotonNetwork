using UnityEngine;

public class Player : MonoBehaviour {
    public float speed, jumpForce;

    private Rigidbody2D rig;
    private Animator anim;
    private SpriteRenderer sprite;

    private void Start() {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        float inputX = Input.GetAxis("Horizontal");

        rig.linearVelocity = (transform.right * speed * inputX) +
            (transform.up * rig.linearVelocity.y);

        if (inputX > 0) {
            sprite.flipX = false;
        } else if (inputX < 0) {
            sprite.flipX = true;
        }

        if (Input.GetButtonDown("Jump")) {
            rig.AddForce(transform.up * jumpForce);
        }

        anim.SetFloat("velocityX", Mathf.Abs(rig.linearVelocity.x));
        anim.SetFloat("velocityY", rig.linearVelocity.y);
    }
}
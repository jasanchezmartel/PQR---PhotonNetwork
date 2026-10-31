using UnityEngine;
using Photon.Pun;

public class Player : MonoBehaviourPun {
    public float speed, jumpForce;

    private Rigidbody2D rig;
    private Animator anim;
    private SpriteRenderer sprite;
    private bool facingLeft;

    private void Awake() {
        // Se asignan en todas las copias, también en las remotas
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        if (!photonView.IsMine) return;

        Camera cam = Camera.main;
        if (cam == null) cam = FindFirstObjectByType<Camera>();
        if (cam == null) {
            Debug.LogError("No hay ninguna cámara en la escena");
            return;
        }

        cam.transform.SetParent(transform);
        cam.transform.localPosition = new Vector3(0, 2, -10);
    }

    private void Update() {
        if (!photonView.IsMine) return;

        float h = Input.GetAxis("Horizontal");
        rig.linearVelocity = new Vector2(speed * h, rig.linearVelocity.y);

        // Solo se envía el RPC cuando cambia la dirección
        if (rig.linearVelocity.x > 0.1f && facingLeft) {
            facingLeft = false;
            photonView.RPC("RotateSprite", RpcTarget.All, false);
        } else if (rig.linearVelocity.x < -0.1f && !facingLeft) {
            facingLeft = true;
            photonView.RPC("RotateSprite", RpcTarget.All, true);
        }

        if (Input.GetButtonDown("Jump")) {
            rig.AddForce(transform.up * jumpForce);
        }

        anim.SetFloat("velocityX", Mathf.Abs(rig.linearVelocity.x));
        anim.SetFloat("velocityY", rig.linearVelocity.y);
    }

    [PunRPC]
    public void RotateSprite(bool rotate) {
        sprite.flipX = rotate;
    }
}
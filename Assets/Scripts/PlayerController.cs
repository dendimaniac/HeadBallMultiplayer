using System;
using UnityEngine;

public class PlayerController : Bolt.EntityBehaviour<IPlayerState>
{
    #region SerializeFields

    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float jumpForce = 400f;
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float ballPushForce = 5000f;

    #endregion

    #region NonSerializeFields

    private Rigidbody2D rb2D;
    private Vector2 movement;

    #endregion


    public override void Attached()
    {
        rb2D = GetComponent<Rigidbody2D>();

        state.SetTransforms(state.Transform, transform);
    }

    public override void SimulateOwner()
    {
        movement = Vector2.zero;

        movement.x = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            var jumpVector = new Vector2(0, jumpForce * BoltNetwork.FrameDeltaTime);
            rb2D.AddForce(jumpVector, ForceMode2D.Impulse);
        }

        if (movement == Vector2.zero) return;

        var playerTransform = transform;
        playerTransform.position += (Vector3) (BoltNetwork.FrameDeltaTime * moveSpeed * movement.normalized);
    }

//    private void OnCollisionEnter2D(Collision2D other)
//    {
//        if (!other.gameObject.CompareTag("Ball")) return;
//        
//        other.gameObject.GetComponent<Rigidbody2D>().AddForce(BoltNetwork.FrameDeltaTime * ballPushForce * movement.normalized, ForceMode2D.Impulse);
//    }

    private bool IsGrounded()
    {
        var playerPosition = transform.position;
        var groundCheck = new Vector2(playerPosition.x, playerPosition.y - 0.5f);
        var hit2D = Physics2D.Linecast(playerPosition, groundCheck, groundMask);
        return hit2D.transform != null;
    }
}
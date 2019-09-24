using UnityEngine;

public class PlayerController : Bolt.EntityBehaviour<IPlayerState>
{
    #region SerializeFields

    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float jumpForce = 400f;
    [SerializeField] private float moveSpeed = 4f;

    #endregion

    #region NonSerializeFields

    private Rigidbody2D rb2D;

    #endregion


    public override void Attached()
    {
        rb2D = GetComponent<Rigidbody2D>();

        state.SetTransforms(state.Transform, transform);
    }

    public override void SimulateOwner()
    {
        var movement = Vector2.zero;

        if (Input.GetKey(KeyCode.A))
        {
            movement.x -= 1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            movement.x += 1;
        }

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb2D.AddForce(new Vector2(0, jumpForce * BoltNetwork.FrameDeltaTime), ForceMode2D.Impulse);
        }

        if (movement == Vector2.zero) return;

        var playerTransform = transform;
        playerTransform.position += (Vector3) (BoltNetwork.FrameDeltaTime * moveSpeed * movement.normalized);
    }

    private bool IsGrounded()
    {
        var playerPosition = transform.position;
        var groundCheck = new Vector2(playerPosition.x, playerPosition.y - 0.5f);
        RaycastHit2D hit2D = Physics2D.Linecast(playerPosition, groundCheck, groundMask);
        return hit2D.transform != null;
    }
}
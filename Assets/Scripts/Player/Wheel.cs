using UnityEngine;

public class Wheel : MonoBehaviour
{
    public bool IsGrounded { get; private set; }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground")) IsGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground")) IsGrounded = false;
    }
}
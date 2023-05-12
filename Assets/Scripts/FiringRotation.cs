using UnityEngine;
using UnityEngine.InputSystem;
public class FiringRotation : MonoBehaviour
{
    // using script to rotate player with angle of Cstick. Got this code from a forum post, not really sure how it works.
    Controls controls;
    private Vector2 rotate;
    void Awake()
    {
        // get vector information from right stick
        controls = new Controls();
        controls.Gameplay.Rotate.performed += ctx => ctx.ReadValue<Vector2>();
    }
    private void Update()
    {
        // update players rotation with right stick turns
        if (Mathf.Abs(rotate.x) > 0.1 || Mathf.Abs(rotate.y) > 0.1)
        {
            Vector3 playerDirection = Vector3.right * -rotate.x + Vector3.forward * -rotate.y;
            if (playerDirection.sqrMagnitude > 0.0f)
            {
                Quaternion newRotation = Quaternion.LookRotation(playerDirection, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, 1000 * Time.deltaTime);
            }
        }
    }
    public void OnRotate(InputAction.CallbackContext ctx)
    {
        // get vector from right stick and assign it to variable at the top to use in update function
        rotate = ctx.ReadValue<Vector2>();
    }
}

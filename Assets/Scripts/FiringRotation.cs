using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class FiringRotation : MonoBehaviour
{
    private Vector2 rotate;
    Controls controls;
    Rigidbody playerRb;

    void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
        controls = new Controls();

        controls.Gameplay.Rotate.performed += ctx => ctx.ReadValue<Vector2>();

    }

    private void Update()
    {
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
        rotate = ctx.ReadValue<Vector2>();
    }

    



}

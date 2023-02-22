using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody))]
public class CharacterLocomotor : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float movementSpeed = 5;
    [SerializeField] private float turnSpeed = 180;

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();
        
    }
#endif

    private void Move(Vector3 direction)
    {
        rb.MovePosition(transform.position + transform.forward * direction.normalized.magnitude * movementSpeed * Time.deltaTime);
    }

    private void LookAt(float heading)
    {
        var targetRotation = Quaternion.Euler(0f, heading, 0f);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }

    private void SetMoveSpeed(float speed) => movementSpeed = speed;
}

using UnityEngine;

public class LightMovement : MonoBehaviour
{
    public Vector3 rotationSpeed;

    private void Update()
    {
        transform.Rotate(Time.deltaTime * rotationSpeed);
    }
}

using UnityEngine;

public class ObstacleRotation : MonoBehaviour
{
    void Update()
    {
        transform.Rotate (Vector3.down, 75 * Time.deltaTime, 0);
    }
}

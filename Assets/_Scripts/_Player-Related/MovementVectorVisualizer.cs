using System.Collections;
using System.Collections.Generic;
using UnityEngine;

# if UNITY_EDITOR
public class MovementVectorVisualizer : MonoBehaviour
{
    Rigidbody2D rb;
    void Start()
    {
        TryGetComponent<Rigidbody2D>(out rb);
    }
    void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position,transform.position + new Vector3(rb.velocity.x,rb.velocity.y),Color.blue);
    }
}
#endif
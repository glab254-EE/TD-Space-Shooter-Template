using Unity.Mathematics;
using UnityEngine;

public class MovementVectorVisualizer : MonoBehaviour
{
    [SerializeField] private GameObject visualizationobj;
    [SerializeField] private Color color;
    [SerializeField] private float Width = .5f;
    Rigidbody2D rb;
    GameObject currentvisualization;
    
    void Start()
    {
        currentvisualization = Instantiate(visualizationobj,transform.position,quaternion.identity);
        currentvisualization.name = "MovementVisualization";
        currentvisualization.GetComponent<SpriteRenderer>().color = color;
        TryGetComponent<Rigidbody2D>(out rb);
    }
    void FixedUpdate()
    {
        if (currentvisualization != null){
            Vector3 Velocity = new Vector3(rb.velocity.x,rb.velocity.y);
            Vector3 TargetPoint = transform.position + Velocity;
            Velocity.z = 0;
            currentvisualization.transform.position = transform.position + Velocity/2; // halfed to get middle point.
            currentvisualization.transform.localScale = new Vector3(Width,Vector3.Distance(transform.position,TargetPoint),1);
            currentvisualization.transform.up = (TargetPoint-currentvisualization.transform.position).normalized;
        }
    }
}
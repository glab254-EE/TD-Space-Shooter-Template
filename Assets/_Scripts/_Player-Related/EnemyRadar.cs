using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyRadarVisualization : MonoBehaviour
{
    [SerializeField] private GameObject visualizationobj;
    [SerializeField] private Color color;
    [SerializeField] private float Width = .5f;
    [SerializeField] private float Lenght = 1f;
    Dictionary<GameObject,GameObject> currentobjects;
    Rigidbody2D rb;
    void OnEnemyDestroyed(GameObject enemy)
    {
        if (currentobjects[enemy] != null){
            Destroy(currentobjects[enemy]);
            currentobjects.Remove(enemy);
        }
    }
    void AddVisualization(GameObject enemy)
    {
        GameObject currentvisualization = Instantiate(visualizationobj,transform.position,quaternion.identity);
        currentvisualization.name = "EnemyRadarVisualization";
        currentvisualization.GetComponent<SpriteRenderer>().color = color;
        currentobjects.Add(enemy,currentvisualization);
    }
    void Start()
    {
        currentobjects = new();
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy")){
            AddVisualization(enemy);
        }
    }
    void FixedUpdate()
    {
        foreach(KeyValuePair<GameObject,GameObject> vals in currentobjects)
        {
            if (vals.Key.IsDestroyed()) 
            {
                OnEnemyDestroyed(vals.Key);
                continue;
            }
            Vector3 direction = vals.Key.transform.position-transform.position;
            direction.Normalize();
            direction*=Lenght;
            Transform Visualization = vals.Value.transform;

            Visualization.position = transform.position+direction/2;
            Visualization.localScale = new Vector3(Width,Vector3.Distance(transform.position,transform.position+direction));
            Visualization.transform.up = direction;
        }
    }
}
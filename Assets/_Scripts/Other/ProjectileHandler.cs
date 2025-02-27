using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class ProjectileHandler : MonoBehaviour
{
    [SerializeField] internal int Damage;
    [SerializeField] internal string IgnoreTag;
    [SerializeField] private float Speed;
    [SerializeField] private float HomingSpeed;
    [SerializeField] private float SpeedDecay;
    [SerializeField] private float LifeTime;
    float currnetReaminingTime;
    float currentSpeed;
    Rigidbody2D rb;
    bool touched = false;
    Transform target;
    void Start()
    {
        currnetReaminingTime= LifeTime;
        currentSpeed = Speed;
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!touched)
        {
            rb.velocity = transform.up * currentSpeed;
            if (target != null){
                rb.AddForce(currentSpeed * HomingSpeed * Time.deltaTime * (target.position-transform.position).normalized);
            }
            currentSpeed -= SpeedDecay * Time.deltaTime;
        }
        currnetReaminingTime -= Time.deltaTime;
        if (currnetReaminingTime <= 0){
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(IgnoreTag) == false && !touched){
            touched = true;
            rb.velocity = Vector3.zero;
            if (collision.gameObject.TryGetComponent<PlayerHealthHandler>(out PlayerHealthHandler component))
            {
                component.TakeDamage(Damage);
            }else if (collision.gameObject.TryGetComponent<EnemyHealth>(out EnemyHealth component1))
            {
                component1.TakeDamage(Damage);
            }else if (collision.gameObject.TryGetComponent<DestructableObject>(out DestructableObject component2))
            {
                component2.TakeDamage(Damage);
            }
            
             // sorry teach, i just dont know how to make "shared" INTERNAL void from interface
            currnetReaminingTime = 1;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (HomingSpeed > 0 && (collision.gameObject.TryGetComponent<EnemyHandler>(out _) || collision.gameObject.TryGetComponent<PlayerHealthHandler>(out _)) && collision.gameObject.CompareTag(IgnoreTag) == false){
            target = collision.transform;
        } 
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform == target){
            target = null;
        }
    }
}

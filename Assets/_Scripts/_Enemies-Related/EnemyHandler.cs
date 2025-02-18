using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    [Header("Speed settings")]
    [SerializeField] private float GlobalSpeed = 10;
    [SerializeField] private float ForwardThrustMultiplier = 2;
    [SerializeField] private float SpeedMultiplyer = 2;
    [SerializeField] private float TurnSpeed = 2;
    [Header("AI settings")]
   /* [SerializeField] private float MinTurnToMoveForward = 0.25f;
    [SerializeField] private float SideMovementTreeshold = 0.1f;*/
    [SerializeField] private int DamageOnTouch = -1;
    [SerializeField] private float DamageCooldown = -1;
    Rigidbody2D rb;
    Transform target;
    bool CanDamage = true;
    Vector2 movementvector()
    {
        Vector2 result = Vector2.zero;
        if (target != null)
        {
            
            result.y = 1;
        }
        return result;
    }
     void TurnTowardsMouse()
    {
        Vector3 difference = target.position - transform.position; 
        difference.z = transform.position.z;
        float TargetRotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg; // removed 90 from targetrotation as its fixed angle needed to face mouse
        transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.Euler(0,0,TargetRotationZ-90),Time.deltaTime*TurnSpeed);
    }
    void MoveStep()
    {
        Vector2 force = movementvector();
        float BackForthaddition = force.y > 0 ? SpeedMultiplyer * ForwardThrustMultiplier : SpeedMultiplyer;
        Vector2 newforce = transform.up * force.y * GlobalSpeed  * BackForthaddition + transform.right * force.x * SpeedMultiplyer * GlobalSpeed;
        rb.AddForce(newforce * Time.deltaTime);
    }
    IEnumerator Damage(PlayerHealthHandler component){
        CanDamage = false;
        component.Health -= DamageOnTouch;
        Debug.Log(component.Health);
        if (DamageCooldown != -1)
        {
            yield return new WaitForSeconds(DamageCooldown);
        }
        CanDamage = true;
    }
    void Start()
    {
        if (!gameObject.TryGetComponent<Rigidbody2D>(out rb))
        {
            Debug.LogError("No RigidBody2d found!");
        }
    }
    void Update()
    {
        if (target != null)
        {
            TurnTowardsMouse();
            MoveStep();
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerHealthHandler>(out PlayerHealthHandler component))
        {
            if (CanDamage && DamageOnTouch != -1)
            {
                StartCoroutine(Damage(component));
            }
        }   
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerHealthHandler>(out PlayerHealthHandler component))
        {
            target = collision.transform;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerHealthHandler>(out PlayerHealthHandler component))
        {
            if (target != null){
                target = null;
            }
        } 
    }
}

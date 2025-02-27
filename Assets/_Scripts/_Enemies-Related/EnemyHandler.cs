using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(EnemyHealth)),RequireComponent(typeof(Rigidbody2D))]
public class EnemyHandler : MonoBehaviour
{
    [Header("Speed settings")]
    [SerializeField] private float GlobalSpeed = 10;
    [SerializeField] private float ForwardThrustMultiplier = 2;
    [SerializeField] private float SpeedMultiplyer = 2;
    [SerializeField] private float TurnSpeed = 2;
    [Header("AI damage settings")]
    [SerializeField] private int DamageOnTouch = -1;
    [SerializeField] private float DamageCooldown = -1;
    [SerializeField] private GameObject projectile;
    [SerializeField] private float ProjectileCoolDown = 4;
    [SerializeField] private int ProjectileDamage = 1;
    EnemyHealth health;
    Rigidbody2D rb;
    Transform target;
    bool CanDamage = true;
    bool CanShoot = true;
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
    IEnumerator Shoot()
    {
        CanShoot = false;
        GameObject bullet = Instantiate(projectile,transform.position+transform.up,transform.rotation);
        if (bullet.TryGetComponent<ProjectileHandler>(out ProjectileHandler projectileHandler))
        {
            projectileHandler.Damage = ProjectileDamage;
            projectileHandler.IgnoreTag = gameObject.tag;
        }
        yield return new WaitForSeconds(ProjectileCoolDown);
        CanShoot = true;
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
        health = GetComponent<EnemyHealth>();
        if (!gameObject.TryGetComponent<Rigidbody2D>(out rb))
        {
            Debug.LogError("No RigidBody2d found!");
        }
        if (projectile == null || ProjectileCoolDown <= 0) CanShoot = false;
        else CanShoot = true;
    }
    void Update()
    {
        if (target != null && health.Health > 0)
        {
            TurnTowardsMouse();
            MoveStep();
            if (CanShoot) StartCoroutine(Shoot());
        } else if (health.Health <= 0){
            target = null;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerHealthHandler>(out PlayerHealthHandler component) && health.Health > 0)
        {
            if (CanDamage && DamageOnTouch != -1)
            {
                StartCoroutine(Damage(component));
            }
        }   
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerHealthHandler>(out _) && health.Health > 0)
        {
            target = collision.transform;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerHealthHandler>(out PlayerHealthHandler _))
        {
            if (target != null){
                target = null;
            }
        } 
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamagable
{
    [field:SerializeField]public int MaxHealth {get; private set;}
    [SerializeField] private ParticleSystem DeathParticle;
    [SerializeField] private AudioClip DeathSFX;
    [SerializeField] private AudioSource sfxsource;
    public int Health {get; internal set;}

    void Awake()
    {
        Health = MaxHealth;
    }
    IEnumerator Respawn(){
        if (DeathParticle != null){
            DeathParticle.Play();
        }
        if (sfxsource != null && DeathSFX != null){
            sfxsource.clip = DeathSFX;
            sfxsource.Play();
        }
        yield return new WaitForSecondsRealtime(5);
        Destroy(gameObject);
    }
    internal void TakeDamage(int Damage){
        if (Health > -1) Health = Mathf.Clamp(Health-Damage,0,MaxHealth);
    }
    public void Update()
    {
        if (Health == 0){
            Health = -1;
            StartCoroutine(Respawn());
        }
    }
}


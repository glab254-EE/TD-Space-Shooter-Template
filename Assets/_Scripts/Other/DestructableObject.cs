using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestructableObject : MonoBehaviour, IDamagable
{
    [field:SerializeField]public int MaxHealth {get; private set;}
    public int Health {get; internal set;}

    void Awake()
    {
        Health = MaxHealth;
    }
    internal void TakeDamage(int Damage){
        Health = Mathf.Clamp(Health-Damage,0,MaxHealth);
    }
    public void Update()
    {
        if (Health == 0){
            Health = -1;
            Destroy(gameObject);
        }
    }
}

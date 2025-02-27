using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DestructableObject : MonoBehaviour, IDamagable
{
    [field:SerializeField]public int MaxHealth {get; private set;}
    [field:SerializeField]private ParticleSystem particles;
    public int Health {get; internal set;}


    void Awake()
    {
        Health = MaxHealth;
    }
    IEnumerator OnKill(){
        if (gameObject.TryGetComponent<SpriteRenderer>(out SpriteRenderer renderer)){
            renderer.material.SetColor("_Color",new Color(0,0,0,0));
        }
        gameObject.GetComponent<Collider2D>().isTrigger = true;
        particles.Play();
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
    internal void TakeDamage(int Damage){
        Health = Mathf.Clamp(Health-Damage,0,MaxHealth);
    }
    public void Update()
    {
        if (Health == 0){
            Health = -1;
            StartCoroutine(OnKill());
        }
    }
}

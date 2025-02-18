using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthHandler : MonoBehaviour
{
    [SerializeField] internal int MaxHealth;
    internal int Health;
    void Awake()
    {
        Health = MaxHealth;
    }
    IEnumerator Respawn(){
        Destroy(gameObject);
        yield return new WaitForSecondsRealtime(5);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
    public void Update()
    {
        if (Health == 0){
            Health = -1;
            StartCoroutine(Respawn());
        }
    }
}

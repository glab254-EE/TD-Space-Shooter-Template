using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Shooting_Handler : MonoBehaviour
{
    [SerializeField] private float Firerate = 60;
    [SerializeField] private GameObject Bullet;
    [SerializeField] private int Damage;
    [field:SerializeField] private PlayerHealthHandler health;
    [SerializeField] private AudioClip ShootSFX;
    [SerializeField] private AudioSource sfxsource;
    CorePlayerInput inputActions;
    bool _shooting = false;
    float _CoolDown;
    void OnButtonPress(InputAction.CallbackContext callbackContext)
    {
        _shooting = callbackContext.ReadValueAsButton();
        _CoolDown = 60/Firerate;
    }
    void Fire(){
        if (ShootSFX != null && sfxsource != null){
            sfxsource.clip = ShootSFX;
            sfxsource.Play();
        }
        GameObject _bullet = Instantiate(Bullet,transform.position+transform.up,transform.rotation);
        if (_bullet.TryGetComponent<ProjectileHandler>(out ProjectileHandler projectileHandler))
        {
            projectileHandler.Damage = Damage;
            projectileHandler.IgnoreMask = gameObject.layer;
            projectileHandler.IgnoreMask = 3;
        }
    }
    void Start()
    {
        _CoolDown = 60/Firerate;
        inputActions = new();
        inputActions.PrimaryGameControl.Fire.performed += OnButtonPress;
        inputActions.PrimaryGameControl.Fire.canceled  += OnButtonPress;
        inputActions.PrimaryGameControl.Fire.Enable();
    }

    void Update()
    {
        if (health != null && health.Health > 0 && _shooting){
            _CoolDown -= Time.deltaTime;
            if (_CoolDown <= 0){
                Fire();
                _CoolDown = 60/Firerate;
            }
        }
    }
    void OnDestroy()
    {
        _shooting = false;
        inputActions.PrimaryGameControl.Fire.performed -= OnButtonPress;
        inputActions.PrimaryGameControl.Fire.canceled  -= OnButtonPress;
        inputActions.PrimaryGameControl.Fire.Disable();
    }
}

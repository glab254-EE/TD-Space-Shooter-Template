using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ParticleOnKeybind : MonoBehaviour
{
    [SerializeField] private ParticleSystem particle;
    [Tooltip("Must be an button type to work properly.")]
    [SerializeField] private InputAction keytopress;
    [SerializeField] private bool HeldDown;
    bool _held = false;
    private void OnPerfom(InputAction.CallbackContext callbackContext)
    {
        _held = callbackContext.ReadValueAsButton();
    }
    void Start()
    {
        if (particle == null) particle = gameObject.GetComponent<ParticleSystem>();
        keytopress.performed += OnPerfom;
        keytopress.canceled += OnPerfom;
        keytopress.Enable();
    }

    void Update()
    {
        if (_held && !particle.isPlaying){
            if (!HeldDown) _held = false;
            particle.Play();
        } else if (!_held && particle.isPlaying){
            particle.Stop();
        }
    }
    void OnDestroy()
    {
        keytopress.performed -= OnPerfom;
        keytopress.canceled -= OnPerfom;
        keytopress.Disable();
    }
}

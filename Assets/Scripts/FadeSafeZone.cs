using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeSafeZone : MonoBehaviour
{ 
    private MeshRenderer _meshRenderer;
    private Material _material;
    private float _ogTransparency;
    private static readonly int TweakTransparency = Shader.PropertyToID("_Tweak_transparency");
    
    // Start is called before the first frame update
    void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _material = _meshRenderer.sharedMaterial;
        _ogTransparency = 0.35f;
        _material.SetFloat(TweakTransparency, -1);
    }

    private IEnumerator FadeIn()
    { 
        for (float i = -1; i <= _ogTransparency; i += 0.05f)
        {
            _material.SetFloat(TweakTransparency, i);
            yield return new WaitForSeconds(0.01f);
        }
        _material.SetFloat(TweakTransparency, _ogTransparency);
    }
    
    private void OnEnable()
    {
        _material ??= _meshRenderer.sharedMaterial;
        StartCoroutine(FadeIn());
    }
    
    public void FadeOut()
    {
        if (gameObject.activeSelf)
        {
            StartCoroutine(FadeOutRoutine());

        }
    }
    
    private IEnumerator FadeOutRoutine()
    {
        for (float i = _ogTransparency; i >= -1; i -= 0.05f)
        {
            _material.SetFloat(TweakTransparency, i);
            yield return new WaitForSeconds(0.01f);
        }
        _material.SetFloat(TweakTransparency, _ogTransparency);
        gameObject.SetActive(false);
    }
}

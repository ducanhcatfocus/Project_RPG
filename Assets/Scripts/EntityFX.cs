using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    [SerializeField] private Material hitMaterial;
    private Material originalMaterial;

    [SerializeField] private Color[] chillColor;
    [SerializeField] private Color[] igniteColor;
    [SerializeField] private Color[] shockColor;


    SpriteRenderer sr;


    private void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        originalMaterial = sr.material;
    }

    private IEnumerator FlashFX()
    {
        sr.material = hitMaterial;
        Color currentColor = sr.color;
        sr.color = Color.white;
        yield return new WaitForSeconds(.2f);

        sr.color = currentColor;
        sr.material = originalMaterial;
    }

    private void RedColorBlinkFx()
    {
        if (sr.color != Color.white)
            sr.color = Color.white;
        else sr.color = Color.red;
    }

    private void CancelRedBlickFX()
    {
        CancelInvoke();
        sr.color = Color.white;
    }

    public void InvokeIgniteColorFX(float second)
    {
        InvokeRepeating("IgniteColorFx", 0, 0.3f);
        Invoke("CancelRedBlickFX", second);
    }

    public void InvokeShockColorFX(float second)
    {
        InvokeRepeating("ShockColorFx", 0, 0.3f);
        Invoke("CancelRedBlickFX", second);
    }

    public void InvokeChillColorFX(float second)
    {
        InvokeRepeating("ChillColorFx", 0, 0.3f);


        Invoke("CancelRedBlickFX", second);
    }
    private void IgniteColorFx()
    {
        if (sr.color != igniteColor[0])
            sr.color = igniteColor[0];
        else sr.color = igniteColor[1];
    }

    private void ShockColorFx()
    {
        if (sr.color != shockColor[0])
            sr.color = shockColor[0];
        else sr.color = shockColor[1];
    }

    private void ChillColorFx()
    {
        if (sr.color != chillColor[0])
            sr.color = chillColor[0];
        else sr.color = chillColor[1];
    }


}

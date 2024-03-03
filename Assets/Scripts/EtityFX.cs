using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EtityFX : MonoBehaviour
{
    private SpriteRenderer sr;
    [Header("Flash FX")]
    [SerializeField] private Material hitMat;
    private Material originMat;

    private void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        originMat = sr.material;
    }

    public IEnumerator FlashFX()
    {
        sr.material = hitMat;
        yield return new WaitForSeconds(.2f);
        sr.material = originMat;
    }
}

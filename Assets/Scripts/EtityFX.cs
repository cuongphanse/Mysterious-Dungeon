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

    public void RedColorBlink()
    {
        if(sr.color != Color.white)
        {
            sr.color = Color.white;
        }else
        {
            sr.color = Color.red;
        }
    }
    public void CancelRedBlink()
    {
        CancelInvoke();
        sr.color = Color.white ;
    }
}

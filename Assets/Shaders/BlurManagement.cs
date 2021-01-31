using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlurManagement : MonoBehaviour
{
    public Camera blurCamera;
    public Material blurMT;
    void Start()
    {
        if(blurCamera.targetTexture != null)
        {
            blurCamera.targetTexture.Release();
        }
        blurCamera.targetTexture = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.ARGB32, 1);
        blurMT.SetTexture("_RenderTex", blurCamera.targetTexture);
    }
}

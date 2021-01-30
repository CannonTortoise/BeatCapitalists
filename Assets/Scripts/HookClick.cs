using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookClick : MonoBehaviour
{
    private HookScript hook;

    private void Start()
    {
        hook = GetComponentInChildren<HookScript>();
    }

    private void OnMouseDown()
    {
        hook.BeClicked();
    }
}

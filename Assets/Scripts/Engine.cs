using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{
    public Color highlightColor;
    public GameObject selectionPanel;

    private void OnMouseEnter()
    {
        GetComponent<SpriteRenderer>().color = highlightColor;
    }

    private void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    private void OnMouseUp()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
        selectionPanel.SetActive(true);
    }

    public void CloseSelectionPanel()
    {
        selectionPanel.SetActive(false);
    }

}

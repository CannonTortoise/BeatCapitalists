﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;

public class RingMenu : MonoBehaviour
{
    public Ring Data;
    public RingPiece RingPiecePrefab;
    public float GapWidthDegree = 1f;
    public Action<string> callback;
    protected RingPiece[] Pieces;
    //protected RingM
    [HideInInspector]
    public string Path;

    void Start()
    {
        var stepLength = 360f / Data.Elements.Length;
        var iconDist = Vector3.Distance(RingPiecePrefab.Icon.transform.position, RingPiecePrefab.Piece.transform.position);

        Pieces = new RingPiece[Data.Elements.Length];
        for (int i = 0; i < Data.Elements.Length; i++)
        {
            Pieces[i] = Instantiate(RingPiecePrefab, transform);
            Pieces[i].transform.localPosition = Vector3.zero;
            Pieces[i].transform.localRotation = Quaternion.identity;

            //set icon
            Pieces[i].Icon.transform.localPosition = Pieces[i].Piece.transform.localPosition + Quaternion.AngleAxis(i * stepLength, Vector3.forward) * Vector3.up * iconDist;
            Pieces[i].Icon.sprite = Data.Elements[i].Icon;

            //set piece
            Pieces[i].Piece.fillAmount = 1f / Data.Elements.Length - GapWidthDegree / 360f;
            Pieces[i].Piece.transform.localPosition = Vector3.zero;
            Pieces[i].Piece.transform.localRotation = Quaternion.Euler(0, 0, -stepLength / 2f + GapWidthDegree / 2f + i * stepLength - 30f);
            Pieces[i].Piece.color = new Color(1f, 1f, 1f, 0.5f);
        }
    }

    private void Update()
    {
        var stepLength = 120f;
        //判断分段激活
        var mouseAngle = NormalizeAngle(Vector3.SignedAngle(Vector3.up, Input.mousePosition - transform.position, Vector3.forward) + stepLength / 1f);
        var activeElement = (int)(mouseAngle / stepLength);
        for (int i = 0; i < Data.Elements.Length; i++)
        {
            if (i == activeElement)
                Pieces[i].Piece.color = new Color(1f, 1f, 1f, 0.75f);
            else
                Pieces[i].Piece.color = new Color(1f, 1f, 1f, 0.5f);
        }

        if (Input.GetMouseButtonDown(0))
        {
            var path = Path + "/" + Data.Elements[activeElement].Name; //档位接口
            if(path == "/955")
            {
                Debug.Log("Mode 1");
                WorkersController.Instance.WorkStrength = 0;
            }
            else if(path == "/996")
            {
                Debug.Log("Mode 2");
                WorkersController.Instance.WorkStrength = 1;
            }
            else if (path == "/007")
            {
                Debug.Log("Mode 3");
                WorkersController.Instance.WorkStrength = 2;
            }
            //callback?.Invoke(path);
            gameObject.SetActive(false);
        }
    }

    private float NormalizeAngle(float a) => (a + 360f) % 360f;
}

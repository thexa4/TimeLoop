﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LoopLib;

public class TankView : Containers.LoopEntity
{

    [Header("View Settings")]
    public GameObject Turret;
    public GameObject[] Bodies;
    public GameObject ExplosionPrefab;
    public RadialTransfrom RadTransfrom;

    public bool live = true;

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    public override void OnUpdate(ClientView view)
    {
        var tank = view.Get((TankEntity)Type.LoopType, Id);
       
        if (tank != null)
        {
            gameObject.SetActive(true);
            RadTransfrom.Positsion = tank.Value.X;
            RadTransfrom.Height = tank.Value.Y;
            Turret.transform.localRotation = Quaternion.AngleAxis(tank.Value.TurretAngle, new Vector3(0, 0, 1));
        }
        else
        {
            if (live && gameObject.activeSelf)
            {
                live = false;
                var go = GameObject.Instantiate(ExplosionPrefab);
                go.transform.SetParent(gameObject.transform.parent, false);
                go.transform.localPosition = gameObject.transform.localPosition + new Vector3(0.2f, 0.1f, -1f);

                go = GameObject.Instantiate(ExplosionPrefab);
                go.transform.SetParent(gameObject.transform.parent, false);
                go.transform.localPosition = gameObject.transform.localPosition + new Vector3(0.1f, 0.0f, -1f);

                go = GameObject.Instantiate(ExplosionPrefab);
                go.transform.SetParent(gameObject.transform.parent, false);
                go.transform.localPosition = gameObject.transform.localPosition + new Vector3(-0.1f, 0.3f, -1f);
            }
            //gameObject.SetActive(false);
        }
    }
}

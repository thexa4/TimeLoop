using System.Collections;
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
    public GameObject Sprites;

    private bool _isLive = false;

    public bool IsLive { get { return _isLive; } }

    // Use this for initialization
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void OnUpdate(ClientState view)
    {
        var tank = view.Get((TankEntity)Type.LoopType, Id);
       
        if (tank != null)
        {
            _isLive = true;
            Sprites.SetActive(true);
            foreach (var body in Bodies)
            {
                body.SetActive(false);
            }
            Bodies[tank.Value.TankColorId].SetActive(true);
            RadTransfrom.Positsion = tank.Value.X;
            RadTransfrom.Height = tank.Value.Y;
            Turret.transform.localRotation = Quaternion.AngleAxis(tank.Value.TurretAngle, new Vector3(0, 0, 1));
        }
        else
        {
            Sprites.SetActive(false);
            if (_isLive && Sprites.activeSelf)
            {
                _isLive = false;
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
        }
    }
}

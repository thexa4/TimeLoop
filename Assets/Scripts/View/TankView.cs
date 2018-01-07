using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LoopLib;

public class TankView : MonoBehaviour
{

    [Header("View Settings")]
    public GameObject Turret;
    public SpriteRenderer BodySprite;
    public GameObject boom;


    [Header("Tank Settings")]
    public int TankId;
    public Color Color;
    public GameController GameController;

    public bool live = true;

    public void OnValidate()
    {
        if (BodySprite != null)
        {
            BodySprite.color = Color;
        }
    }

    // Use this for initialization
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    public void OnUpdate(ClientView view)
    {
        var tank = view.Get(GameController.TankEnityType, TankId);
        if (tank != null)
        {
            gameObject.SetActive(true);
            transform.localPosition = new Vector3(tank.Value.X, tank.Value.Y, 0);
            Turret.transform.rotation = Quaternion.AngleAxis(tank.Value.TurretAngle, new Vector3(0, 0, 1));
        }
        else
        {
            if (live && gameObject.activeSelf)
            {
                live = false;
                var go = GameObject.Instantiate(boom);
                go.transform.SetParent(gameObject.transform.parent, false);
                go.transform.localPosition = gameObject.transform.localPosition + new Vector3(0.2f, 0.1f, -1f);

                go = GameObject.Instantiate(boom);
                go.transform.SetParent(gameObject.transform.parent, false);
                go.transform.localPosition = gameObject.transform.localPosition + new Vector3(0.1f, 0.0f, -1f);

                go = GameObject.Instantiate(boom);
                go.transform.SetParent(gameObject.transform.parent, false);
                go.transform.localPosition = gameObject.transform.localPosition + new Vector3(-0.1f, 0.3f, -1f);

                Color = Color.black;
                OnValidate();
            }
            //gameObject.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LoopLib;

public class TankView : MonoBehaviour {

    [Header("View Settings")]
    public GameObject Turret;
    public SpriteRenderer BodySprite;


    [Header("Tank Settings")]
    public int TankId;
    public Color Color;
    public GameController GameController;

    public void OnValidate()
    {
        if (BodySprite != null)
        {
            BodySprite.color = Color;
        }
    }

    // Use this for initialization
    void Start ()
    {
        gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	public void OnUpdate (ClientView view)
    {
        var tank = view.Get(GameController.TankEnityType, TankId);
        if(tank != null)
        {
            gameObject.SetActive(true);
            transform.position = new Vector3(tank.Value.X, tank.Value.Y, 0);
            Turret.transform.rotation = Quaternion.AngleAxis(tank.Value.TurretAngle, new Vector3(0, 0, 1));
        }
        else
        {
            gameObject.SetActive(false);
        }
	}
}

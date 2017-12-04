using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniverseBehavior : MonoBehaviour {

    LoopLib.Universe universe;

	// Use this for initialization
	void Start () {
        LoopLib.EntityType[] types = {
            new LinearMoveEntity(),
        };
        universe = new LoopLib.Universe(20, 60, 6, types);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

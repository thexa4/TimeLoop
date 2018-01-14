using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldDraw : MonoBehaviour {

    private Material material;

	// Use this for initialization
	void Start () {
        var shader = Shader.Find("Draw/WorldShader");
        material = new Material(shader);

        var renderer = GetComponent<MeshRenderer>();

        renderer.material = material;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

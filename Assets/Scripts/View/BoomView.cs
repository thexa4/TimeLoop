using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomView : MonoBehaviour {



	// Use this for initialization
	IEnumerator Start () {
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
	}
	
}

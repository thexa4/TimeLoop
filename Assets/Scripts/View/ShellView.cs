using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LoopLib;

public class ShellView : Containers.LoopEntity {

    [Header("View Settings")]
    public GameObject ExplosionPrefab;

    // Update is called once per frame
    public override void OnUpdate (ClientView view)
    {
       // var shell = view.Get(GameController.ShellEnityType, ShellId);
        /*if(shell != null)
        {
            gameObject.SetActive(true);
            transform.localPosition = new Vector3(shell.Value.X, shell.Value.Y, 0);
            transform.rotation = Quaternion.LookRotation(Vector3.forward, new Vector3(shell.Value.XSpeed, shell.Value.YSpeed, 0f));
        }
        else*/
        {
            if (gameObject.activeSelf)
            {
                var go = GameObject.Instantiate(ExplosionPrefab);
                go.transform.SetParent(gameObject.transform.parent, false);
                go.transform.localPosition = new Vector3(transform.localPosition.x,0f);

            }
            gameObject.SetActive(false);
        }
	}
}

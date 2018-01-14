using UnityEngine;
using LoopLib;

public class ShellView : Containers.LoopEntity {

    [Header("View Settings")]
    public GameObject Sprites;
    public GameObject ExplosionPrefab;
    public RadialTransfrom RadTransform;

    public override void OnUpdate (ClientState view)
    {
        var shell = view.Get((ShellEntity)Type.LoopType, Id);
        if (shell != null)
        {
            Sprites.SetActive(true);
            RadTransform.Positsion = shell.Value.X;
            RadTransform.Height = shell.Value.Y;
           //transform.localRotation = Quaternion.LookRotation(Vector3.forward, new Vector3(shell.Value.XSpeed, shell.Value.YSpeed, 0f));
        }
        else
        {
            if (Sprites.activeSelf)
            {
                var go = Instantiate(ExplosionPrefab);
                go.transform.SetParent(gameObject.transform.parent, false);
                go.transform.localPosition = new Vector3(transform.localPosition.x,0f);
            }
            Sprites.SetActive(false);
        }
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectCloner : MonoBehaviour {

    public GameObject Template;
    
    public int CloneAmount;

    public void CloneObjects()
    {
        if (Template == null) return;

        for (int i = transform.childCount; i < CloneAmount; i++)
        {
            var go = Instantiate(Template);
            go.name = string.Format("{1} instance {0}", i + 1, Template.name);
            go.transform.SetParent(Template.transform.parent, false);
        }            
    }
}

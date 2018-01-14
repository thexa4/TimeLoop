using UnityEngine;

public class RadialTransfrom : MonoBehaviour
{

    [SerializeField]
    public GameObject _world;

    [SerializeField]
    public float _height = 0f;

    [SerializeField]
    public float _positsion = 0f;

    [SerializeField]
    public float _depth = 0f;

    public float WorldSize
    {
        get
        {
            return _world.transform.localScale.x / 2;
        }
    }

    public float Height
    {
        get
        {
            return _height;
        }
        set
        {
            _height = value;
            Reposition();
        }
    }

    public float Positsion
    {
        get
        {
            return _positsion;
        }
        set
        {
            _positsion = value;
            Reposition();
        }
    }

    public float Depth
    {
        get
        {
            return _depth;
        }
        set
        {
            _depth = value;
            Reposition();
        }
    }

    private void OnValidate()
    {
        Reposition();
    }

    private void Reposition()
    {
        transform.localPosition = new Vector3(Mathf.Cos(Positsion + (Mathf.PI / 2)) * (Height + WorldSize), Mathf.Sin(Positsion + (Mathf.PI / 2)) * (Height + WorldSize), Depth);
        transform.localRotation = Quaternion.Euler(0f, 0f, (Positsion * Mathf.Rad2Deg));
    }

}

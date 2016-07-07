using UnityEngine;
using System.Collections;

public class LayerConfig : MonoBehaviour
{

    public static LayerConfig instance;

    public LayerMask projectileCollision;


    void Awake()
    {
        instance = this;
    }
}

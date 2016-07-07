using UnityEngine;
using System.Collections;

public class ProjectileConfig : MonoBehaviour
{
    public static ProjectileConfig instance;

    public float projectileHeight;

    void Awake()
    {
        instance = this;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private VolumeControl volumeControl;

    void Start()
    {
        volumeControl.DefineSliders();
    }
}
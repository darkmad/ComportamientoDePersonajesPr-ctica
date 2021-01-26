using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverData : MonoBehaviour
{

    public bool[] positions;

    private void Start()
    {
        positions = new bool[6];
        for (int i =0; i< positions.Length; i++)
        {
            positions[i] = false;
        }
    }

}

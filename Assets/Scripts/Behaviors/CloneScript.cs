using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneScript : MonoBehaviour {

    public GameObject cloneMe(float x, float y, float z)
    {
        return Instantiate(gameObject, new Vector3(x, y, z), Quaternion.identity);
    }
}

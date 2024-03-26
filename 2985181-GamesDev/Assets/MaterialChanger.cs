using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class MaterialChanger : MonoBehaviour
{
    // Variables
    public Material testMat;
    // Start is called before the first frame update
    void Start()
    {
        testMat = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        testMat.color = Random.ColorHSV();
    }
}

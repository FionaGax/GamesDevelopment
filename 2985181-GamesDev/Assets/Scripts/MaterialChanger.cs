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
    
    private void OnCollisionEnter()
    {
        testMat.color = Random.ColorHSV();
    }
}

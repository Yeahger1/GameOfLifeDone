using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueHolder : MonoBehaviour
{
    public float spawnChancePercentage = 15;
    public float cameraSize;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AdjustSpawnChancePercentage(float newSpawnChancePercentage)
    {
        spawnChancePercentage = newSpawnChancePercentage;
    }
    public void AdjustCameraSize(float newCameraSize)
    {
        cameraSize = newCameraSize;
    }
}

using UnityEngine;

public class StepOne : MonoBehaviour
{

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float newZ = startPos.z + (Mathf.Sin(Time.time))/4;
        transform.position = new Vector3(startPos.x, startPos.y, newZ);
    }
}
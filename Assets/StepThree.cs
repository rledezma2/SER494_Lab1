using UnityEngine;

public class StepThree : MonoBehaviour
{

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float newZ = startPos.z + (Mathf.Sin(Time.time))/2;
        transform.position = new Vector3(startPos.x, startPos.y, newZ);
    }
}
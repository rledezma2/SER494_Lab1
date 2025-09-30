using UnityEngine;

public class StepFive : MonoBehaviour
{

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float newY = startPos.y + (Mathf.Sin(Time.time))/4;
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }
}
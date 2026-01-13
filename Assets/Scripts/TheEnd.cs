using UnityEngine;

public class TheEnd : MonoBehaviour
{
    public float scrollSpeed = 0.1f;

    void Update()
    {
        transform.position +=
            Vector3.up * scrollSpeed * Time.deltaTime;
    }
}
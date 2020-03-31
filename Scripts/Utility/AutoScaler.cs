using UnityEngine;
using System.Collections;

public class AutoScaler : MonoBehaviour
{
    public float scaleSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.localScale += (new Vector3(scaleSpeed, scaleSpeed, scaleSpeed) * Time.deltaTime);
    }
}

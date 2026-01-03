using System.Collections;
using UnityEngine;

public class CrushingHazard : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        StartCoroutine(Crush());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Crush()
    {
        while (true)
        {
            transform.Translate(Vector2.down * 6);
            yield return new WaitForSeconds(1.5f);
            transform.Translate(Vector2.up * 6);
            yield return new WaitForSeconds(2);
        }
    }
}

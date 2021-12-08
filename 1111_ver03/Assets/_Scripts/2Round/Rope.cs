using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    [SerializeField] private Transform startX;
    [SerializeField] private Transform endX;
    [SerializeField] private Transform startZ;
    [SerializeField] private Transform endZ;
    //[SerializeField] private float angleY = 0f;
    [SerializeField] private float direction = 8.0f;

    private float currentX;
    private float currentZ;

    void Start()
    {
        
        

        print(transform.eulerAngles.y);
        switch (transform.eulerAngles.y)
        {
            case 0f:
                currentX = transform.position.x;
                print("Rope: 0");
                StartCoroutine(MoveX());
                break;
            case 270f:
                currentZ = transform.position.z;
                print("Rope: 270");
                StartCoroutine(MoveZ());
                break;
        }
    }

    IEnumerator MoveX()
    {
        while (true)
        {
            currentX += Time.deltaTime * direction;

            if (currentX >= endX.localPosition.x)
            {
                direction *= -1;
                currentX = endX.localPosition.x;
            }

            if (currentX <= startX.localPosition.x)
            {
                currentX = startX.localPosition.x;
                direction *= -1;
            }

            transform.localPosition = new Vector3(currentX, transform.localPosition.y, transform.localPosition.z);

            yield return null;
        }
    }

    IEnumerator MoveZ()
    {
        while (true)
        {
            currentZ += Time.deltaTime * direction;

            if (currentZ >= endZ.localPosition.z)
            {
                direction *= -1;
                currentZ = endZ.localPosition.z;
            }

            if (currentZ <= startZ.localPosition.z)
            {
                currentZ = startZ.localPosition.z;
                direction *= -1;
            }

            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, currentZ);

            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            print("Rope: Player Trigger");
            other.gameObject.SetActive(false);
        }
    }
}
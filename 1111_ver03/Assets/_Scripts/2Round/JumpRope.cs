using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpRope : MonoBehaviour
{
    [SerializeField] private GameObject ropePrefab;
    [SerializeField] private Transform ropeParent;
    [SerializeField] Transform angle0;
    [SerializeField] Transform angle90;
    [SerializeField] Text countText;

    private float angleY = 0f;
    private int count = 3;
    //private float delayTime = 10;

    private Vector3 test;

    void Start()
    {
        StartCoroutine(CountDown());
        //StartCoroutine(GameUpdate());
    }

    IEnumerator CountDown()
    {
        while (true)
        {
            if (count == 0)
            {
                countText.gameObject.SetActive(false);
                yield return StartCoroutine(GameUpdate());
            }

            countText.text = count.ToString();
            yield return new WaitForSeconds(1.0f);
            count--;
        }
    }

    IEnumerator GameUpdate()
    {
        while (true)
        {
            test = Vector3.zero;

            switch (angleY)
            {
                case 0f:
                    test = angle0.localPosition;
                    Instantiate(ropePrefab, test, Quaternion.Euler(0, 0, 0), ropeParent);
                    angleY = -90f;
                    break;
                case -90f:
                    test = angle90.localPosition;
                    Instantiate(ropePrefab, test, Quaternion.Euler(0, angleY, 0), ropeParent);
                    angleY = 0f;
                    break;
            }

            yield return new WaitForSeconds(Random.Range(10, 17));
        }
    }
}

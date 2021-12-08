using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleObstacleSpwaner : MonoBehaviour
{
    [Header ("Arrow")]
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private GameObject arrowZone;
    [SerializeField] private float arrowDelayTime;

    [Header("Rock")]
    [SerializeField] private GameObject rockPrefab;
    [SerializeField] private GameObject rockZone;
    [SerializeField] private float rockDelayTime;

    [Header ("Knight")]
    [SerializeField] private GameObject knightPrefab;
    [SerializeField] private GameObject knightSpwaner;
    [SerializeField] private float knightDelayTime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpwanArrow(arrowDelayTime));
        StartCoroutine(SpwanRock(rockDelayTime));
        StartCoroutine(SpwanKnight(knightDelayTime));

    }   

    IEnumerator SpwanArrow(float delaytime)
    {
        while (true)
        {
            float randX = Random.Range(-9f, 9f);
            float randZ = Random.Range(-10f, 61f);
            arrowZone.transform.position = new Vector3(randX, 10f, randZ);

            Instantiate(arrowPrefab, arrowZone.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(delaytime);
        }
    }

    IEnumerator SpwanRock(float delaytime)
    {
        while (true)
        {
            float randX = Random.Range(-7.5f, 9.5f);
            float randZ = Random.Range(75f, 165f);
            rockZone.transform.position = new Vector3(randX, 10f, randZ);

            Instantiate(rockPrefab, rockZone.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(delaytime);
        }
    }

    IEnumerator SpwanKnight(float delaytime){
        while (true)
        {
            float randX = Random.Range(-7.5f, 8.5f);
            knightSpwaner.transform.position = new Vector3(randX, 3.5f, 160f);

            Instantiate(knightPrefab, knightSpwaner.transform.position, Quaternion.Euler(0,180,0));
            yield return new WaitForSeconds(delaytime);
        }
    }

    // void SpwanArrow(){
    //     float randX = Random.Range(-9f,9f);
    //     float randZ = Random.Range(-10f, 61f);

    //     arrowZone.transform.position = new Vector3(randX,10f,randZ);
        
    //     Instantiate(arrowPrefab, arrowZone.transform);
    // }
}

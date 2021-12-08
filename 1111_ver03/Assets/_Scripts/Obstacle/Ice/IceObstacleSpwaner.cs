using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceObstacleSpwaner : MonoBehaviour
{
    [Header ("SnowBall")]
    [SerializeField] private GameObject snowBallPrefab;
    [SerializeField] private GameObject[] snowBallZone;
    [SerializeField] private float snowBallDelayTime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpwanSnowBall(snowBallDelayTime));
    }

    IEnumerator SpwanSnowBall(float delaytime)
    {
        while (true)
        {
            float randX = Random.Range(-9f, 9f);
            float randZ = Random.Range(-10f, 61f);

            int snowBallZoneChoose = Random.Range(0,2);
            snowBallZone[snowBallZoneChoose].transform.position = new Vector3(randX, 10f, randZ);

            Instantiate(snowBallPrefab, snowBallZone[snowBallZoneChoose].transform.position, Quaternion.identity);
            yield return new WaitForSeconds(delaytime);
        }
    }

}

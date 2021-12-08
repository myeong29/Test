using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{
    [SerializeField] private float speed;    
    [SerializeField] private float destroyTime;
    
    private Rigidbody knightRigidbody;

    void Start()
    {
        knightRigidbody = GetComponent<Rigidbody>();
        Destroy(this.gameObject, destroyTime);
    }

    void Update()
    {
        knightRigidbody.AddForce(0, 0, -speed * Time.deltaTime);        
    }
}

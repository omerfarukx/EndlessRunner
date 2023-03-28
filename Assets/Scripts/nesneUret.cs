using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nesneUret : MonoBehaviour
{
    public GameObject nesnePrefab;
    public float sure = 5f; 

    private float sonrakiUretimZamani; 

    void Start()
    {
        sonrakiUretimZamani = Time.time + sure;
    }

    void Update()
    {
        
        if (Time.time >= sonrakiUretimZamani)
        {
            Instantiate(nesnePrefab, transform.position, transform.rotation);
            sonrakiUretimZamani += sure +1f;
        }
    }

}

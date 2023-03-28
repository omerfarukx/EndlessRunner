using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteBackground : MonoBehaviour
{
    public GameObject nesnePrefab, nesnePrefab2;
    public float sure = 5f;

    private float sonrakiUretimZamani;

    void Start()
    {
        sonrakiUretimZamani = Time.time + sure;
    }

    void Update()
    {

        if (GameManager.distance < 45 && Time.time >= sonrakiUretimZamani)
        {
            Instantiate(nesnePrefab, transform.position, transform.rotation);
            sonrakiUretimZamani += sure + 1f;
        }
        else if (GameManager.distance >= 45 && Time.time >= sonrakiUretimZamani)
        {
            Instantiate(nesnePrefab2, transform.position, transform.rotation);
            sonrakiUretimZamani += sure + 1f;
        }
    }
}

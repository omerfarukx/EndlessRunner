using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeminSpawn : MonoBehaviour
{
    public GameObject zeminPrefab;
    public int zeminGenislik;
    public int zeminYukseklik;
    public Vector2 spawnKonumu;

    void Start()
    {
        for (int i = 0; i < zeminGenislik; i++)
        {
            for (int j = 0; j < zeminYukseklik; j++)
            {
                Vector2 spawnPozisyonu = new Vector2(i, j) + spawnKonumu;
                Instantiate(zeminPrefab, spawnPozisyonu, Quaternion.identity);
            }
        }
    }
}

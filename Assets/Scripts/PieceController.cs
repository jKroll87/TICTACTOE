using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceController : MonoBehaviour
{
    public GameObject[] piecePrefabs = new GameObject[2];
    static List<GameObject> OPiecePool;
    static List<GameObject> XPiecePool;
    public int poolSize;

    void Awake()
    {
        ObjectPooling();
    }

    void ObjectPooling()
    {
        if (OPiecePool == null)
        {
            OPiecePool = new List<GameObject>();
        }
        if (XPiecePool == null)
        {
            XPiecePool = new List<GameObject>();
        }

        for (int i = 0; i < poolSize; i++)
        {
            GameObject OPieceObject = Instantiate(piecePrefabs[0]);
            GameObject XPieceObject = Instantiate(piecePrefabs[1]);

            OPieceObject.SetActive(false);
            OPiecePool.Add(OPieceObject);

            XPieceObject.SetActive(false);
            XPiecePool.Add(XPieceObject);
        }
    }
}
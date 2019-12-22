using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public int aliensGridWidth;
    public int aliensGridHeight;
    public float aliensWidthSpacing;
    public float aliensHeightSpacing;

    public GameObject[] aliensPrefabs;
    public Transform aliensGridParent;

    private float gridInitX;
    private float gridInitY;

    // Start is called before the first frame update
    void Start()
    {
        gridInitX = Screen.width - 15f;
        gridInitY = Screen.height / 3f;
        StartCoroutine(InitAliens());
    }

    private IEnumerator InitAliens()
    {
        for (int h = 0; h < aliensGridHeight; h++)
        {
            for (int w = aliensGridWidth; w > 0; w--)
            {
                Vector3 pos = new Vector3(gridInitX + w * aliensWidthSpacing, gridInitY + h * aliensHeightSpacing);
                Instantiate(aliensPrefabs[Random.Range(0, aliensPrefabs.Length)], pos, Quaternion.identity, aliensGridParent);
                yield return new WaitForSeconds(.05f);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

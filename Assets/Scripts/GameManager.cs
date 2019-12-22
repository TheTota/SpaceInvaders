using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    // Grid spawn/layout
    public int aliensGridWidth;
    public int aliensGridHeight;
    public float aliensWidthSpacing;
    public float aliensHeightSpacing;

    public GameObject[] aliensPrefabs;
    public Transform aliensGridParent;

    private float gridInitX;
    private float gridInitY;

    // Gameplay
    public float aliensXMoveDistance = .0001f;

    // Useful infos 
    private List<Alien> livingAliens;

    // Start is called before the first frame update
    void Start()
    {
        livingAliens = new List<Alien>();
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
                GameObject spawnedAlien = Instantiate(aliensPrefabs[Random.Range(0, aliensPrefabs.Length)], pos, Quaternion.identity, aliensGridParent);
                livingAliens.Add(spawnedAlien.GetComponent<Alien>());
                yield return new WaitForSeconds(.085f);
            }
        }

        StartCoroutine(MoveAliens());
    }

    private IEnumerator MoveAliens()
    {
        while (livingAliens.Count != 0)
        {
            foreach (Alien alien in livingAliens)
            {
                alien.transform.Translate(new Vector3(aliensXMoveDistance, 0f, 0f));
                yield return new WaitForSeconds(.5f);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

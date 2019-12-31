using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    [Header("Grid spawn/layout")]
    public int aliensGridWidth;
    public int aliensGridHeight;
    public float aliensWidthSpacing;
    public float aliensHeightSpacing;

    public GameObject[] aliensPrefabs;
    public Transform aliensGridParent;

    private float gridInitX;
    private float gridInitY;

    [Header("Gameplay")]
    public float aliensXMoveDistance = .1f;
    public float aliensYMoveDistance = -1f;
    public float aliensSpeedFactor = 0.01f;

    // Useful infos 
    private List<Alien> livingAliens;
    private Camera mainCam;
    private bool canMoveDown;

    void Awake()
    {
        Instance = this;
        this.mainCam = Camera.main;

        livingAliens = new List<Alien>();
        gridInitX = Screen.width - 15f;
        gridInitY = Screen.height / 2.985f;
        StartCoroutine(InitAliens());

        this.canMoveDown = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            livingAliens[0].gameObject.SetActive(false);
        }
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
            // move down
            if (this.canMoveDown)
            {
                foreach (Alien alien in livingAliens.ToList())
                {
                    alien.SwitchDirection();
                    alien.Translate(new Vector3(0f, aliensYMoveDistance, 0f));
                    yield return new WaitForSeconds(this.livingAliens.Count * aliensSpeedFactor);
                }
                this.canMoveDown = false;
            }

            // move laterally
            foreach (Alien alien in livingAliens.ToList())
            {
                Vector3 alienPosOnScreen = this.mainCam.WorldToScreenPoint(alien.transform.position);
                if (alien.directionFactor == 1f && alienPosOnScreen.x >= Screen.width - (Screen.width / 10f))
                {
                    this.canMoveDown = true;
                }
                else if (alien.directionFactor == -1f && alienPosOnScreen.x <= Screen.width / 10f)
                {
                    this.canMoveDown = true;
                }

                alien.Translate(new Vector3(aliensXMoveDistance * alien.directionFactor, 0f, 0f));
                yield return new WaitForSeconds(this.livingAliens.Count * aliensSpeedFactor);
            }
        }
    }

    public void RemoveAlienFromMgr(Alien a)
    {
        livingAliens.Remove(a);
    }
}

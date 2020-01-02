using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float xMoveSpeed = .05f;
    public float xClampPadding;
    public GameObject bulletPrefab;
    public Transform bulletsParent;
    public Transform canonTip;

    private Camera mainCam;
    private float moveClampMin;
    private float moveClampMax;

    public float secondsBetweenShots;
    private float lastShotTime;
    private float currentTime;

    // Start is called before the first frame update
    void Awake()
    {
        this.mainCam = Camera.main;
        this.moveClampMin = this.mainCam.ScreenToWorldPoint(new Vector3(0f, 0f, 0f)).x + xClampPadding;
        this.moveClampMax = this.mainCam.ScreenToWorldPoint(new Vector3(Screen.width, 0f, 0f)).x - xClampPadding;
    }

    // Update is called once per frame
    void Update()
    {
        this.currentTime += Time.deltaTime;

        if (GameManager.Instance.AliensSpawned)
        {
            // moving
            bool leftInputs = Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.LeftArrow);
            bool rightInputs = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
            if (leftInputs && !rightInputs)
            {
                Move(-xMoveSpeed);
            }
            else if (!leftInputs && rightInputs)
            {
                Move(xMoveSpeed);
            }

            // shooting
            if (Input.GetKey(KeyCode.Space))
            {
                Shoot();
            }
        }
    }

    private void Move(float xSpeed)
    {
        this.transform.Translate(new Vector3(xSpeed, 0f, 0f));
        this.transform.position = new Vector3(Mathf.Clamp(this.transform.position.x, moveClampMin, moveClampMax), this.transform.position.y, this.transform.position.z);
    }

    private void Shoot()
    {
        if (this.currentTime - this.lastShotTime >= this.secondsBetweenShots)
        {
            this.lastShotTime = Time.time;
            GameObject bullet = Instantiate(this.bulletPrefab, this.canonTip.position, Quaternion.identity, this.bulletsParent);
            Bullet b = bullet.GetComponent<Bullet>();
            b.bulletSpeed = .1f;
        }
    }
}

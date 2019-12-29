using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Alien : MonoBehaviour
{
    public Sprite altSprite;
    private Sprite initSprite;
    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Awake()
    {
        this.sr = this.transform.GetComponent<SpriteRenderer>();
        this.initSprite = sr.sprite;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnDestroy()
    {
        GameManager.Instance.RemoveAlienFromMgr(this);
        Debug.Log("TODO: Here play an animation for the death of da boi");
    }

    public void Animate()
    {
        if (this.sr.sprite == this.initSprite)
        {
            this.sr.sprite = this.altSprite;
        }
        else
        {
            this.sr.sprite = this.initSprite;
        }
    }
}

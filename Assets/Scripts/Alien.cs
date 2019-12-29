using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Alien : MonoBehaviour
{
    public Sprite altSprite;
    public float directionFactor; // -1 or 1 to go right or left
    private Sprite initSprite;
    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Awake()
    {
        this.directionFactor = 1f;  

        this.sr = this.transform.GetComponent<SpriteRenderer>();
        this.initSprite = sr.sprite;
    }

    public void Translate(Vector3 v)
    {
        this.transform.Translate(v);
        Animate();
    }

    private void Animate()
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

    public void SwitchDirection()
    {
        this.directionFactor = (this.directionFactor == 1f ? -1f : 1f);
    }

    private void OnDestroy()
    {
        GameManager.Instance.RemoveAlienFromMgr(this);
        Debug.Log("TODO: Here play an animation for the death of da boi");
    }
}

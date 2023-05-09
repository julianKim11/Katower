using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private SpriteRenderer mySpriteRenderer;
    private void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void Select()
    {
        mySpriteRenderer.enabled = !mySpriteRenderer.enabled;
    }
}

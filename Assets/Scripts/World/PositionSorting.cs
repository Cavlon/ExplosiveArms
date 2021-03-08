using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionSorting : MonoBehaviour
{
    [SerializeField]
    private int sortingOrderBase = 2000;
    [SerializeField]
    private int offset = 0;
    [SerializeField]
    private bool runOnce = false;

    private Renderer myRenderer;

    private void Awake()
    {
        myRenderer = GetComponent<Renderer>();
    }

    private void LateUpdate()
    {
        myRenderer.sortingOrder = (int)(sortingOrderBase - (transform.parent.position.y * 2) - offset);
        if (runOnce)
        {
            Destroy(this);
        }
    }
}

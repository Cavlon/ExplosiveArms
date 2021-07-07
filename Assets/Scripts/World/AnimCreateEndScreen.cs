using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimCreateEndScreen : MonoBehaviour
{

    public bool animVariable;

    private CreateEndScreen createEnd;

    private void Awake()
    {
        createEnd = transform.parent.GetComponent<CreateEndScreen>();
    }

    void Update()
    {
        if (animVariable)
        {
            createEnd.SpawnOptions();
            animVariable = false;
        }
    }
}

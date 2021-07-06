using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimCreateOptions : MonoBehaviour
{

    public bool animVariable;

    private CreateOptions createOptions;

    private void Awake()
    {
        createOptions = transform.parent.GetComponent<CreateOptions>();
    }

    void Update()
    {
        if (animVariable)
        {
            createOptions.SpawnOptions();
            animVariable = false;
        }
    }
}

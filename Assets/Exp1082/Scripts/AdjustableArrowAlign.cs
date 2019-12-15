using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustableArrowAlign : MonoBehaviour
{

    void Start()
    {
        
    }
    
    void LateUpdate()
    {
        if (AdjustableManager.selected)
            transform.position = AdjustableManager.selected.transform.position;
    }
}

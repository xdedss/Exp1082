using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

public class AdjustableControl : MonoBehaviour
{
    public Vector2 adjustLimit;
    public Transform tipPanel;
    Outline outline;
    public bool shouldShowTip;

    private void Awake()
    {
        AdjustableManager.adjustables.Add(this);
    }

    void Start()
    {
        outline = GetComponent<Outline>();
    }

    void Update()
    {
        var hit = Raycaster.hitObject == gameObject;
        var selected = AdjustableManager.selected == this;
        outline.enabled = hit || selected;
        outline.color = selected ? 1 : 0;

        if(Input.GetMouseButtonDown(0))
        {
            if (hit)
            {
                AdjustableManager.instance.Select(this);
            }
            else
            {
                if (Raycaster.hitObject == null || Raycaster.hitObject.layer != 10)
                {
                    AdjustableManager.instance.Deselect(this);
                }
            }
        }

        shouldShowTip = hit || selected;
    }

    private void OnDisable()
    {
        AdjustableManager.instance.Deselect(this);
        shouldShowTip = false;
    }
}

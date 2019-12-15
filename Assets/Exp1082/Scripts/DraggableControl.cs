using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

public class DraggableControl : MonoBehaviour
{
    public string tipName;
    public float X => -transform.localPosition.z;

    Outline outline;
    bool dragging = false;
    bool prevHit = false;

    void Start()
    {
        outline = GetComponent<Outline>();
        DraggableManager.instance.draggables.Add(this);
    }

    void Update()
    {
        var hit = Raycaster.hitObject == gameObject;
        outline.enabled = hit || dragging;
        outline.color = dragging ? 1 : 0;

        if ((hit && !prevHit && !AdjustableManager.IsShowingTip) || dragging)
        {
            DraggableManager.instance.ShowDraggableTip(this);
        }
        if (!hit && !dragging && DraggableManager.instance.currentTip == this)
        {
            DraggableManager.instance.HideDraggableTip();
        }
        if (Input.GetMouseButtonDown(1) && hit)
        {
            var c = transform.GetChild(0);
            c.gameObject.SetActive(!c.gameObject.activeInHierarchy);
        }
        if (Input.GetMouseButtonDown(0) && hit)
        {
            dragging = true;
            Raycaster.LockCursor();
        }
        if (Input.GetMouseButtonUp(0))
        {
            dragging = false;
            Raycaster.RestoreCursor();
        }
        if (dragging)
        {
            DraggableManager.instance.TryMove(this, Input.GetAxis("Mouse X") * Time.deltaTime / 4);
        }

        if (!AdjustableManager.IsShowingTip)
        {
            prevHit = hit;
        }
    }
}

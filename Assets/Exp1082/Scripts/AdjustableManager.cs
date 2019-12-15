using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class AdjustableManager : MonoBehaviour
{
    public static AdjustableManager instance;
    public static AdjustableControl selected;
    public static List<AdjustableControl> adjustables = new List<AdjustableControl>();
    public static bool IsShowingTip = false;

    public Transform arrows;

    public void Select(AdjustableControl me)
    {
        selected = me;
        ShowArrows(me);
    }

    public void Deselect(AdjustableControl me)
    {
        if(selected == me)
        {
            selected = null;
            HideArrows();
        }
    }

    public void TryMove(AdjustableControl me, Vector2 move)
    {
        Vector3 res = (Vector3)move * Mathf.Min(me.adjustLimit.x, me.adjustLimit.y) * 1f + me.transform.localPosition;
        res.x = Mathf.Clamp(res.x, -me.adjustLimit.x, me.adjustLimit.x);
        res.y = Mathf.Clamp(res.y, -me.adjustLimit.y, me.adjustLimit.y);
        me.transform.localPosition = res;
    }

    private void ShowArrows(AdjustableControl me)
    {
        if(me.adjustLimit.sqrMagnitude > 0)
        {
            arrows.gameObject.SetActive(true);
        }
        else
        {
            HideArrows();
        }
    }

    private void HideArrows()
    {
        arrows.gameObject.SetActive(false);
    }

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        foreach(var adjustable in adjustables)
        {
            TryMove(adjustable, new Vector2((Random.value - 0.5f) * 2, (Random.value - 0.5f) * 2));
        }
    }
    
    void Update()
    {
        IsShowingTip = false;
        if (selected)
        {
            IsShowingTip = true;
            foreach (var adjustable in adjustables)
            {
                adjustable.tipPanel.gameObject.SetActive(adjustable == selected);
            }
        }
        else
        {
            foreach (var adjustable in adjustables)
            {
                IsShowingTip |= adjustable.shouldShowTip;
                adjustable.tipPanel.gameObject.SetActive(adjustable.shouldShowTip);
            }
        }
    }
}

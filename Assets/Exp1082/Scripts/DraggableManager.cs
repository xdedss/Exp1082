using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DraggableManager : MonoBehaviour
{
    public static DraggableManager instance;
    public List<DraggableControl> draggables = new List<DraggableControl>();

    public Transform tipPanel;
    public DraggableControl currentTip;
    Text tipText;
    Text infoText;
    Text titleText;

    public void TryMove(DraggableControl me, float dz)
    {
        float maxz = 0f;
        float minz = -1.5f;
        float myz = me.transform.localPosition.z;
        foreach(var draggable in draggables)
        {
            if (draggable == me) continue;
            var thisz = draggable.transform.localPosition.z;
            if(thisz > myz)
            {
                if(thisz - 0.04f < maxz)
                {
                    maxz = thisz - 0.04f;
                }
            }
            else
            {
                if(thisz + 0.04f > minz)
                {
                    minz = thisz + 0.04f;
                }
            }
        }
        me.transform.localPosition = new Vector3(0, 0, Mathf.Clamp(myz + dz, minz, maxz));
    }

    public void ShowDraggableTip(DraggableControl me)
    {
        currentTip = me;
        tipPanel.gameObject.SetActive(true);
        titleText.text = me.tipName;
        infoText.text = string.Format("x = {0:0.00} cm", me.X * 100);
    }

    
    public void HideDraggableTip(/*迷惑中*/)
    {
        currentTip = null;
        tipPanel.gameObject.SetActive(false);
    }

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        tipText = tipPanel.Find("tip").GetComponent<Text>();
        titleText = tipPanel.Find("title").GetComponent<Text>();
        infoText = tipPanel.Find("info").GetComponent<Text>();
    }
    
    void Update()
    {
        
    }
}

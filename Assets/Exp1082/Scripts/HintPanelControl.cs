using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintPanelControl : MonoBehaviour
{
    public List<Transform> hints = new List<Transform>();
    public Text pageNum;
    public int currentIndex = 0;

    public void NextHint()
    {
        SwitchTo(currentIndex + 1);
    }
    public void PrevHint()
    {
        SwitchTo(currentIndex - 1);
    }

    public void SwitchTo(int index)
    {
        if (index < 0) index = 0;
        if (index >= hints.Count) index = hints.Count - 1;
        currentIndex = index;
        Refresh();
    }

    void Refresh()
    {
        for (int i = 0; i < hints.Count; i++)
        {
            hints[i].gameObject.SetActive(i == currentIndex);
        }
        pageNum.text = string.Format("{0}/{1}", currentIndex + 1, hints.Count);
    }

    void Start()
    {
        for(int i = 0; i < transform.childCount - 1; i++)
        {
            hints.Add(transform.GetChild(i));
        }
        Refresh();
    }
    
    void Update()
    {
        
    }
}

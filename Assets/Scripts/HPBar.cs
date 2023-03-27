using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    //public int maxHP, hp;
    public Image hpBar;
    public Image maxHPBar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetHP(int value, int maxValue)
    {
        
        float maxWidth = maxHPBar.GetComponent<RectTransform>().rect.width;
        float maxHeight = maxHPBar.GetComponent<RectTransform>().rect.height;

        float currentWidth = (float)value / (float)maxValue * maxWidth;
        float currentHeight = maxHeight;

        //Current HP Bar Size
        hpBar.GetComponent<RectTransform>().sizeDelta = new Vector2(currentWidth, currentHeight);

        //Current Hp Bar Position
        float x = -(maxHPBar.GetComponent<RectTransform>().rect.width - hpBar.GetComponent<RectTransform>().rect.width)/2;
        hpBar.transform.localPosition = new Vector2(x, 0);
    }
}

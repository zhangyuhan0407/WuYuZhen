using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TDHeroSlot : MonoBehaviour
{

    public HeroIcon hero;

    Text textCost;

    // Start is called before the first frame update
    void Start()
    {
        textCost = GetComponentInChildren<Text>();
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Accept(HeroIcon hero)
    {
        this.hero = hero;
        UpdateUI();
    }

    public bool HasHero()
    {
        return this.hero != null;
    }


    public void UpdateUI()
    {
        if(HasHero() == false)
        {
            GetComponent<Image>().CrossFadeAlpha(0.3f, 1, true);
            textCost.gameObject.transform.parent.gameObject.SetActive(false);
            return;
        }

        hero.gameObject.SetActive(true);
        GetComponent<Image>().CrossFadeAlpha(1.0f, 1, true);
        textCost.gameObject.transform.parent.gameObject.SetActive(true);
        hero.transform.SetParent(transform);
        hero.transform.localPosition = Vector3.zero;
        textCost.text = hero.cost + "";
    }

}

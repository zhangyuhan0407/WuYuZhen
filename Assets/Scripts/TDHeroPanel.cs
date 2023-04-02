using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TDHeroPanel : MonoBehaviour
{

    public List<TDHeroSlot> slots;

    public GameObject prefabHero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }



    public void Accept(List<HeroIcon> heroes)
    {
        foreach(var hero in heroes)
        {
            AddHero(hero);
        }
    }



    public void AddHero(HeroIcon hero)
    {
        foreach(var slot in this.slots)
        {
            if(slot.HasHero() == false)
            {
                slot.Accept(hero);
                return;
            }
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDPlayerSystem : MonoBehaviour
{

    public static TDPlayerSystem Instance;

    private void Awake()
    {
        Instance = this;
    }

    public int gold;
    public int exp;

    public List<HeroIcon> heroes;
    public List<string> heroNames;


    // Start is called before the first frame update
    void Start()
    {
        heroes = new List<HeroIcon>();
        GenerateHeroes();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void GenerateHeroes()
    {
        foreach(var name in heroNames)
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/HeroAvatars/" + name);
            GameObject hero = Instantiate(prefab);
            hero.gameObject.SetActive(false);
            this.heroes.Add(hero.GetComponent<HeroIcon>());
        }

    }



}

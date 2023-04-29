using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TDShopPanel : MonoBehaviour, IPointerClickHandler
{
    //战士、骑士、猎人、法师、  刺客、亡灵术士、牧师、德鲁伊、萨满祭司、地精、
    // Start is called before the first frame update


    //出售的干员
    public List<HeroIcon> heroesOnSell;

    //选中的干员
    public HeroIcon selectingHero;
    //选中干员的边框
    public Image selectingBorder;




    /// <summary>
    /// //功能：
    /// 1、选择英雄
    /// 2、购买英雄
    /// 3、刷新商店出售中的英雄
    /// 4、刷新界面（UpdateUI）
    /// </summary>


    void Start()
    {
        RefreshShop();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void UpdateUI()
    {
        //排列出售的干员
        float x = -500;
        foreach (var hero in this.heroesOnSell)
        {
            hero.transform.SetParent(transform);
            hero.transform.localPosition = new Vector2(x, 200);
            x += 200;
        }

        //排列玩家已拥有的干员
        x = -300;
        foreach (var hero in TDPlayerSystem.Instance.heroes)
        {
            hero.transform.SetParent(transform);
            hero.transform.localPosition = new Vector2(x, -200);
            hero.gameObject.SetActive(true);
            x += 200;
        }

        //设置选中干员Border位置
        if(this.selectingHero != null)
        {
            this.selectingBorder.gameObject.SetActive(true);
            this.selectingBorder.transform.position = this.selectingHero.transform.position;
            //设置为层级中最下面一层；最下面一层显示在最上面
            this.selectingBorder.transform.SetAsLastSibling();
        }
        else
        {
            this.selectingBorder.gameObject.SetActive(false);
        }
    }


    //当鼠标点击界面时，调用该函数
    public void OnPointerClick(PointerEventData eventData)
    {
        float x = eventData.position.x;
        float y = eventData.position.y;
        foreach (var hero in heroesOnSell)
        {
            Rect rect = hero.GetComponent<RectTransform>().rect;
            float minX = hero.transform.position.x - rect.width / 2;
            float maxX = hero.transform.position.x + rect.width / 2;
            float minY = hero.transform.position.y - rect.height / 2;
            float maxY = hero.transform.position.y + rect.height / 2;
            if (x > minX && x < maxX && y > minY && y < maxY)
            {
                selectingHero = hero;
                break;  //跳出for循环；选中一个英雄，不可能选中第二个英雄
            }
        }

        UpdateUI();
    }


    public void BuyHero()
    {
        this.heroesOnSell.Remove(selectingHero);
        TDPlayerSystem.Instance.heroes.Add(selectingHero);
        selectingHero = null;
        UpdateUI();
    }


    public void RefreshShop()
    {
        //清空再出售的干员
        for(int i =0;i< heroesOnSell.Count;i++)
        {
            Destroy(heroesOnSell[i].gameObject);
        }

        heroesOnSell = new List<HeroIcon>();

        int requiredHeroNumber = 3;

        //随机3次
        for (int i = 0; i < requiredHeroNumber; i++)
        {
            int index = Random.Range(0, 3);     //随机出0，1，2（半开半闭区间）
            if (index == 0)
            {
                heroesOnSell.Add(HeroIcon.Create("HeroIcon_Warrior"));
            }
            else if (index == 1)
            {
                heroesOnSell.Add(HeroIcon.Create("HeroIcon_Hunter"));
            }
            else if (index == 2)
            {
                heroesOnSell.Add(HeroIcon.Create("HeroIcon_Master"));
            }
        }

        UpdateUI();

    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TDShopPanel : MonoBehaviour, IPointerClickHandler
{
    //սʿ����ʿ�����ˡ���ʦ��  �̿͡�������ʿ����ʦ����³����������˾���ؾ���
    // Start is called before the first frame update


    //���۵ĸ�Ա
    public List<HeroIcon> heroesOnSell;

    //ѡ�еĸ�Ա
    public HeroIcon selectingHero;
    //ѡ�и�Ա�ı߿�
    public Image selectingBorder;




    /// <summary>
    /// //���ܣ�
    /// 1��ѡ��Ӣ��
    /// 2������Ӣ��
    /// 3��ˢ���̵�����е�Ӣ��
    /// 4��ˢ�½��棨UpdateUI��
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
        //���г��۵ĸ�Ա
        float x = -500;
        foreach (var hero in this.heroesOnSell)
        {
            hero.transform.SetParent(transform);
            hero.transform.localPosition = new Vector2(x, 200);
            x += 200;
        }

        //���������ӵ�еĸ�Ա
        x = -300;
        foreach (var hero in TDPlayerSystem.Instance.heroes)
        {
            hero.transform.SetParent(transform);
            hero.transform.localPosition = new Vector2(x, -200);
            hero.gameObject.SetActive(true);
            x += 200;
        }

        //����ѡ�и�ԱBorderλ��
        if(this.selectingHero != null)
        {
            this.selectingBorder.gameObject.SetActive(true);
            this.selectingBorder.transform.position = this.selectingHero.transform.position;
            //����Ϊ�㼶��������һ�㣻������һ����ʾ��������
            this.selectingBorder.transform.SetAsLastSibling();
        }
        else
        {
            this.selectingBorder.gameObject.SetActive(false);
        }
    }


    //�����������ʱ�����øú���
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
                break;  //����forѭ����ѡ��һ��Ӣ�ۣ�������ѡ�еڶ���Ӣ��
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
        //����ٳ��۵ĸ�Ա
        for(int i =0;i< heroesOnSell.Count;i++)
        {
            Destroy(heroesOnSell[i].gameObject);
        }

        heroesOnSell = new List<HeroIcon>();

        int requiredHeroNumber = 3;

        //���3��
        for (int i = 0; i < requiredHeroNumber; i++)
        {
            int index = Random.Range(0, 3);     //�����0��1��2���뿪������䣩
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

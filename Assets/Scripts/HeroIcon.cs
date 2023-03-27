using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HeroIcon : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{

    public Vector2 originalPosition;
    public string heroName;
    public int cost;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (CheckInAvailable())
        {
            HintManager.Instance.ShowHintText("Not Enough Ap");
            return;
        }

        originalPosition = transform.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (CheckInAvailable())
        {
            return;
        }
        //Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 pos = Input.mousePosition;
        Cell cell = Map.instanse.FindCellAtPosition(pos.x, pos.y);
        if(cell == null)
        {
            PutBack();
            return;
        }
        else
        {
            DropHero();
            TDGameManager.instance.cost -= this.cost;
            transform.position = cell.transform.position;
            Debug.Log(cell.id);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (CheckInAvailable())
        {
            return;
        }

        transform.position = Input.mousePosition;
    }


    public bool CheckInAvailable()
    {
        //Not Enough AP
        if (this.cost < TDGameManager.instance.cost)
        {
            return false;
        }
        return true;
    }

    public void PutBack()
    {
        //Wrong Position(out of map)(existing other units)

        transform.position = originalPosition;
    }


    public void DropHero()
    {
        //reduce ap
        //cell accep this hero
        //destroy this hero
        GameObject prefeb = Resources.Load<GameObject>("Prefabs/Heroes/" + heroName);
        GameObject hero = Instantiate(prefeb);
        hero.transform.SetParent(this.transform.parent);
        hero.transform.position = this.transform.position;
        Destroy(gameObject);
    }


}

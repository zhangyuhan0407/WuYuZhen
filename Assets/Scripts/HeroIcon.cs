using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HeroIcon : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{

    public Vector2 originalPosition;
    public string heroName;
    public int cost;

    Cell tempCell;      //上一个高亮的Cell
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

        if(tempCell!= null)
        {
            tempCell.Highlight(Color.white);
            tempCell = null;
        }
        
        Vector2 pos = Input.mousePosition;
        Cell cell = Map.instanse.FindCellAtPosition(pos.x, pos.y);
        if (cell == null)
        {
            PutBack();
            return;
        }
        else
        {
            DropHero(cell) ;
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

        var cell = Map.instanse.FindCellAtPosition(Input.mousePosition.x, Input.mousePosition.y);
        if (tempCell != null && tempCell != cell)
        {     
            tempCell.Highlight(Color.white); 
        }

        if (cell != null)
        {
            tempCell = cell;
            cell.Highlight(Color.cyan);
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


    public void DropHero(Cell cell)
    {
        //reduce ap
        //cell accep this hero
        //destroy this hero
        GameObject prefeb = Resources.Load<GameObject>("Prefabs/Heroes/" + heroName);
        GameObject hero = Instantiate(prefeb);
        hero.transform.SetParent(cell.transform);
        hero.transform.localPosition = Vector2.zero + new Vector2(0,25);

        Destroy(gameObject);
    }



    public static HeroIcon Create(string key)
    {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/HeroAvatars/" + key);
        GameObject hero = Instantiate(prefab);
        return hero.GetComponent<HeroIcon>();
    }


}

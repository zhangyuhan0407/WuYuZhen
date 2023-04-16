using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    //singletance danli moshi 
    public static Map instanse;
    private void Awake()
    {
        instanse = this;
    }
    public GameObject cellprefab;
    public int row = 5;
    public int col = 7;
    public List<Cell> cells;
    // Start is called before the first frame update
    void Start()
    {
        cells= new List<Cell>();
        for (int i = 0; i < col; i++)
        {
            for (int j = 0; j < row; j++)
            {
                GameObject c = Instantiate(cellprefab);
                Cell cell = c.GetComponent<Cell>();
                cell.id = i * row + j;
                cell.transform.SetParent(transform);
                cell.name = "cell" + cell.id;
                cell.transform.localPosition = new Vector2(-1050/2 + 150 * i + 75, -750/2 + 150 * j + 75);
                cells.Add(cell);
            }
        }

        this.GetComponent<RectTransform>().sizeDelta = new Vector2(7 * 150, 5 * 150);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public Cell FindCellAtPosition(float x, float y)
    {
        foreach(var cell in cells)
        {
            Rect rect = cell.GetComponent<RectTransform>().rect;
            float minX = cell.transform.position.x - rect.width / 2;
            float maxX = cell.transform.position.x + rect.width / 2;
            float minY = cell.transform.position.y - rect.height / 2;
            float maxY = cell.transform.position.y + rect.height / 2;
            if (x >minX && x < maxX && y > minY && y < maxY) 
            {
                return cell;
            }
        }

        Debug.Log("return null");
        return null;
    }

}

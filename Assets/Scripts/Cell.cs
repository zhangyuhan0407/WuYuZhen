using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    public TDOperatorAnimation player;
    public EnemyController enemy;
    public int id;
    public Text t;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        t.text = id+"";
    }
    public void Acceptplayer(TDOperatorAnimation p)
    {
        Debug.Log("abc");
        if (IsEmpty()) 
        {
            Debug.Log("xyz");
            player = p;
            player.transform.SetParent(transform);
            player.transform.localPosition = new Vector3(60,60,60);
        }
    }
    public void Acceptenemy(EnemyController e)
    {
        if (IsEmpty())
        {
            enemy = e;
            enemy.transform.SetParent(transform);
        }
    }
    public bool IsEmpty()
    {
        return player == null && enemy == null;
    }
    public bool Hasplayer()
    {
        return player != null;
    }
    public bool Hasenemy()
    {
        return enemy != null;
    }


    public void Highlight(Color color)
    {
        this.GetComponent<Image>().color = color;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static HintManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    public Text hintText;
    public float disappearTime;
    void Start()
    {
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        disappearTime -= Time.deltaTime;
        if (disappearTime < 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    public void ShowHintText(string s)
    {
        this.gameObject.SetActive(true);
        hintText.text = s;
        disappearTime = 1;
    }
}

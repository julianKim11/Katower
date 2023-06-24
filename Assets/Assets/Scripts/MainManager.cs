using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;
    public GameObject profilePanel;
    public Text yarmText;
    private int yarm = 10;

    public int Yarm
    {
        get
        {
            return yarm;
        }
        set
        {
            this.yarm = value;
        }
    }
    private void Awake()
    {
        if (MainManager.Instance == null)
        {
            
            MainManager.Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        yarmText.text = yarm.ToString();
    }
    public void WinYarm(int amount)
    {
        Debug.Log("ganaste 2 estambres");
        yarm += amount;
    }
}
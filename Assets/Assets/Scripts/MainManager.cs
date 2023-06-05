using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public static MainManager instance;
    [SerializeField] public int estambre = 0;
    public GameObject tower;
    public GameObject ProfilePanel;
    public Text estambreText;
    //public bool InGame;

    private void Awake()
    {
        //InGame = true;
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            //if (InGame)
            //{
            //    ProfilePanel.SetActive(true);
            //}
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        estambreText.text = estambre.ToString();
    }
    
}

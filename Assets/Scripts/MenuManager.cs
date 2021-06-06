using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Transform menu;
    [SerializeField] private Transform credit;
    [SerializeField] private Transform tuto;

    private void Start()
    {
        menu.gameObject.SetActive(true);
        credit.gameObject.SetActive(false);
        tuto.gameObject.SetActive(false);
    }

    public void ActiveUI(string s)
    {
        menu.gameObject.SetActive(false);
        credit.gameObject.SetActive(false);
        tuto.gameObject.SetActive(false);
        switch (s)
        {
            case "menu":
                menu.gameObject.SetActive(true);
                break;
            case "credit":
                credit.gameObject.SetActive(true);
                break;
            case "tuto":
                tuto.gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }
    public void GoToLevel(int id)
    {
        SceneManager.LoadScene(id);
    }

    public void Exit()
    {
        Application.Quit(0);
    }


}

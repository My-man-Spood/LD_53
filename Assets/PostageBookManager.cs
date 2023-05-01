using System.Collections.Generic;
using UnityEngine;

public class PostageBookManager : MonoBehaviour
{
    public GameObject bookButton;
    public List<GameObject> pages;
    public int page = 0;

    public void NextPage()
    {
        if (page < pages.Count - 1)
        {
            page++;
        }
        else
        {
            page = 0;
        }
        UpdatePage();
    }

    public void PreviousPage()
    {
        if (page > 0)
        {
            page--;
        }
        else
        {
            page = pages.Count - 1;
        }
        UpdatePage();
    }

    void UpdatePage()
    {
        foreach (var page in pages)
        {
            page.SetActive(false);
        }

        print("open page: " + page);
        pages[page].SetActive(true);
    }

    public void OpenBook()
    {
        UpdatePage();
        bookButton.SetActive(false);
    }

    public void CloseBook()
    {
        foreach (var page in pages)
        {
            page.SetActive(false);
        }
        bookButton.SetActive(true);
    }
}

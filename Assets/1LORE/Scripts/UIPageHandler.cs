using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIPageHandler : MonoBehaviour
{
    public Image[] pageContainers; // Array to hold the containers for the pages
    public Sprite[] pages; // Array of sprites representing the pages
    private int currentPageIndex = 0;
    public GameObject book;
    public AudioSource auds;
    private void Start()
    {
        UpdatePageContainers();        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            ToggleBookDisplay();
            
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseBook();
        }
    }

    public void NextPages()
    {
        currentPageIndex += 2;
        if (currentPageIndex >= pages.Length)
        {
            currentPageIndex = pages.Length - 2;
        }
        UpdatePageContainers();
    }

    public void PreviousPages()
    {
        currentPageIndex -= 2;
        if (currentPageIndex < 0)
        {
            currentPageIndex = 0;
        }
        UpdatePageContainers();
    }

    private void UpdatePageContainers()
    {
        for (int i = 0; i < pageContainers.Length; i++)
        {
            int pageIndex = currentPageIndex + i;
            if (pageIndex < pages.Length)
            {
                pageContainers[i].sprite = pages[pageIndex];
                pageContainers[i].gameObject.SetActive(true);
                auds.Play();
            }
            else
            {
                pageContainers[i].gameObject.SetActive(false);
            }
        }
        
    }

    private void ToggleBookDisplay()
    {
        book.SetActive(!book.activeSelf);
        if (book.activeSelf)
        {
            UpdatePageContainers();
        }
    }

    private void CloseBook()
    {
        book.SetActive(false);
    }
}

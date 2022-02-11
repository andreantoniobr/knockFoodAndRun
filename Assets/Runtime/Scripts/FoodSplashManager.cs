using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSplashManager : MonoBehaviour
{
    [SerializeField] private GameObject splash;

    private static FoodSplashManager instance;
    public static FoodSplashManager Instance => instance;

    private void Awake()
    {
        SetThisInstance();
    }

    private void SetThisInstance()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void InstantiateSplash(Vector3 position)
    {
        if (splash)
        {
            GameObject currentSplash = Instantiate(splash, transform.parent);
            if (currentSplash)
            {
                currentSplash.transform.position = position;
            }
        }
    }
}

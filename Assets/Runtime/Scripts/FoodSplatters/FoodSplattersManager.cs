using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSplattersManager : MonoBehaviour
{
    [SerializeField] private FoodSplatter[] foodSplatters;
    [SerializeField] private FoodSplatter[] foodSplattersEffects;
    [SerializeField] private float foodSplatterTimeToDestroy = 2f;

    private static FoodSplattersManager instance;
    public static FoodSplattersManager Instance => instance;

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

    private void InstantiateRandomFoodSplater(Vector3 location, FoodSplatter[] foodSplatters)
    {
        int foodSplattersAmount = foodSplatters.Length;
        int foodSplatterRandomIndex = Random.Range(0, foodSplattersAmount);
        if (foodSplattersAmount > 0 && InFoodSplatersRange(foodSplattersAmount, foodSplatterRandomIndex))
        {
            InstantiateFoodSplatter(location, foodSplatters, foodSplatterRandomIndex);
        }
    }

    private void InstantiateFoodSplatter(Vector3 location, FoodSplatter[] foodSplatters, int foodSplatterRandomIndex)
    {
        FoodSplatter foodSplatterObject = Instantiate(foodSplatters[foodSplatterRandomIndex], transform.parent);
        if (foodSplatterObject)
        {
            foodSplatterObject.transform.position = location;
            RotateFoodSplatter(location, foodSplatterObject);
            SetTimeToDestroyFoodSplatter(foodSplatterObject);
        }
    }

    private void SetTimeToDestroyFoodSplatter(FoodSplatter foodSplatterObject)
    {
        Destroy(foodSplatterObject.gameObject, foodSplatterTimeToDestroy);
    }

    private void RotateFoodSplatter(Vector3 location, FoodSplatter foodSplatterObject)
    {
        int angle = Random.Range(0, 359);
        foodSplatterObject.transform.RotateAround(location, Vector3.up, angle);
    }

    private bool InFoodSplatersRange(int foodSplattersAmount, int foodSplatterRandomIndex)
    {
        return  foodSplatterRandomIndex >= 0 && foodSplatterRandomIndex <= foodSplattersAmount - 1;
    }

    public void InstantiateRandomFoodSplatter(Vector3 location)
    {
        InstantiateRandomFoodSplater(location, foodSplatters);
        InstantiateRandomFoodSplater(location, foodSplattersEffects);
    }    
}

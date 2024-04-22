using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlessingsDesignationController : MonoBehaviour
{
    [SerializeField] InfiniteScroll _penguinPowerInfiniteScroll;
    [SerializeField] InfiniteScroll _polarBearPowerInfiniteScroll;

    [SerializeField] private Power_SO[] _penguinPowersArray;
    [SerializeField] private Power[] _polarBearPowersArray;

    [SerializeField] private GameObject _blessingsDesignationPanel;

    private void Start()
    {
        
    }
}
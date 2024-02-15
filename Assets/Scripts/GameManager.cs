 using System;
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool isgameOver;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && isgameOver == true)
        {
        }
    }
}

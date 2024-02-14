using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    [SerializeField]
    private Text scoretext;

    [SerializeField] 
    private Sprite[] Livesprites;
    
    
    [SerializeField]
    private Image Livesimg;

    [SerializeField] 
    private Text gameoverText;
    
    [SerializeField]
    private Text RestartText;
    // Start is called before the first frame update
    void Start()
    {
        scoretext.text = "Score: ";
        gameoverText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updatescore(int playerscore)
    {
        scoretext.text = "Score:" + playerscore.ToString();
    }

    public void updatelives(int currentLives)

    {
        Livesimg.sprite = Livesprites[currentLives];
        if (currentLives == 0)
        {
            GameOverSequence();
        }
        
    }

    void GameOverSequence()
    {
        gameoverText.gameObject.SetActive(true);
        RestartText.gameObject.SetActive(true);
        StartCoroutine(GameoverFlickerRoutine());
    }

    IEnumerator GameoverFlickerRoutine()
    {
        while (true)
        {
            gameoverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            gameoverText.text = "";
            yield return new WaitForSeconds(0.5f);


        }
    }
}

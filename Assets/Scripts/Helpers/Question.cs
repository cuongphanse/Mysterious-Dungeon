using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Question : MonoBehaviour
{
    [SerializeField] GameObject questionCheck;
    [SerializeField] GameObject question;
    [SerializeField] GameObject questionItem;
    [SerializeField] PlayerInfo playerInfo;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void acceptedAnswer()
    {
        questionCheck.SetActive(false);
        question.SetActive(true);
         MenuBehavior.isPaused = true;
    }

    public void rejectedAnswer()
    {
        questionCheck.SetActive(false);
        questionItem.SetActive(false);
        MenuBehavior.isPaused = false;

    }

    public void correctAnswer()
    {
        playerInfo.Coin += 100;
    }

    public void incorrectAnswer()
    {
        playerInfo.Coin -= 100;
    }

}

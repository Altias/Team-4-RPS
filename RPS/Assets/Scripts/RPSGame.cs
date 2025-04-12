using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RPSGame : MonoBehaviour
{
	[SerializeField] RPSChecker playerHand;
	[SerializeField] Button goBtn;
	[SerializeField] Button replayBtn;
	[SerializeField] RawImage rock;
	[SerializeField] RawImage paper;
	[SerializeField] RawImage scissors;
	[SerializeField] TextMeshProUGUI resultDisplay;
	[SerializeField] GameObject resultBg;
	
	RawImage[] choices;
	
	string playerGesture;
	int result;
	
    // Start is called before the first frame update
    void Start()
    {
        goBtn.onClick.AddListener(PlayRound);
		replayBtn.onClick.AddListener(ResetGame);
		
		choices = new []{rock,paper,scissors};
    }

    // Update is called once per frame
    void Update()
    {
		
    }
	
	void PlayRound()
	{
		int aiChoice = Random.Range(0,3); //0 for Rock, 1 for paper, 2 for scissors
		
		playerHand.lockIn(); //Lock in current gesture
		
		playerGesture = playerHand.getGesture();
		Debug.Log("Player chose " + playerGesture);
		
		if (playerGesture != "None")
		{
			goBtn.gameObject.SetActive(false);
			replayBtn.gameObject.SetActive(true);
			
			choices[aiChoice].gameObject.SetActive(true);
		
			if (playerGesture == "Rock")
			{
				if (aiChoice == 0) //Rock vs Rock
				{
					result = 2;
				}
				else if (aiChoice == 1) //Rock vs Paper
				{
					result = 1;
				}
				else //Rock vs Scissors
				{
					result = 0;
				}
			}
			else if (playerGesture == "Scissors")
			{
				if (aiChoice == 0) //Scissors vs Rock
				{
					result = 1;
				}
				else if (aiChoice == 1) //Scissors vs Paper
				{
					result = 0;
				}
				else //Scissors vs Scissors
				{
					result = 2;
				}
			}
			else if (playerGesture == "Paper")
			{
				if (aiChoice == 0) //Paper vs Rock
				{
					result = 0;
				}
				else if (aiChoice == 1) //Paper vs Paper
				{
					result = 2;
				}
				else //Paper vs Scissors
				{
					result = 1;
				}
			}
			
			if(result == 0) //Win
			{
				resultDisplay.text = "You Win!";
				resultDisplay.color = Color.green;
			}
			else if (result == 1) //Loss
			{
				resultDisplay.text = "You Lose!";
				resultDisplay.color = Color.red;
			}
			else //Draw
			{
				resultDisplay.text = "Draw!";
				resultDisplay.color = Color.yellow;
			}
			
			resultBg.SetActive(true);
			resultDisplay.gameObject.SetActive(true);
			
			
		}
		else
		{
			Debug.Log("Can't play with no gesture!");
			ResetGame();
		}
		
	}
	
	void ResetGame()
	{
		playerHand.lockIn(); //Unlock gesture
		
		foreach(RawImage i in choices)
		{
			i.gameObject.SetActive(false);
		}
		
		goBtn.gameObject.SetActive(true);
		replayBtn.gameObject.SetActive(false);
		resultBg.SetActive(false);
		resultDisplay.gameObject.SetActive(false);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RPSGame : MonoBehaviour
{
	[SerializeField] RPSChecker playerHand;
	[SerializeField] Button goBtn;
	string playerGesture;
	
    // Start is called before the first frame update
    void Start()
    {
        goBtn.onClick.AddListener(PlayRound);
    }

    // Update is called once per frame
    void Update()
    {
        playerGesture = playerHand.getGesture();
		
		//Debug.Log("Gesture: " + playerGesture);
    }
	
	void PlayRound()
	{
		Debug.Log("Go");
	}
}

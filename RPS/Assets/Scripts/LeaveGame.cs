using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  

public class LeaveGame : MonoBehaviour
{
	public Button quitButton;
	
	void Start()
	{
		quitButton.onClick.AddListener(ToMainMenu);
	}
    void ToMainMenu()
	{
		SceneManager.LoadScene("MenuScene");
	}
}

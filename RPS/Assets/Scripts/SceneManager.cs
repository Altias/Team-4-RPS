using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;  

//Side note. When creating a large project with multiple scenes you may want to break your scripts folder down into more subfolders for each scene. I did not because this is the lamest most basic menu to ever exist.
public class StartGame : MonoBehaviour
{
	public Button startButton; //Drag and drop the button to this script from the game scene.
    // Start is called before the first frame update
	public Button quitButton;
	
    void Start()
    {
        Button btn = startButton.GetComponent<Button>();
		btn.onClick.AddListener(Game); //Add a listener waiting to call the Game function when the button is pressed.
		
		quitButton.onClick.AddListener(Quit);
    }
	
	void Game()
	{
		SceneManager.LoadScene("GameScene"); //Change scenes to take the player to the game.
		
		//Challenge: Create a scene for when the game is won or lost and take the player there when it happens. Whatever conditions you decide trigger this can do so
	}
	
	void Quit()
	{
		Application.Quit();
	}
}

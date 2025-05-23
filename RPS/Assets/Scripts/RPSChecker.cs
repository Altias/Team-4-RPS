using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Klak.TestTools;
using MediaPipe.HandPose;

public class RPSChecker : MonoBehaviour
{
	[SerializeField] ResourceSet _resources = null;
	[SerializeField] ImageSource _source = null;
	[SerializeField] bool _useAsyncReadback = true;
	[SerializeField] RawImage rock;
	[SerializeField] RawImage paper;
	[SerializeField] RawImage scissors;
	
	
	HandPipeline _pipeline;
	
	private Vector3[] thumb;
	private Vector3[] index;
	private Vector3[] middle;
	private Vector3[] ring;
	private Vector3[] pinky;
	
	private int extended;
	private bool lockedIn;
	
    // Start is called before the first frame update
    void Start()
    {
		_pipeline = new HandPipeline(_resources);
		
		extended = 0;
		lockedIn = false;
		
	    thumb = new []{_pipeline.GetKeyPoint(1),_pipeline.GetKeyPoint(2),_pipeline.GetKeyPoint(3),_pipeline.GetKeyPoint(4)};
		index = new []{_pipeline.GetKeyPoint(5),_pipeline.GetKeyPoint(6),_pipeline.GetKeyPoint(7),_pipeline.GetKeyPoint(8)};
		middle = new []{_pipeline.GetKeyPoint(9),_pipeline.GetKeyPoint(10),_pipeline.GetKeyPoint(11),_pipeline.GetKeyPoint(12)};
		ring = new []{_pipeline.GetKeyPoint(13),_pipeline.GetKeyPoint(14),_pipeline.GetKeyPoint(15),_pipeline.GetKeyPoint(16)};
		pinky = new []{_pipeline.GetKeyPoint(17),_pipeline.GetKeyPoint(18),_pipeline.GetKeyPoint(19),_pipeline.GetKeyPoint(20)};
    }

    // Update is called once per frame
    void Update()
    {
        _pipeline.UseAsyncReadback = _useAsyncReadback;
        _pipeline.ProcessImage(_source.Texture);
		
		if (!lockedIn)
		{
		
			//Thumb update
			for(int i = 0; i < 4; i++)
			{
				thumb[i] = _pipeline.GetKeyPoint(i + 1);
			}
		
			//Index update
			for(int i = 0; i < 4; i++)
			{
				index[i] = _pipeline.GetKeyPoint(i + 5);
			}
		
			//Middle update
			for(int i = 0; i < 4; i++)
			{
				middle[i] = _pipeline.GetKeyPoint(i + 9);
			}
		
			//Ring update
			for(int i = 0; i < 4; i++)
			{
				ring[i] = _pipeline.GetKeyPoint(i + 13);
			}
		
			//Pinky update
			for(int i = 0; i < 4; i++)
			{
				pinky[i] = _pipeline.GetKeyPoint(i + 17);
			}
		}
		
		//Debug.Log(extended);
		
		string gesture = getGesture();
		
		if (gesture == "Rock")
		{
			rock.gameObject.SetActive(true);
			paper.gameObject.SetActive(false);
			scissors.gameObject.SetActive(false);
		}
		else if (gesture == "Paper")
		{
			rock.gameObject.SetActive(false);
			paper.gameObject.SetActive(true);
			scissors.gameObject.SetActive(false);
		}
		else if (gesture == "Scissors")
		{
			rock.gameObject.SetActive(false);
			paper.gameObject.SetActive(false);
			scissors.gameObject.SetActive(true);
		}
		else
		{
			rock.gameObject.SetActive(false);
			paper.gameObject.SetActive(false);
			scissors.gameObject.SetActive(false);
		}
		
		//Debug.Log(gesture);
    }
	
	public string getGesture()
	{
		Vector3[][] fingers = {index,middle,ring,pinky};
		extended = 0;
		foreach (Vector3[] finger in fingers)
		{
			float totalY = finger[0].y + finger[1].y + finger[2].y + finger[3].y;
			if (finger[3].y > finger[2].y){
				extended++;
			}
		}
		
		if (extended == 0)
		{
			return("Rock");
		}
		else if (extended == 2)
		{
			return("Scissors");
		}
		else if (extended == 4)
		{
			return("Paper");
		}
		
		else
		{
			return("None");
		}
	}
	
    void OnDestroy()
      => _pipeline.Dispose();
	
	public void lockIn()
	{
		if (!lockedIn)
		{
			lockedIn = true;
		}
		
		else
		{
			lockedIn = false;
		}
	}
}

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
	
	HandPipeline _pipeline;
	
	Vector3[] thumb;
	Vector3[] index;
	Vector3[] middle;
	Vector3[] ring;
	Vector3[] pinky;
	
	int extended;
	
    // Start is called before the first frame update
    void Start()
    {
		_pipeline = new HandPipeline(_resources);
		
		extended = 0;
		
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
			Debug.Log("Rock!");
		}
		else if (extended == 2)
		{
			Debug.Log("Scissors!");
		}
		else if (extended == 4)
		{
			Debug.Log("Paper!");
		}
		
		//Debug.Log(extended);
    }
	
    void OnDestroy()
      => _pipeline.Dispose();
}

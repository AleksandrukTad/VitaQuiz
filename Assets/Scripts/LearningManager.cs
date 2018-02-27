using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LearningManager : MonoBehaviour {
	void Start()
	{
		Time.timeScale = 1; 
		DontDestroyOnLoad (gameObject);
		Screen.orientation = ScreenOrientation.Portrait;
	}
	void Update()
	{
		if (Application.platform == RuntimePlatform.Android) {
			if (Input.GetKey (KeyCode.Escape)) {
				SceneManager.LoadScene ("Menu", LoadSceneMode.Single);
			}
		}
	}
	private void switchScene()
	{
		SceneManager.LoadScene ("LearningDetails", LoadSceneMode.Single);
	}
	public void switchSceneProcess(string n)
	{
		gameObject.GetComponentInChildren<Text>().text = n;
		switchScene ();
	}
}

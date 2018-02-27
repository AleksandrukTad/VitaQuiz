using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bibliography : MonoBehaviour {

	void Start()
	{
		Time.timeScale = 1; 
	}
	void Update()
	{
		if (Application.platform == RuntimePlatform.Android) {
			if (Input.GetKey (KeyCode.Escape)) {
				SceneManager.LoadScene ("Menu", LoadSceneMode.Single);
			}
		}
	}
}

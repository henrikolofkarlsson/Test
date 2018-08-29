using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public bool gameOver = true;
    private UIManager _uimanager;
    public GameObject _playerPrefab;

	// Use this for initialization
	void Start () {
        _uimanager = GameObject.Find("Canvas").GetComponent<UIManager>();
	}
	
	// Update is called once per frame
	void Update () {
        if (gameOver)
        {
            _uimanager.ShowStartScreen();
            _uimanager.score = 0;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _uimanager.HideStartScreen();
                Instantiate(_playerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                gameOver = false;
            }
        }
    }
}

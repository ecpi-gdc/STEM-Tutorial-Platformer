using UnityEngine;

public class PauseMenu : MonoBehaviour
{

	public GameObject pauseScreen;

	private bool isPaused;

	// Start is called before the first frame update
	void Start()
	{
		SetPaused(false);
	}

	// Update is called once per frame
	void Update()
	{
		bool escape = Input.GetButtonDown("Pause");

		if (escape)
		{
			SetPaused(!isPaused);
		}
	}

	private void SetPaused(bool paused)
	{

		if (paused)
		{
			Time.timeScale = 0;
		}
		else
		{
			Time.timeScale = 1;
		}

		AudioListener.pause = paused;
		pauseScreen.SetActive(paused);
		isPaused = paused;
	}

#region Prepared Code
	public void Resume()
	{
		SetPaused(false);
	}

	public void Pause()
	{
		SetPaused(true);
	}
#endregion

}

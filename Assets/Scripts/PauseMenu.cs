using UnityEngine;

public class PauseMenu : MonoBehaviour
{

	public GameObject pauseScreen;

	private bool isPaused;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	private void SetPaused(bool paused)
	{
		
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

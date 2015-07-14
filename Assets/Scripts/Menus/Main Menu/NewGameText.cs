using UnityEngine;

public class NewGameText : MonoBehaviour
{
	MainMenu menu;

	void Start ()
    {
		this.menu = GameObject.Find("Main Camera").GetComponent<MainMenu>();
	}
	
	/// <summary>
	///     Raises the mouse Down event.
	/// </summary>
	void OnMouseDown()
    {
	    if (!TutorialManager.Instance.IsActive)
	    {
            this.GetComponent<Renderer>().material.color = Color.red;
	    }
	}

	/// <summary>
	///     Raises the mouse up event.
	/// </summary>
	void OnMouseUp()
    {
	    if (TutorialManager.Instance.IsActive)
	    {
	        return;
	    }

	    this.GetComponent<Renderer>().material.color = Color.white;
	    this.menu.GoToMapSelection();
    }

}

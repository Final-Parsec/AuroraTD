using UnityEngine;

public class HelpText : MonoBehaviour
{
	/// <summary>
	/// 	Raised when the mouse is clicked.
	/// </summary>
	void OnMouseDown()
	{
	    if (!TutorialManager.Instance.IsActive)
	    {
            this.GetComponent<Renderer>().material.color = Color.blue;
	    }
	}
	
	/// <summary>
	/// 	Raised when the mouse is released.
	/// </summary>
	void OnMouseUp()
	{
	    if (TutorialManager.Instance.IsActive)
	    {
	        return;
	    }

		this.GetComponent<Renderer>().material.color = Color.white;
		TutorialManager.Instance.Continue();
	}
}
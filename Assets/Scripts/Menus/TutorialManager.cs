using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance
    {
        get; 
        private set;
    }

	public GameObject[] tutorialSections;

	private Canvas canvas;
	private int currentTutorialSection;
	
	/// <summary>
	/// 	Use this for initialization
	/// </summary>
	void Start ()
	{
		var tutorialCanvas = GameObject.Find("Tutorial Canvas");
		this.canvas = tutorialCanvas.GetComponent<Canvas>();
		this.canvas.enabled = false;

	    TutorialManager.Instance = this;
	}
	

	public void Continue()
	{
		if (!this.canvas.enabled)
		{
			this.canvas.enabled = true;
			this.currentTutorialSection = -1;
		}
		
		this.currentTutorialSection++;
		
		if (this.currentTutorialSection >= this.tutorialSections.Length)
		{
			this.BackToMenu();
			return;
		}
		
		this.tutorialSections[this.currentTutorialSection].SetActive(true);
		for (int i = 0; i < this.tutorialSections.Length; i++)
		{
			if (i != this.currentTutorialSection)
			{
				this.tutorialSections[i].SetActive(false);
			}
		}
	}
	
	public void BackToMenu()
	{
		this.canvas.enabled = false;
	}

    public bool IsActive
    {
        get { return this.canvas.enabled; }
    }
}
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIManager : MonoBehaviour {

    public enum TemplateType
    {
        Sidescroll = 1,
        FlappyBird = 2,
    }

    public static UIManager instance;

    [SerializeField]
	private InputField seed;

	public InputField Seed {
		get { return seed; }
	}

    [SerializeField]
    private Dropdown templates;

    [SerializeField]
    private Dropdown difficulty;

	private TemplateType currentTemplate;
	/*
    public TemplateType CurrentTemplate
    {
        get { return currentTemplate; }
        set { currentTemplate = value; }
    }*/

    // Use this for initialization
    void Start() {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void onClickedGenerate()
    {	
		if (string.IsNullOrEmpty (seed.text))
			return;

		onClickedClear ();

		if (templates.value == 0)
			currentTemplate = TemplateType.Sidescroll;
		else
			currentTemplate = TemplateType.FlappyBird;
		
		SceneManager.LoadScene (currentTemplate.ToString (), LoadSceneMode.Additive);
    }

	public void onClickedClear()
	{
		SceneManager.UnloadScene (currentTemplate.ToString ());
	}

	public Difficulty GetDifficulty(){
		switch (difficulty.value) {
			case 0:
				return Difficulty.EASY;
			case 1:
				return Difficulty.MEDIUM;
			case 2:
				return Difficulty.HARD;
			default:
				return Difficulty.EASY;
		}
	}
}

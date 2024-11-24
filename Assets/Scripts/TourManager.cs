using UnityEngine.UI;
using UnityEngine;

public class TourManager : MonoBehaviour
{

    [Header("360 Materials")]
    [SerializeField] private Material DefaultSky;
    [SerializeField] private Material EntryHumanFood;
    [SerializeField] private Material HumanFoodOne;
    [SerializeField] private Material HumanFoodTwo;
    [SerializeField] private Material EntryAnimalFood;
    [SerializeField] private Material AnimalFoodOne;
    [SerializeField] private Material AnimalFoodTwo;

    [Header("Panels")]
    [SerializeField] private GameObject FoundersPanel;
    [SerializeField] private GameObject EntryPanel;
    [SerializeField] private Button BackBtnFoundersPanel;
    [SerializeField] private Button FoundersBtn;
    [SerializeField] private Button PitchBtn;


    [Header("Animation Stuff")]
    [SerializeField] private Animator anim;



    void showFoundersPanel()
    {
        FoundersPanel.SetActive(true);
        EntryPanel.SetActive(false);
    }

    void ShowEntryPanel()
    {
        FoundersPanel?.SetActive(false);
        EntryPanel?.SetActive(true);
    }

    void ChangeSky(Material mat)
    {
        RenderSettings.skybox = mat;
    }

    void PlayZoom()
    {
        anim.SetBool("zoom", true);
        EntryPanel.SetActive(false);
    }



    private void Start()
    {
        BackBtnFoundersPanel.onClick.AddListener(() => ShowEntryPanel());
        FoundersBtn.onClick.AddListener(() => showFoundersPanel());
        PitchBtn.onClick.AddListener(() => PlayZoom());
    }


    private void Update()
    {
        if(OVRInput.Get(OVRInput.Button.One))
        {
            ChangeSky(DefaultSky);
            showFoundersPanel();
        }
    }


}



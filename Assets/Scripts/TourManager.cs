using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

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
    [SerializeField] private Animator animFade;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;


    [Header("Wait Times")]
    [SerializeField] private float TimeToZoomIn = 4.0f;
    [SerializeField] private float TimeToEntryHumanFood = 10.0f;
    [SerializeField] private float TimeToHumanFoodOne = 30.0f;
    [SerializeField] private float TimeToHumanFoodTwo = 30.0f;

    [SerializeField] private float TimeToEntryAnimalFood = 10.0f;
    [SerializeField] private float TimeToAnimalFoodOne = 30.0f;
    [SerializeField] private float TimeToAnimalFoodTwo = 30.0f;


    private Material currentM;

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
        currentM = mat;
        animFade.SetBool("fade", true);
        Invoke(nameof(quickChange), 1.1f);
        Invoke(nameof(voidFade), 4);
    }

    void quickChange()
    {
        RenderSettings.skybox = currentM;
    }

    void voidFade()
    {
        animFade.SetBool("fade", false);
        Debug.Log("Running Reset Fade");
    }

    void PlayZoom()
    {
        anim.SetBool("zoom", true);
        EntryPanel.SetActive(false);
        StartCoroutine(MoveToEntryHumanFood());
    }


    IEnumerator MoveToEntryHumanFood()
    {
        yield return new WaitForSeconds(TimeToZoomIn);
        ChangeSky(EntryHumanFood);
        audioSource.Play();

        yield return new WaitForSeconds(TimeToEntryHumanFood);
       
        StartCoroutine(MoveToHumanFoodOne());
    }

    IEnumerator MoveToHumanFoodOne()
    {
        ChangeSky(HumanFoodOne);
        yield return new WaitForSeconds(TimeToHumanFoodOne);
     
        StartCoroutine (MoveToHumanFoodTwo());

    }

    IEnumerator MoveToHumanFoodTwo()
    {
        ChangeSky(HumanFoodTwo);
        yield return new WaitForSeconds (TimeToHumanFoodTwo);
       
        StartCoroutine(MoveToEntryAnimalFood());
    }

    IEnumerator MoveToEntryAnimalFood()
    {
        ChangeSky(EntryAnimalFood);
        yield return new WaitForSeconds(TimeToEntryAnimalFood);
 
        StartCoroutine(MoveToAnimalFoodOne());
    }

    IEnumerator MoveToAnimalFoodOne()
    {
        ChangeSky(AnimalFoodOne);
        yield return new WaitForSeconds(TimeToAnimalFoodOne);
    
        StartCoroutine(MoveToAnimalFoodTwo());
    }

    // last entry

    IEnumerator MoveToAnimalFoodTwo()
    {
        ChangeSky(AnimalFoodTwo);
        yield return new WaitForSeconds(TimeToAnimalFoodTwo);

        ChangeSky(DefaultSky);
        ShowEntryPanel();
        audioSource.Stop();
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



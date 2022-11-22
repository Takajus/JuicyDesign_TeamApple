using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuicyManager : MonoBehaviour
{
    // private static JuicyManager Instance;
    private static JuicyManager instance;

    public static JuicyManager Instance { get { return instance; } }

    public GameObject
        popUpScorePrefab,
        destructionPrefeb;

    [SerializeField]
    private GameObject player;

    private GameObject tempPrefab;

    private string playerTag;

    // Start is called before the first frame update
    void Start()
    {
        if(instance != null && instance != this)
            Destroy(gameObject);

        instance = this;

        if (!player)
            player = GameObject.FindGameObjectWithTag(playerTag);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            PopUpScoreSystem();
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            DestructionSystem();
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {

        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {

        }
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {

        }
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {

        }
        if (Input.GetKeyDown(KeyCode.Keypad7))
        {

        }
        if (Input.GetKeyDown(KeyCode.Keypad8))
        {

        }
        if (Input.GetKeyDown(KeyCode.Keypad9))
        {

        }
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {

        }
        tempPrefab = null;
    }

    public void PopUpScoreSystem(GameObject obj = null, string score = "Insert Parameter")
    {
        if (obj)
        {
            tempPrefab = Instantiate(popUpScorePrefab, obj.transform.position, obj.transform.rotation);
        }
        else
        {
            tempPrefab = Instantiate(popUpScorePrefab, player.transform.position, player.transform.rotation);
        }
        tempPrefab.GetComponent<TextMesh>().text = score;
    }

    public void DestructionSystem(GameObject obj = null)
    {
        if (obj)
        {
            tempPrefab = Instantiate(destructionPrefeb, obj.transform.position, obj.transform.rotation);
        }
        else
        {
            tempPrefab = Instantiate(destructionPrefeb, player.transform.position, player.transform.rotation);
        }
    }

}

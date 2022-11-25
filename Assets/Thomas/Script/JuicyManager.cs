using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JuicyManager : MonoBehaviour
{
    // private static JuicyManager Instance;
    private static JuicyManager instance;

    public static JuicyManager Instance { get { return instance; } }

    [Header("FX Prefab")]
    public GameObject
        popUpScorePrefab,
        destructionPrefab,
        propulsionPrefab, 
        sprayPrefab;

    [Header("Player")]
    [SerializeField]
    private GameObject player, weapon;

    private GameObject tempPrefab;

    private string playerTag;

    [Header("VR + Keyboard Input")]
    public InputActionProperty trigger, movement, test, trigger2;
    public bool VrVsInput;

    public bool temp, temp1;

    private Vector3 position1 = Vector3.zero;

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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (VrVsInput)
                VrVsInput = false;
            else if (!VrVsInput)
                VrVsInput = true;
        }

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
            Propulsion(-1f);
        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            StartShooting();
        }
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            CameraShake();
        }
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            // sound
        }
        if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            PlayerController.Instance.SetIntensity(0.2f);
        }
        if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            // BFG
        }
        if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            // Destruction Vaisseau
        }
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            // Chargement du BFG
        }
        tempPrefab = null;

        temp = Fire1();
        temp1 = Fire2();

        // Debug.Log(vectorTest().ToString());
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
        //tempPrefab.GetChildComponent<TextMesh>().text = score;
        tempPrefab.GetComponentInChildren<TextMesh>().text = score;
        Destroy(tempPrefab, 1f);
    }

    public void DestructionSystem(GameObject obj = null)
    {
        if (obj)
        {
            tempPrefab = Instantiate(destructionPrefab, obj.transform.position, obj.transform.rotation);
        }
        else
        {
            tempPrefab = Instantiate(destructionPrefab, player.transform.position, player.transform.rotation);
        }
        Destroy(tempPrefab, 0.3f);
    }

    public void Propulsion(float position)
    {
        tempPrefab = Instantiate(propulsionPrefab, new Vector3(player.transform.position.x + position, player.transform.position.y, player.transform.position.z), new Quaternion(propulsionPrefab.transform.rotation.x, propulsionPrefab.transform.rotation.y, propulsionPrefab.transform.rotation.z, propulsionPrefab.transform.rotation.w), player.transform);
        Destroy(tempPrefab, 0.3f);
    }

    public void StartShooting()
    {
        tempPrefab = Instantiate(sprayPrefab, new Vector3(weapon.transform.position.x, weapon.transform.position.y, weapon.transform.position.z + 2), new Quaternion(player.transform.rotation.x, player.transform.rotation.y, player.transform.rotation.z, player.transform.rotation.w), player.transform);
        Destroy(tempPrefab, 0.3f); 
    }

    public void CameraShake()
    {
        player.GetComponent<CameraShake>().enabled = true;
    }

    #region Input

    public bool Fire1()
    {
        bool tempBool = false;
        if (VrVsInput)
        {
            tempBool = trigger.action.WasPressedThisFrame();
        }
        else if (!VrVsInput)
        {
            tempBool = Input.GetButtonDown("Fire1");
        }
        /*if(tempBool)
            Debug.Log("Fire1");*/

        return tempBool;
    }

    public bool Fire2()
    {
        bool tempBool = false;
        if (VrVsInput)
        {
            tempBool = trigger2.action.WasPressedThisFrame();
        }
        else if (!VrVsInput)
        {
            tempBool = Input.GetButtonDown("Fire2");
        }
        /*if (tempBool)
            Debug.Log("Fire2");*/

        return tempBool;
    }

    public float vectorTest()
    {
        float movementF = 0f;
        if (VrVsInput)
        {
            movementF = movement.action.ReadValue<Vector2>().x;
        }
        else if (!VrVsInput)
        {
            movementF = Input.GetAxis("Horizontal");
        }
        //Debug.Log(movement.ToString());

        return movementF;
    }
    #endregion

}

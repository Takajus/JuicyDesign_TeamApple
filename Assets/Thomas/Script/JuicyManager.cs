using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JuicyManager : MonoBehaviour
{
    // private static JuicyManager Instance;
    private static JuicyManager instance;

    public static JuicyManager Instance { get { return instance; } }

    public GameObject
        popUpScorePrefab,
        destructionPrefab,
        propulsionPrefab;

    [SerializeField]
    private GameObject player;

    private GameObject tempPrefab;

    private string playerTag;

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
            
        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            Propulsion(-1f, 90f);
        }
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            Propulsion(1f, -90f);
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
    }

    public void Propulsion(float position, float rotation)
    {
        tempPrefab = Instantiate(propulsionPrefab, new Vector3(player.transform.position.x + position, player.transform.position.y, player.transform.position.z), new Quaternion(player.transform.rotation.x, player.transform.rotation.y, player.transform.rotation.z + rotation, player.transform.rotation.w), player.transform);
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

using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Camera Camera;

    [Header("Managers")]
    [SerializeField] public GameObject AnimateManager;

    [Header("PasswordPanel")]
    [SerializeField] public GameObject password_drawer;
    [SerializeField] public GameObject password_OnDesk;

    [Header("Key")]
    [SerializeField] public GameObject Key;

    [Header("Position Setting")]
    [SerializeField] public float RightLimit;
    [SerializeField] public float MoveChuck;
    [SerializeField] public float Start_CameraSize;
    [SerializeField] public Vector3 Start_Position;
    [SerializeField] public Vector3 Start_Rotation;
    
    [Header("Booleans")]
    public bool IsZoomIn;
    public bool HavingKey;
    public bool IsPasswordLocking_drawer;
    public bool IsPasswordLocking_table;
    public bool ShowTheMessageOfDrawerOpen;


    void Update()
    {
        var CallUICtrl = GetComponent<UIController>();
        var animator = AnimateManager.GetComponent<Animate>();
        if(IsZoomIn)
            CallUICtrl.IgnoreLeftRightButton();
        else
            CallUICtrl.ActiveLeftRightButton();

        if (Input.GetMouseButtonDown(0))
            ShootRay();
        if (ShowTheMessageOfDrawerOpen)
        {
            StartCoroutine(ShowTheText("なんか開きました！"));
            if(!IsPasswordLocking_drawer) 
            {
                this.GetComponent<CameraController>().ZoomIn("Cabinet");
                animator.OpenTheDrawer();
                StartCoroutine(ShowTheKey());
            }
            if(!IsPasswordLocking_table) animator.OpenTheDoorOfDrawer();
            ShowTheMessageOfDrawerOpen = false;
        }
    }
    void ShootRay()
    {
        Vector3 mousePosition = Input.mousePosition;
        Ray myRay = Camera.ScreenPointToRay(mousePosition);
        RaycastHit raycastHit;
        bool weHitSomething = Physics.Raycast(myRay, out raycastHit);
        var FoundTag = raycastHit.transform.tag;
        var CamCtrl = this.GetComponent<CameraController>();
        
        if (weHitSomething)
        {
            Debug.Log(FoundTag);
            if(FoundTag== "DoorLock" && !IsZoomIn)
                CamCtrl.ZoomIn("DoorLock");
            if (FoundTag == "Key")
            {
                if (CamCtrl.checkRoomPos(1))
                {
                    if(!IsZoomIn) CamCtrl.ZoomIn("DoorLock");
                    if(HavingKey && IsZoomIn)
                    {
                        StartCoroutine(ShowTheText("ドアーが開きました！"));
                        this.GetComponent<UIController>().FadeIn = true;
                    }
                    else 
                        StartCoroutine(ShowTheText("錠をかけている"));
                }
                if (CamCtrl.checkRoomPos(0))
                {
                    Key.SetActive(false);
                    HavingKey = true;
                }
            }

            if(FoundTag == "Cabinet")
            {
                if (!IsZoomIn && (IsPasswordLocking_drawer || IsPasswordLocking_table))
                {
                    CamCtrl.ZoomIn("Cabinet");
                }
                else if(!IsPasswordLocking_drawer && !IsPasswordLocking_table)
                {
                    CamCtrl.ZoomIn("OpenedDrawer");
                }
            }
            

            if(FoundTag == "Password" && CamCtrl.checkRoomPos(0))
            {
                CamCtrl.ZoomIn("PasswordPanel");
                if (IsPasswordLocking_drawer)
                {
                    password_drawer.GetComponent<Password_in_drawer>().changeTheBool(raycastHit.transform.name);
                }
                
            }
            if(FoundTag == "Password" && CamCtrl.checkRoomPos(3))
            {
                CamCtrl.ZoomIn("PasswordPanelTable");
                if(IsPasswordLocking_table)
                    password_OnDesk.GetComponent<Password_on_table>().changeTheBool(raycastHit.transform.name);
            }
            if(FoundTag == "Picture")
            {
                AnimateManager.GetComponent<Animate>().DropThePicture();
            }
        }
        else
        {
            return;
        }
    }

    private IEnumerator ShowTheKey()
    {
        yield return new WaitForSeconds(1f);
        Key.SetActive(true);
    }
    private IEnumerator ShowTheText(string Text)
    {
        this.GetComponent<UIController>().ActiveText(Text);
        yield return new WaitForSeconds(1.5f);
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using TMPro;

public class GameManager : MonoBehaviour
{
    private Camera Camera;
    [SerializeField] public GameObject AnimatManager,PasswordPanel_drawer, PasswordPanel_OnDesk, Key;
    [SerializeField] public float RightLimit, MoveChuck, Start_CameraSize;
    [SerializeField] public Vector3 Start_Position, Start_Rotation;
    
    public bool IsZoomIn, HavingKey,IsPasswordLocking_drawer,IsPasswordLocking_table;
    public bool Dummy = false;

    void Awake()
    {
        Camera = Object.FindFirstObjectByType<Camera>();
        Key.SetActive(false);
    }
    void Update()
    {
        var CallUICtrl = GetComponent<UIController>();
        var animator = AnimatManager.GetComponent<Animat>();
        if(IsZoomIn)
            CallUICtrl.IgnorLeftRightButton();
        else
            CallUICtrl.ActiveLeftRightButton();

        if (Input.GetMouseButtonDown(0))
            ShootRay();
        if (Dummy)
        {
            this.GetComponent<UIController>().ActiveText("Somthing Oped");
            if(!IsPasswordLocking_drawer) 
            {
                this.GetComponent<CameraController>().ZoomIn("Cabinet");
                animator.OpenTheDrawer();
                StartCoroutine(ShowTheKey());
            }
            if(!IsPasswordLocking_table) animator.OpenTheDoorOfDrawer();
            Dummy = false;
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
                        Debug.Log("Open door");
                        this.GetComponent<UIController>().FadeIn = true;
                    }
                    else 
                        Debug.Log("Lock");
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
                    PasswordPanel_drawer.GetComponent<Password_in_drawer>().changeTheBool(raycastHit.transform.name);
                }
                
            }
            if(FoundTag == "Password" && CamCtrl.checkRoomPos(3))
            {
                CamCtrl.ZoomIn("PasswordPanelTable");
                if(IsPasswordLocking_table)
                    PasswordPanel_OnDesk.GetComponent<Password_on_table>().changeTheBool(raycastHit.transform.name);
            }
            if(FoundTag == "Picture")
            {
                AnimatManager.GetComponent<Animat>().DropThePicture();
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
}

using Unity.VisualScripting;
using UnityEngine;

public class Animat : MonoBehaviour
{
    [SerializeField] GameObject Picture, Drawer;


    public void DropThePicture()
    {
        Picture.GetComponent<Animator>().SetTrigger("DropPic");
    }
    public void OpenTheDoorOfDrawer()
    {
        Drawer.GetComponent<Animator>().Play("OpenTheDoorOfDrawer");
    }
    public void OpenTheDrawer()
    {
        Drawer.GetComponent<Animator>().SetTrigger("OpenDrawer");
    }
}

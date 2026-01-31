using UnityEngine;

public class Writer_sc : MonoBehaviour
{
    public Degisken_SC degisken;
    public GameObject MM_screan;
    public GameObject Left_btn;
    public GameObject Right_btn;

    private void Update()
    {
        if (degisken.in_game)
        {
            Left_btn.SetActive(true);
            Right_btn.SetActive(true);
            MM_screan.SetActive(false);
        }
        else
        {
            Left_btn.SetActive(false);
            Right_btn.SetActive(false);
            MM_screan.SetActive(true);
        }  
    }
}

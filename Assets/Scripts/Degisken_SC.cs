using UnityEngine;

public class Degisken_SC : MonoBehaviour
{
    [Header("Toplanabilirler")]
    public bool rotating = false;
    public bool rotating_ = false;
    public bool rotating_MM = false;
    public Transform pickedObj;

    [Header("Oyun")]
    public bool in_game=false;
    public bool out_game=false;

    [Header("Ayarlar")]
    public float donus_hiz = 4f;
}

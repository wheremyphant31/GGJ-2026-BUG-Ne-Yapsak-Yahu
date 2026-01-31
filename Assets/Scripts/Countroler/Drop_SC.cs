using UnityEngine;

public class Drop_SC : MonoBehaviour
{
    public Degisken_SC degisken;
    public float moveSpeed = 10f;    // Taşınma hızı
    private bool isThrowing = false;
    private Vector3 targetPos;
    private Quaternion targetRot;

    void Update()
    {
        // Sol tık ile ray at
        if (Input.GetMouseButtonDown(0))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Sadece ortadaki objeye tıklanınca çalışsın
                if (hit.collider.CompareTag("Boiler")|| hit.collider.CompareTag("Trash")) // Ortadaki objeye "Target" tag'i ver
                {
                    targetPos = hit.collider.transform.position;
                    targetRot = hit.collider.transform.rotation;
                    isThrowing = true;
                }
            }
        }

        // Objeyi hedefe doğru yavaşça taşı
        if (isThrowing && degisken.pickedObj != null)
        {
            degisken.pickedObj.position = Vector3.MoveTowards(
                degisken.pickedObj.position,
                targetPos,
                moveSpeed * Time.deltaTime
            );

            degisken.pickedObj.rotation = Quaternion.Lerp(
                degisken.pickedObj.rotation,
                targetRot,
                moveSpeed * Time.deltaTime
            );

            // Hedefe ulaştığında sil
            if (Vector3.Distance(degisken.pickedObj.position, targetPos) < 0.01f)
            {
                Destroy(degisken.pickedObj.gameObject); // Objeyi sahneden kaldır
                degisken.pickedObj = null;
                isThrowing = false;
            }
        }
    }


}

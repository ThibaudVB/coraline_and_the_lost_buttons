using UnityEngine;
using TMPro;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public TMP_Text artefactCountText;
    public DoorsLocked doorLocked;
    public TMP_Text messageText;

    private Vector3 initialScale;
    private bool doorAlreadyUnlocked = false;

    void Awake()
    {
        // Reset automatique à chaque début de scène
        ResetArtefactCountOnDeath();
    }

    void OnEnable()
    {
        Artefact.OnArtefactCollected += UpdateArtefactCount;
    }

    void OnDisable()
    {
        Artefact.OnArtefactCollected -= UpdateArtefactCount;
    }

    void Start()
    {
        initialScale = artefactCountText.transform.localScale;
        artefactCountText.text = "0/5";
    }

    void UpdateArtefactCount(int count)
    {
        artefactCountText.text = Mathf.Min(count, 5) + "/5";
        StartCoroutine(AnimateArtefactCount());

        if (count >= 5 && !doorAlreadyUnlocked)
        {
            doorAlreadyUnlocked = true;
            doorLocked.UnlockDoor();
            ShowMessage("Porte déverrouillée !");
        }
    }

    IEnumerator AnimateArtefactCount()
    {
        Vector3 targetScale = initialScale * 1.5f;
        float duration = 0.2f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            artefactCountText.transform.localScale = Vector3.Lerp(initialScale, targetScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            artefactCountText.transform.localScale = Vector3.Lerp(targetScale, initialScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        artefactCountText.transform.localScale = initialScale;
    }

    void ShowMessage(string message)
    {
        messageText.text = message;
        Invoke("ClearMessage", 2f);
    }

    void ClearMessage()
    {
        messageText.text = "";
    }

    public void ResetArtefactCountOnDeath()
    {
        Artefact.ResetArtefactsCollected();  // Reset compteur
        artefactCountText.text = "0/5";      // Reset UI
        doorAlreadyUnlocked = false;         // Reset porte
    }
}

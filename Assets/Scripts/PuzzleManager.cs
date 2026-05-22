using UnityEngine;
using System.Collections;

public class PuzzleManager : MonoBehaviour
{
    public GameObject top1;
    public GameObject top2;
    public GameObject top3;

    public GameObject duvar1future;

    private int currentStep = 1;

    public void TopTouched(string topName)
    {
        if (topName == "top1" && currentStep == 1)
        {
            StartCoroutine(ActivateTop(top1));
            currentStep = 2;
        }

        else if (topName == "top2" && currentStep == 2)
        {
            StartCoroutine(ActivateTop(top2));
            currentStep = 3;
        }

        else if (topName == "top3" && currentStep == 3)
        {
            StartCoroutine(FinishPuzzle());
        }
    }

    IEnumerator ActivateTop(GameObject top)
    {
        Renderer r = top.GetComponent<Renderer>();

        // Yeþil yap
        r.material.color = Color.green;

        // 1 saniye bekle
        yield return new WaitForSeconds(1f);

        // Top kaybolsun
        top.SetActive(false);
    }

    IEnumerator FinishPuzzle()
    {
        Renderer r = top3.GetComponent<Renderer>();

        r.material.color = Color.green;

        yield return new WaitForSeconds(1f);

        top3.SetActive(false);

        Debug.Log("Puzzle tamamlandý");

        Destroy(duvar1future);
    }
}
using UnityEngine;
using System.Collections;

public class CubePuzzleManager : MonoBehaviour
{
    public Transform player;
    public Transform spawnPoint;

    public GameObject g1, g2, p1, p2, g3, g4, p3, p4;
    public GameObject d1, d2, d3, d4;

    int step = 1;

    void Start()
    {
        ResetPuzzle();
    }

    public void CubeTouched(string cubeName, GameObject cube)
    {
        // HER ZAMAN YANLIÞ OLAN KÜPLER
        if (cubeName == "g2" || cubeName == "p2" || cubeName == "g3" || cubeName == "p4")
        {
            StartCoroutine(RedFail(cube));
            return;
        }

        switch (step)
        {
            case 1:

                if (cubeName == "g1")
                {
                    StartCoroutine(GreenCube(cube));
                    d1.SetActive(false);
                    step = 2;
                }

                break;

            case 2:

                if (cubeName == "p1")
                {
                    StartCoroutine(GreenCube(cube));
                    d2.SetActive(false);
                    step = 3;
                }

                break;

            case 3:

                if (cubeName == "g4")
                {
                    StartCoroutine(GreenCube(cube));
                    d3.SetActive(false);
                    step = 4;
                }

                break;

            case 4:

                if (cubeName == "p3")
                {
                    StartCoroutine(GreenCube(cube));
                    d4.SetActive(false);

                    Debug.Log("Puzzle tamam");
                }

                break;
        }
    }

    IEnumerator GreenCube(GameObject cube)
    {
        cube.GetComponent<Renderer>().material.color = Color.green;

        yield return null;
    }

    IEnumerator RedFail(GameObject cube)
    {
        cube.GetComponent<Renderer>().material.color = Color.red;

        yield return new WaitForSeconds(0.3f);

        CharacterController cc = player.GetComponent<CharacterController>();

        if (cc != null)
            cc.enabled = false;

        player.position = spawnPoint.position;

        if (cc != null)
            cc.enabled = true;

        ResetPuzzle();
    }

    void ResetPuzzle()
    {
        step = 1;

        d1.SetActive(true);
        d2.SetActive(true);
        d3.SetActive(true);
        d4.SetActive(true);

        ResetColor(g1);
        ResetColor(g2);
        ResetColor(p1);
        ResetColor(p2);
        ResetColor(g3);
        ResetColor(g4);
        ResetColor(p3);
        ResetColor(p4);
    }

    void ResetColor(GameObject obj)
    {
        obj.GetComponent<Renderer>().material.color = Color.white;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustManager : MonoBehaviour
{
    public static DustManager instance = null;
    List<GameObject> dusts = new List<GameObject>();

    void Start()
    {
        if (instance == null)
            instance = this;

        for (int i = 0; i < transform.childCount; i++)
        {
            dusts.Add(transform.GetChild(i).gameObject);
        }
    }

    public void SpawnDust(Vector2 position)
    {
        for (int i = 0; i < dusts.Count; i++)
        {
            if(!dusts[i].activeSelf)
            {
                dusts[i].transform.position = new Vector3(position.x, position.y, 0);
                dusts[i].SetActive(true);
                StartCoroutine(HideDust(dusts[i]));
                break;
            }
        }
    }

    IEnumerator HideDust(GameObject dust)
    {
        yield return new WaitForSeconds(2);
        dust.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UNKSController : MonoBehaviour
{
    private UNKSManager manager;

    void Start()
    {
        manager = GetComponent<UNKSManager>();
        StartCoroutine(MoveRadar());
    }

    IEnumerator MoveRadar()
    {
        print(string.Join(' ', manager.MoveLeft(1)));
        yield return new WaitForSeconds(3f);
        print(string.Join(' ', manager.MoveRight(1)));
        yield return new WaitForSeconds(1f);
        print(string.Join(' ', manager.GetStatus()));
        manager.MoveStop();
        manager.MoveLeft(1);
        yield return new WaitForSeconds(5f);
        print(string.Join(' ', manager.GetStatus()));
        manager.MoveRight(1);
        yield return new WaitForSeconds(10f);
        print(string.Join(' ', manager.GetStatus()));
    }
}

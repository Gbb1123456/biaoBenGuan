using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZXKFramework;

public class GuanChaWanBi : MonoBehaviour
{
    Button btn;
    // Start is called before the first frame update
    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(() =>
        {
            GameManager.Instance.playerRot.enabled = true;
            //GameManager.Instance.playerMove.enabled = true;
            GameManager.Instance.transform.FindFirst("Look_Canvas").SetActive(false);
            for (int i = 0; i < GameManager.Instance.allLookModel.Count; i++)
            {
                GameManager.Instance.allLookModel[i].SetActive(false);
            }
            GameManager.Instance.transform.FindFirst("PlayerControllerFPS").GetComponent<FirstPersonController>().enabled = true;
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

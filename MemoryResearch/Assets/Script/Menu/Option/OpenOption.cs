using System.Collections.Generic;
using UnityEngine;
using CustomInputKey;

public class OpenOption : MonoBehaviour
{
    //�I�v�V�����p�l��
    [SerializeField]GameObject option;

    //���j���[�p�l���̕\���p
    bool show;

    // Start is called before the first frame update
    void Start()
    {
        option.SetActive(false);
        show = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(CustomInput.Interval_InputKeydown(KeyCode.O, 1))
        {
            if (!option.activeSelf)
            {
                OnOption();
            }
            else
            {
                OffOption();
            }
        }
    }

    public void OnOption()
    {
        option.SetActive(true);
    }

    public void OffOption()
    {
        option.SetActive(false);
    }
}

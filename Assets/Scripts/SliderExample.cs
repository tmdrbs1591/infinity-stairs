using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderExample : MonoBehaviour
{
    public Slider slider;
    public Player player;
    public float decreaseSpeed = 1.0f; // 슬라이더의 감소 속도를 조절할 변수

    void Start()
    {
        // slider 변수에 현재 슬라이더 게임 오브젝트의 Slider 컴포넌트를 할당합니다.
        slider = GetComponent<Slider>();
    }

    void Update()
    {

        if (player.isDie)
            return;
        // 슬라이더 값을 시간 변수를 이용하여 감소시킵니다.
        slider.value -= decreaseSpeed * Time.deltaTime; // decreaseSpeed에 Time.deltaTime을 곱해 속도를 조절합니다.

        // 슬라이더의 값이 0 이하로 떨어지면 0으로 고정합니다.
        if (slider.value <= 0)
        {
            player.CharDie();
        }
    }
}

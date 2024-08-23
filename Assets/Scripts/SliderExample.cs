using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderExample : MonoBehaviour
{
    public Slider slider;
    public Player player;
    public float decreaseSpeed = 1.0f; // �����̴��� ���� �ӵ��� ������ ����

    void Start()
    {
        // slider ������ ���� �����̴� ���� ������Ʈ�� Slider ������Ʈ�� �Ҵ��մϴ�.
        slider = GetComponent<Slider>();
    }

    void Update()
    {

        if (player.isDie)
            return;
        // �����̴� ���� �ð� ������ �̿��Ͽ� ���ҽ�ŵ�ϴ�.
        slider.value -= decreaseSpeed * Time.deltaTime; // decreaseSpeed�� Time.deltaTime�� ���� �ӵ��� �����մϴ�.

        // �����̴��� ���� 0 ���Ϸ� �������� 0���� �����մϴ�.
        if (slider.value <= 0)
        {
            player.CharDie();
        }
    }
}

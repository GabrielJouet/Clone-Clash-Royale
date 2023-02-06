using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class PlayableController : MonoBehaviour
{
    [SerializeField]
    protected Slider _manaSlider;


    public float Mana { get; protected set; } = 7;

    protected float _manaPerSecond = 0.25f;



    protected void Start()
    {
        StartCoroutine(ChargeUpMana());
    }


    protected IEnumerator ChargeUpMana()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.25f);
            AddMana(_manaPerSecond / 4);
        }
    }


    protected void AddMana(float amount)
    {
        Mana = Mathf.Clamp(Mana + amount, 0, 10);
        _manaSlider.value = Mana;
    }


    protected void RemoveMana(float amount)
    {
        Mana = Mathf.Clamp(Mana - amount, 0, 10);
        _manaSlider.value = Mana;
    }
}
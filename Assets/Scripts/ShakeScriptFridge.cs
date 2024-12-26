using UnityEngine;
using UnityEngine.UI;
using System.Collections;  // Добавляем это пространство имен для работы с корутинами

public class FridgeAnimationController : MonoBehaviour
{
    public Animator fridgeAnimator;  // Ссылка на Animator холодильника
    public Button shakeButton;  // Ссылка на кнопку для короткой тряски
    public Button breakdanceButton;  // Ссылка на кнопку для брейкданса

    private void Start()
    {
        // Проверяем, что Animator привязан
        if (fridgeAnimator == null)
        {
            fridgeAnimator = GetComponent<Animator>();
        }

        // Привязываем кнопки к методам
        if (shakeButton != null)
        {
            shakeButton.onClick.AddListener(ShakeStart);  // Короткая тряска
        }

        if (breakdanceButton != null)
        {
            breakdanceButton.onClick.AddListener(StartBreakdance);  // Брейкданс
        }
    }

    // Метод для короткой тряски (при повторном нажатии кнопки)
    public void ShakeStart()
    {
        if (!fridgeAnimator.GetCurrentAnimatorStateInfo(0).IsName("ShakeAnimation"))  // Проверка, не проигрывается ли уже эта анимация
        {
            fridgeAnimator.SetTrigger("ShakeTrigger");
            StartCoroutine(ResetToIdle(1f));  // Возврат в idle через 1 секунду (длительность анимации)
        }
    }

    // Метод для брейкданса (однократная анимация по нажатию на кнопку)
    public void StartBreakdance()
    {
        if (!fridgeAnimator.GetCurrentAnimatorStateInfo(0).IsName("BreakdanceAnimation"))  // Проверка, не проигрывается ли уже эта анимация
        {
            fridgeAnimator.SetTrigger("BreakdanceTrigger");
            StartCoroutine(ResetToIdle(2f));  // Возврат в idle через 2 секунды (длительность анимации)
        }
    }

    // Корутин для возврата в idle после завершения анимации
    private IEnumerator ResetToIdle(float delay)
    {
        yield return new WaitForSeconds(delay);  // Ждём заданное время (время длительности анимации)
        fridgeAnimator.SetTrigger("Idle");  // Возвращаемся в idle
    }
}

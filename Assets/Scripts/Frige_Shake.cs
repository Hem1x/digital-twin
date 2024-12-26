using UnityEngine;

public class FridgeShake : MonoBehaviour
{
    public float shakeForce = 5f; // Сила тряски
    public float shakeDuration = 2f; // Длительность тряски

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Rigidbody rb;
    private bool isShaking = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        rb.isKinematic = true; // По умолчанию Rigidbody статичен
    }

    public void StartShake()
    {
        if (!isShaking)
        {
            isShaking = true;
            rb.isKinematic = false; // Разрешаем воздействие физики
            InvokeRepeating("ApplyRandomShake", 0f, 0.1f); // Начинаем тряску
            Invoke("StopShake", shakeDuration); // Останавливаем через заданное время
        }
    }

    private void ApplyRandomShake()
    {
        Vector3 randomForce = Random.insideUnitSphere * shakeForce; // Случайная сила
        rb.AddForce(randomForce, ForceMode.Impulse);
    }

    private void StopShake()
    {
        CancelInvoke("ApplyRandomShake"); // Останавливаем вызов случайных сил
        rb.isKinematic = true; // Отключаем физику
        transform.position = initialPosition; // Возвращаем в исходное положение
        transform.rotation = initialRotation; // Возвращаем в исходный поворот
        isShaking = false;
    }
}

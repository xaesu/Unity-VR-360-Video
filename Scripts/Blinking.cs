using UnityEngine;

public class Blinking : MonoBehaviour
{
    public float blinkInterval = 1.0f;
    public float fadeDuration = 1.0f; // 밝아지고 어두워지는 전환 시간 설정

    private CanvasRenderer objectRenderer;
    private bool isBlinking = false;
    private float targetAlpha = 0f; // 목표 알파값
    private float currentAlpha = 1f; // 현재 알파값
    private float fadeTimer = 0f; // 보간 타이머

    private void Start()
    {
        objectRenderer = GetComponent<CanvasRenderer>();
        StartBlinking();
    }

    private void StartBlinking()
    {
        if (!isBlinking)
        {
            isBlinking = true;
            InvokeRepeating("ToggleVisibility", 0f, blinkInterval);
        }
    }

    private void StopBlinking()
    {
        if (isBlinking)
        {
            isBlinking = false;
            CancelInvoke("ToggleVisibility");
            ResetVisibility();
        }
    }

    private void ToggleVisibility()
    {
        targetAlpha = targetAlpha == 0f ? 1f : 0f; // 알파값 목표를 토글
        fadeTimer = 0f; // 보간 타이머 초기화
    }

    private void ResetVisibility()
    {
        objectRenderer.SetAlpha(1f); // 알파값 초기화
        targetAlpha = 0f;
        currentAlpha = 1f;
        fadeTimer = 0f;
    }

    private void Update()
    {
        if (isBlinking)
        {
            if (fadeTimer < fadeDuration)
            {
                fadeTimer += Time.deltaTime;
                currentAlpha = Mathf.Lerp(currentAlpha, targetAlpha, fadeTimer / fadeDuration); // 보간
                objectRenderer.SetAlpha(currentAlpha);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isBlinking)
            {
                StopBlinking();
            }
            else
            {
                StartBlinking();
            }
        }
    }
}

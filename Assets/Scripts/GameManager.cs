using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("正解数の設定")]
    public int totalTargets = 3;
    private int currentCorrect = 0;
    private bool isCleared = false;
    private bool canGoNext = false;

    [Header("UI 表示")]
    public TextMeshProUGUI statusText; // 1 / 3 の表示など
    public GameObject nextButton; // 次へボタン（任意）

    [Header("ステージ遷移設定")]
    public string nextStageName; // 次に進むシーンの名前（Inspectorで入力）

    [Header("クリア演出")]
    public GameObject door;
    public Animator doorAnimator;
    public AudioSource victorySE;

    void Start()
    {
        UpdateStatusText();

        // 次へボタンを最初は非表示に
        if (nextButton != null)
            nextButton.SetActive(false);
    }

    public void ReportCorrect()
    {
        if (isCleared) return;

        currentCorrect++;
        Debug.Log($"正解数: {currentCorrect}/{totalTargets}");
        UpdateStatusText();

        if (currentCorrect >= totalTargets)
        {
            OnClear();
        }
    }

    public void ReportCancel()
    {
        if (isCleared) return;

        currentCorrect--;
        if (currentCorrect < 0) currentCorrect = 0;
        Debug.Log($"キャンセル: {currentCorrect}/{totalTargets}");
        UpdateStatusText();
    }

    void UpdateStatusText()
    {
        if (statusText != null)
        {
            if (isCleared)
                statusText.text = "Clear!";
            else
                statusText.text = $"{currentCorrect} / {totalTargets}";
        }
    }

    void OnClear()
    {
        isCleared = true;
        canGoNext = true;

        Debug.Log("🎉 すべてのアイテムが正しく配置されました！");

        if (doorAnimator != null)
            doorAnimator.SetTrigger("Open");

        /*if (door != null)
            door.transform.rotation = Quaternion.Euler(0, 90, 0);*/

        if (victorySE != null)
            victorySE.Play();

        if (nextButton != null)
            nextButton.SetActive(true);

        UpdateStatusText();
    }

    // 他スクリプト用：通過OKかどうか
    public bool CanGoNext()
    {
        return canGoNext;
    }

    // 他スクリプトやUIボタンから呼び出す
    public void LoadNextStage()
    {
        if (!string.IsNullOrEmpty(nextStageName))
        {
            Debug.Log($"次のステージへ移動: {nextStageName}");
            SceneManager.LoadScene(nextStageName);
        }
        else
        {
            Debug.LogWarning("次のステージ名が設定されていません！");
        }
    }
}

using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("正解数の設定")]
    public int totalTargets = 3; // 目標の数（配置すべき正解数）

    [Header("UI 表示")]
    public TextMeshProUGUI statusText; // 表示用の TMP テキスト

    [Header("クリア時に開くドア")]
    public GameObject door;           // 回転して開くドアオブジェクト
    public Animator doorAnimator;     // アニメーションで動かす場合
    public AudioSource victorySE;     // クリア時の音（任意）

    private int currentCorrect = 0;   // 現在の正解数
    private bool isCleared = false;   // クリア済みかどうか

    void Start()
    {
        UpdateStatusText(); // ゲーム開始時に最初の表示
    }

    // 正解を報告（1つ成功）
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

    // 正解が解除されたとき
    public void ReportCancel()
    {
        if (isCleared) return;

        currentCorrect--;
        if (currentCorrect < 0) currentCorrect = 0;
        Debug.Log($"キャンセル: {currentCorrect}/{totalTargets}");
        UpdateStatusText();
    }

    // 表示更新
    void UpdateStatusText()
    {
        if (statusText != null)
        {
            statusText.text = $"{currentCorrect} / {totalTargets}";
        }
    }

    // すべて正解したときの処理
    void OnClear()
    {
        isCleared = true;
        Debug.Log("🎉 すべてのアイテムが正しく配置されました！");

        if (doorAnimator != null)
        {
            doorAnimator.SetTrigger("Open");
        }

        if (door != null)
        {
            door.transform.rotation = Quaternion.Euler(0, 90, 0);
        }

        if (victorySE != null)
        {
            victorySE.Play();
        }

        // テキストも更新（任意で "Clear!" にするなど）
        if (statusText != null)
        {
            statusText.text = "Clear!";
        }
    }
}

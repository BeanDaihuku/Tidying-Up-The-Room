using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class ShowHand : MonoBehaviour
{
    [Header("Hand Animation Settings")]
    [SerializeField] private Animator handAnimator;
    [SerializeField] private string gripBoolParam = "IsGripping";
    
    [Header("Input Actions")]
    [SerializeField] private InputActionReference gripInputAction;
    [SerializeField] private float gripThreshold = 0.5f;
    
    // 現在のgrip値
    private float currentGripValue = 0f;
    private bool isGripping = false;
    
    void Start()
    {
        // アニメーターが設定されていない場合、このゲームオブジェクトから取得を試みる
        if (handAnimator == null)
        {
            handAnimator = GetComponent<Animator>();
        }
        
        // 必要なコンポーネントが見つからない場合の警告
        if (handAnimator == null)
        {
            Debug.LogWarning("Animator が見つかりません。手動で設定してください。");
        }
        
        if (gripInputAction == null)
        {
            Debug.LogWarning("Grip Input Action が設定されていません。");
        }
    }
    
    void OnEnable()
    {
        // InputActionを有効化
        if (gripInputAction != null)
            gripInputAction.action.Enable();
    }
    
    void OnDisable()
    {
        // InputActionを無効化
        if (gripInputAction != null)
            gripInputAction.action.Disable();
    }
    
    void Update()
    {
        UpdateHandPose();
    }

    private void UpdateHandPose()
    {
        if (handAnimator == null)
            return;

        // Gripボタンの入力値を取得
        if (gripInputAction != null && gripInputAction.action.enabled)
        {
            currentGripValue = gripInputAction.action.ReadValue<float>();

            // デバッグ用にコンソールに表示
            Debug.Log($"Grip Value: {currentGripValue:F3}");

            // しきい値でbool値を判定
            bool newIsGripping = currentGripValue >= gripThreshold;

            // 状態が変わった時のみアニメーターを更新
            if (newIsGripping != isGripping)
            {
                isGripping = newIsGripping;
                handAnimator.SetBool(gripBoolParam, isGripping);
                Debug.Log($"Hand Gripping: {isGripping}");
            }
        }

        Debug.Log($"Grip Value: {currentGripValue:F3}");
        Debug.Log($"Grip Value: {gripInputAction.action.ReadValue<float>():F3}");
        Debug.Log($"Is Gripping: {isGripping}");
    }
}

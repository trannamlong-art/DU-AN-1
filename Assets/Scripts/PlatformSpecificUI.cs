using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // <-- THÊM DÒNG NÀY

public class PlatformSpecificUI : MonoBehaviour
{
    // Kéo và thả Panel chứa các nút điều khiển di động vào đây từ Inspector
    // (Chỉ gán cái này nếu script được đặt trong SceneGameplay - SceneLong)
    public GameObject mobileControlsPanel;

    // Kéo và thả component Button của nút "2 Player" vào đây từ Inspector
    // (Chỉ gán cái này nếu script được đặt trong SceneMenu)
    public Button twoPlayerButton; // <-- THÊM BIẾN NÀY

    void Start()
    {
        // Gọi hàm kiểm tra và điều chỉnh UI khi Scene bắt đầu
        CheckPlatformAndAdjustUI();
    }

    void CheckPlatformAndAdjustUI() // <-- Nên gói logic vào một hàm riêng để dễ đọc
    {
        // Kiểm tra nền tảng hiện tại
        // Enum Application.platform liệt kê các nền tảng khác nhau
        // Link tham khảo: https://docs.unity3d.com/ScriptReference/Application-platform.html

        if (Application.platform == RuntimePlatform.Android ||
            Application.platform == RuntimePlatform.IPhonePlayer)
        {
            // --- Nếu đang chạy trên Android hoặc iOS (Điện thoại) ---

            // Nếu mobileControlsPanel được gán, hiển thị nó
            if (mobileControlsPanel != null)
            {
                mobileControlsPanel.SetActive(true); // Hiển thị panel điều khiển
                Debug.Log("Mobile Controls Panel Enabled for Mobile Platform."); // Để debug
            }

            // Nếu twoPlayerButton được gán, VÔ HIỆU HÓA nó
            if (twoPlayerButton != null)
            {
                twoPlayerButton.interactable = false; // <-- VÔ HIỆU HÓA NÚT
                // Hoặc bạn có thể ẩn nó hoàn toàn nếu muốn:
                // twoPlayerButton.gameObject.SetActive(false);
                Debug.Log("2 Player Button Disabled for Mobile Platform."); // Để debug
            }
        }
        else
        {
            // --- Nếu đang chạy trên các nền tảng khác (Máy tính: Windows, Mac, Linux) ---

            // Nếu mobileControlsPanel được gán, ẩn nó
            if (mobileControlsPanel != null)
            {
                mobileControlsPanel.SetActive(false); // Ẩn panel điều khiển
                Debug.Log("Mobile Controls Panel Disabled for PC Platform."); // Để debug
            }

            // Nếu twoPlayerButton được gán, KÍCH HOẠT nó
            if (twoPlayerButton != null)
            {
                twoPlayerButton.interactable = true; // <-- KÍCH HOẠT NÚT
                // Đảm bảo nó được hiển thị nếu bạn đã chọn ẩn nó ở trên
                // twoPlayerButton.gameObject.SetActive(true);
                Debug.Log("2 Player Button Enabled for PC Platform."); // Để debug
            }
        }
    }
}
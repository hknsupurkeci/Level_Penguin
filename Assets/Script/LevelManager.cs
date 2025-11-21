using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [System.Serializable]
    public class LevelSection
    {
        public GameObject levelObject;
        public float minY; // Bu seviyenin başlangıç Y pozisyonu
        public float maxY; // Bu seviyenin bitiş Y pozisyonu
        public bool isActive;
    }

    public List<LevelSection> levelSections = new List<LevelSection>();
    public Transform player;

    [Header("Kaç seviye yukarı/aşağı aktif olsun?")]
    [Tooltip("Yukarı: Oyuncunun üstündeki seviyeler")]
    public int levelRangeUp = 1; // Yukarı kaç seviye aktif olacak
    [Tooltip("Aşağı: Oyuncunun altındaki seviyeler (düşme için)")]
    public int levelRangeDown = 2; // Aşağı kaç seviye aktif olacak

    [Header("Sabit Seviyeler")]
    [Tooltip("İlk level (Level 0) her zaman aktif kalır")]
    public bool keepFirstLevelActive = true; // İlk level hiç kapanmasın

    [Header("Güncelleme Ayarları")]
    [Tooltip("Saniyede kaç kez kontrol edilsin (düşük = daha stabil)")]
    public float updateInterval = 0.2f; // 0.2 saniyede bir kontrol et (flickering önlemek için)

    private int lastCurrentLevel = -1;
    private float nextUpdateTime = 0f;

    private void Start()
    {
        // Başlangıçta tüm seviyeleri deaktif yap
        foreach (var section in levelSections)
        {
            section.levelObject.SetActive(false);
            section.isActive = false;
        }

        // İlk seviyeyi ve yukarısını aktif yap
        for (int i = 0; i <= levelRangeUp && i < levelSections.Count; i++)
        {
            levelSections[i].levelObject.SetActive(true);
            levelSections[i].isActive = true;
        }
    }

    private void Update()
    {
        if (player == null) return;

        // PERFORMANS + STABİLİTE: Her frame yerine belirli aralıklarla kontrol et
        if (Time.time < nextUpdateTime) return;
        nextUpdateTime = Time.time + updateInterval;

        float playerY = player.position.y;

        // Oyuncunun hangi seviyede olduğunu bul
        int currentLevelIndex = -1;
        for (int i = 0; i < levelSections.Count; i++)
        {
            if (playerY >= levelSections[i].minY && playerY <= levelSections[i].maxY)
            {
                currentLevelIndex = i;
                break;
            }
        }

        // Eğer oyuncu seviyeler arasındaysa en yakın olanı bul
        if (currentLevelIndex == -1)
        {
            float minDistance = float.MaxValue;
            for (int i = 0; i < levelSections.Count; i++)
            {
                float sectionCenterY = (levelSections[i].minY + levelSections[i].maxY) / 2f;
                float distance = Mathf.Abs(playerY - sectionCenterY);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    currentLevelIndex = i;
                }
            }
        }

        // ÖNEMLI: Seviye değişmediyse güncelleme yapma (flickering önler)
        if (currentLevelIndex == lastCurrentLevel) return;

        Debug.Log($"Oyuncu Level {lastCurrentLevel} → Level {currentLevelIndex} geçti");

        // Her seviyeyi kontrol et
        for (int i = 0; i < levelSections.Count; i++)
        {
            bool shouldBeActive = false;

            // Mevcut seviye her zaman aktif
            if (i == currentLevelIndex)
            {
                shouldBeActive = true;
            }
            // Yukarıdaki seviyeler (daha az)
            else if (i > currentLevelIndex && i <= currentLevelIndex + levelRangeUp)
            {
                shouldBeActive = true;
            }
            // Aşağıdaki seviyeler (daha fazla - düşme için önemli!)
            else if (i < currentLevelIndex && i >= currentLevelIndex - levelRangeDown)
            {
                shouldBeActive = true;
            }

            // Aktivasyon durumunu güncelle (sadece değişiklik varsa)
            if (shouldBeActive && !levelSections[i].isActive)
            {
                levelSections[i].levelObject.SetActive(true);
                levelSections[i].isActive = true;
                Debug.Log($"Level {i} aktif edildi");
            }
            else if (!shouldBeActive && levelSections[i].isActive)
            {
                levelSections[i].levelObject.SetActive(false);
                levelSections[i].isActive = false;
                Debug.Log($"Level {i} deaktif edildi");
            }
        }

        lastCurrentLevel = currentLevelIndex;
    }

    // Inspector'dan seviyeleri otomatik ayarlamak için
    [ContextMenu("Auto Setup Level Sections")]
    public void AutoSetupLevelSections()
    {
        levelSections.Clear();

        // Her child objesini bir seviye olarak ekle
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            Renderer[] renderers = child.GetComponentsInChildren<Renderer>();

            if (renderers.Length > 0)
            {
                // Seviyenin bounds'ını hesapla
                Bounds bounds = renderers[0].bounds;
                foreach (var renderer in renderers)
                {
                    bounds.Encapsulate(renderer.bounds);
                }

                LevelSection section = new LevelSection
                {
                    levelObject = child,
                    minY = bounds.min.y,
                    maxY = bounds.max.y,
                    isActive = false
                };

                levelSections.Add(section);
            }
        }

        Debug.Log($"{levelSections.Count} seviye eklendi!");
    }
}
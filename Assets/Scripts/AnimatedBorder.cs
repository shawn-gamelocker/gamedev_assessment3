using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimatedBorder : MonoBehaviour
{
    [Header("Border Settings")]
    [SerializeField] private GameObject dotPrefab; // Assign a UI Image as prefab
    [SerializeField] private int numberOfDots = 20;
    [SerializeField] private float dotSize = 10f;
    [SerializeField] private float speed = 50f; // Units per second
    [SerializeField] private Color dotColor = Color.white;
    
    private RectTransform rectTransform;
    private List<RectTransform> dots = new List<RectTransform>();
    private List<float> dotProgress = new List<float>(); // 0 to 1 around the border
    
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        CreateDots();
    }
    
    void CreateDots()
    {
        for (int i = 0; i < numberOfDots; i++)
        {
            GameObject dot;
            
            // Create dot (either from prefab or create new)
            if (dotPrefab != null)
            {
                dot = Instantiate(dotPrefab, transform);
            }
            else
            {
                dot = new GameObject($"Dot_{i}");
                dot.transform.SetParent(transform);
                Image img = dot.AddComponent<Image>();
                img.color = dotColor;
            }
            
            RectTransform dotRect = dot.GetComponent<RectTransform>();
            dotRect.sizeDelta = new Vector2(dotSize, dotSize);
            dotRect.anchorMin = new Vector2(0.5f, 0.5f);
            dotRect.anchorMax = new Vector2(0.5f, 0.5f);
            dotRect.pivot = new Vector2(0.5f, 0.5f);
            
            dots.Add(dotRect);
            dotProgress.Add(i / (float)numberOfDots); // Evenly distribute
        }
    }
    
    void Update()
    {
        if (rectTransform == null) return;
        
        float perimeter = GetPerimeter();
        float normalizedSpeed = speed / perimeter;
        
        for (int i = 0; i < dots.Count; i++)
        {
            // Update progress
            dotProgress[i] += normalizedSpeed * Time.deltaTime;
            if (dotProgress[i] > 1f) dotProgress[i] -= 1f;
            
            // Set position based on progress
            dots[i].anchoredPosition = GetPositionOnBorder(dotProgress[i]);
        }
    }
    
    float GetPerimeter()
    {
        Rect rect = rectTransform.rect;
        return 2 * (rect.width + rect.height);
    }
    
    Vector2 GetPositionOnBorder(float progress)
    {
        Rect rect = rectTransform.rect;
        float width = rect.width;
        float height = rect.height;
        float perimeter = 2 * (width + height);
        
        float distance = progress * perimeter;
        
        // Top edge (left to right)
        if (distance < width)
        {
            return new Vector2(
                -width / 2 + distance,
                height / 2
            );
        }
        distance -= width;
        
        // Right edge (top to bottom)
        if (distance < height)
        {
            return new Vector2(
                width / 2,
                height / 2 - distance
            );
        }
        distance -= height;
        
        // Bottom edge (right to left)
        if (distance < width)
        {
            return new Vector2(
                width / 2 - distance,
                -height / 2
            );
        }
        distance -= width;
        
        // Left edge (bottom to top)
        return new Vector2(
            -width / 2,
            -height / 2 + distance
        );
    }
}

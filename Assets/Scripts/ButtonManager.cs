using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ButtonManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [Header("Button Settings")]
    [SerializeField] private string sceneName; // Set to "Week3Scene" for Level 1
    [SerializeField] private bool isInteractable = true; // Set to false for Level 2
    
    [Header("Visual Feedback")]
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color highlightColor = Color.yellow;
    [SerializeField] private Color pressedColor = Color.gray;
    [SerializeField] private Color disabledColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);
    [SerializeField] private float scaleOnHighlight = 1.1f;
    
    [Header("Audio Feedback")]
    [SerializeField] private AudioClip hoverSound;
    [SerializeField] private AudioClip clickSound;
    [SerializeField] private AudioSource audioSource;
    
    private Image buttonImage;
    private Vector3 originalScale;
    private bool isPressed = false;
    
    void Start()
    {
        buttonImage = GetComponent<Image>();
        originalScale = transform.localScale;
        
        // Create audio source if not assigned
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
        }
        
        // Set initial color
        if (!isInteractable)
        {
            buttonImage.color = disabledColor;
        }
        else
        {
            buttonImage.color = normalColor;
        }
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isInteractable) return;
        
        buttonImage.color = highlightColor;
        transform.localScale = originalScale * scaleOnHighlight;
        
        if (hoverSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(hoverSound);
        }
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isInteractable) return;
        
        if (!isPressed)
        {
            buttonImage.color = normalColor;
            transform.localScale = originalScale;
        }
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isInteractable) return;
        
        isPressed = true;
        buttonImage.color = pressedColor;
        transform.localScale = originalScale * 0.95f;
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        if (!isInteractable) return;
        
        isPressed = false;
        buttonImage.color = highlightColor;
        transform.localScale = originalScale * scaleOnHighlight;
        
        if (clickSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
        
        // Load scene if specified
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
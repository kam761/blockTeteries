using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] private int x;
    [SerializeField] private int y;

    
    private Image icon;

    private void Start()
    {
        icon = GetComponent<Image>();
    }


}

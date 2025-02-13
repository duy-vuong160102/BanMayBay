using UnityEngine;
public class ScrollingBackground : MonoBehaviour
{
    private MeshRenderer m_Renderer;
    [SerializeField] private float scrollSpeed;
    // Start is called before the first frame update
    void Start()
    {
        m_Renderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Scroll();
    }
    void Scroll()
    {
        Vector2 textureOffset = new Vector2(0, Time.time * scrollSpeed);
        m_Renderer.material.mainTextureOffset = textureOffset;
    }
}


using UnityEngine;
using UnityEngine.UI;

public class ChangeColor : MonoBehaviour
{
    [SerializeField] Image ColorPalette;

    [SerializeField] Image ColorThumb;

    private Vector2 offset;

    [SerializeField] Image[] ColorsUI;

    public Color[] preColor;

    private int numColor;

    private int SelectColor;

    private Renderer cr;

    void Start()
    {

        cr = GetComponent<Renderer>();

        ClickColor();
    }

    public void ClickColor()
    {
        cr.material.SetColor("_Color", preColor[numColor]);

        if (numColor < preColor.Length-1)
            numColor++;
        else
            numColor = 0;
    }

    public void SelectToChangeColor(int Clr)
    {
        SelectColor = Clr;
    }

    public void ChangheColor()
    {
        Vector2 center = ColorPalette.transform.position;
        Vector2 position = Input.mousePosition;

        if (position.x < center.x + ColorPalette.GetComponent<CircleCollider2D>().radius  && position.x > center.x - ColorPalette.GetComponent<CircleCollider2D>().radius && position.y < center.y + ColorPalette.GetComponent<CircleCollider2D>().radius && position.y > center.y - ColorPalette.GetComponent<CircleCollider2D>().radius)
            offset = position - center;

        Vector2 Set = Vector2.ClampMagnitude(offset, ColorPalette.GetComponent<CircleCollider2D>().radius);

        Vector3 newPos = center + Set;

        if (ColorThumb.transform.position != newPos)
        {
            ColorThumb.transform.position = newPos;

            ColorsUI[SelectColor].color = GetColor();

            preColor[SelectColor] = GetColor();
        }

        
    }

    private Color GetColor()
    {
        Vector2 spectrumScreenPosition = ColorPalette.transform.position;
        Vector2 ThumbScreenPosition = ColorThumb.transform.position;
        Vector2 SpectrumXY = new Vector2(ColorPalette.GetComponent<RectTransform>().rect.width, ColorPalette.GetComponent<RectTransform>().rect.height);
        Vector2 position = ThumbScreenPosition - spectrumScreenPosition + SpectrumXY * 0.5f;

        Texture2D texture = ColorPalette.mainTexture as Texture2D;

        position = new Vector2(position.x / ColorPalette.GetComponent<RectTransform>().rect.width, position.y / ColorPalette.GetComponent<RectTransform>().rect.height);

        Color SelectedColor = texture.GetPixelBilinear(position.x, position.y);
        SelectedColor.a = 1;
        return SelectedColor;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

}

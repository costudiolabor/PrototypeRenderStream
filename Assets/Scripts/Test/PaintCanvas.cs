using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class PaintCanvas : MonoBehaviour
{
    RectTransform rectTransform;
    RawImage rawImage;
    Vector3 bottomLeft = Vector3.zero;
    Vector3 topRight = Vector3.zero;
    Texture2D canvas;

    private Vector2Int lastPositionMouse;

    int width = 0;
    int height = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Getting the RectTransform, since this is a RawImage, which exists on the canvas and should have a rect transform
        rectTransform = GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            GetWorldCorners();
        }

        // RawImage that we are going to be updating for our paint application.
        rawImage = GetComponent<RawImage>();
        if (rawImage != null)
        {
            CreateTexture2D();
        }

        //
       // DrawLine(new Vector2Int(0, 0), new Vector2Int(1000, 100));
    }

    // Update is called once per frame
    void Update()
    {
        // Make sure our stuff is valid
        if (rectTransform != null)
        {
            if (rawImage != null)
            {
                HandleInput();
            }
        }
    }

    void HandleInput()
    {
        // Since we can only paint on the canvas if the mouse button is press
        // May be best to revise this so the tool has a call back for example a 
        // fill tool selected would call its own "Handle" method,

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
        {
            Vector2Int mousePos = Vector2Int.zero;
            // We have input, lets convert the mouse position to be relative to the canvas
            ConvertMousePosition(ref mousePos);

            // Checking that our mouse is in bounds, which is stored in our height and width variable and as long as it has a "positive value"
            if (MouseIsInBounds(mousePos))
            {
                // This method could be removed to be the tool method I mention above
                // you would pass in the mousePosition, and color similar to this.
                // This way each tool would be its "own" component that would be activated
                // through some form of UI.


                //PaintTexture(mousePos, Color.red); // Also the color you want would be here to...
                if (lastPositionMouse != mousePos)
                {
                    DrawLine(lastPositionMouse, mousePos);
                    lastPositionMouse = mousePos;
                    Debug.Log(lastPositionMouse + "  " + mousePos);
                }
            }
        }
    }

    void PaintTexture(Vector2Int pos, Color color)
    {
        // In our method we don't allow transparency and we are just replacing the pixel,
        canvas.SetPixel(pos.x, pos.y, color);
        // Applying out change, we dont want to mip levels.
        // If you are doing some blending or transparency stuff that would be handled by your tool
        canvas.Apply(false);
    }

    bool MouseIsInBounds(Vector2Int mousePos)
    {
        // The position is already relative to the texture so if it is >= to 0 and less then the texture
        // width and height it is in bounds.
        if (mousePos.x >= 0 && mousePos.x < width)
        {
            if (mousePos.y >= 0 && mousePos.y < height)
            {
                return true;
            }
        }

        return false;
    }

    void ConvertMousePosition(ref Vector2Int mouseOut)
    {
        // The mouse Position, and the RawImage position are returned in the same space
        // So we can just update based off of that
        mouseOut.x = Mathf.RoundToInt(Input.mousePosition.x - bottomLeft.x);
        mouseOut.y = Mathf.RoundToInt(Input.mousePosition.y - bottomLeft.y);
    }

    void CreateTexture2D()
    {
        // Creating our "Draw" texture to be the same size as our RawImage.
        width = Mathf.RoundToInt(topRight.x - bottomLeft.x);
        height = Mathf.RoundToInt(topRight.y - bottomLeft.y);
        canvas = new Texture2D(width, height);
        rawImage.texture = canvas;
    }

    void GetWorldCorners()
    {
        if (rectTransform != null)
        {
            Vector3[] corners = new Vector3[4];
            rectTransform.GetWorldCorners(corners);

            // Setting our corners  based on the fact GetCorners returns them in clockwise order starting from BL TL TR BR.
            bottomLeft = corners[0];
            topRight = corners[2];
        }
    }


    private void DrawLine(Vector2Int startPosition, Vector2Int endPosition)
    {
        //y=y0+[(y1-y0)/(x1-x0)]*(x-x0)
        
        for (var x = lastPositionMouse.x; x < endPosition.x; x++)
        {
            var y = startPosition.y + ((endPosition.y - startPosition.y) / (endPosition.x - startPosition.x)) * (x - startPosition.x);
            PaintTexture(new Vector2Int(x, y), Color.red);
            Debug.Log(new Vector2Int(x, y));
        }
    }
}
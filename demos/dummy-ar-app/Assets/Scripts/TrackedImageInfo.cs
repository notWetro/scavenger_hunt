using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using ZXing;

public class TrackedImageInfo : MonoBehaviour
{
    [SerializeField]
    private ARTrackedImageManager m_TrackedImageManager;
    private readonly IBarcodeReader _reader = new BarcodeReader();

    private void OnEnable() => m_TrackedImageManager.trackedImagesChanged += OnChanged;

    private void OnDisable() => m_TrackedImageManager.trackedImagesChanged -= OnChanged;

    private void OnChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var newImage in eventArgs.added)
        {
            // Handle added event
        }

        foreach (var updatedImage in eventArgs.updated)
        {
            // Handle updated event
        }

        foreach (var removedImage in eventArgs.removed)
        {
            // Handle removed event
        }
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space))
            ListAllImages();
    }

    private void ListAllImages()
    {
        Debug.Log($"There are {m_TrackedImageManager.trackables.count} images being tracked.");

        //foreach (var trackedImage in m_TrackedImageManager.trackables)
        //{
        //    var reference = trackedImage.referenceImage;
        //    var imageTexture = reference.texture;

        //    if(imageTexture is not null)
        //    {
        //        Debug.Log(imageTexture.name);

        //        imageTexture.ReadPixels(new Rect(Vector2.zero, new Vector2(imageTexture.width, imageTexture.height)), 0, 0);

        //        // Decode the QR code image
        //        var result = _reader.Decode(imageTexture.GetPixels32(), imageTexture.width, imageTexture.height);

        //        if (result is null)
        //        {
        //            Debug.Log("QR Code Text: " + result.Text);
        //        }
        //        else
        //        {
        //            Debug.Log("QR Code not detected or could not be decoded.");
        //        }

        //        Debug.Log($"Image: {trackedImage.referenceImage.name} is at " +
        //                  $"{trackedImage.transform.position}");
        //    }
        //}

        foreach (var trackedImage in m_TrackedImageManager.trackables)
        {
            if (trackedImage.referenceImage == null)
            {
                Debug.LogError("Unable to retrieve image reference for tracked image.");
                continue;
            }

            var reference = trackedImage.referenceImage;
            var imageTexture = reference.texture;

            if (imageTexture != null)
            {
                Debug.Log(imageTexture.name);

                // Ensure that pixel reading is done after rendering
                StartCoroutine(ReadPixelsAfterRender(imageTexture, trackedImage));
            }
        }
    }

    private IEnumerator ReadPixelsAfterRender(Texture2D imageTexture, ARTrackedImage trackedImage)
    {
        // Wait for the end of the frame to ensure rendering is complete
        yield return new WaitForEndOfFrame();

        // Read pixels from the texture
        imageTexture.ReadPixels(new Rect(Vector2.zero, new Vector2(imageTexture.width, imageTexture.height)), 0, 0);
        imageTexture.Apply(); // Apply changes to the texture

        // Decode the QR code image
        var result = _reader.Decode(imageTexture.GetPixels32(), imageTexture.width, imageTexture.height);

        if (result != null)
        {
            Debug.Log("QR Code Text: " + result.Text);
        }
        else
        {
            Debug.Log("QR Code not detected or could not be decoded.");
        }

        Debug.Log($"Image: {trackedImage.referenceImage.name} is at " +
                  $"{trackedImage.transform.position}");
    }
}

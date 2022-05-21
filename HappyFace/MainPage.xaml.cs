using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;

namespace HappyFace;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnCounterClicked(object sender, EventArgs e)
    {
        //var photo = await MediaPicker.PickPhotoAsync();
        var photo = await MediaPicker.CapturePhotoAsync();

        var credentials = new ApiKeyServiceClientCredentials(Constants.SubscriptionKey);
        var faceClient = new FaceClient(credentials) { Endpoint = Constants.Endpoint };
        var url = "https://csdx.blob.core.windows.net/resources/Face/Images/Family1-Dad2.jpg";
        var faceAttributes = new FaceAttributeType[] { FaceAttributeType.Smile };

        using FileStream fs = File.OpenRead(photo.FullPath);
        var detectResult = await faceClient.Face.DetectWithStreamAsync(fs, returnFaceAttributes: faceAttributes);
        var faceResult = detectResult.FirstOrDefault();
        if (faceResult is null) return;
        var smileScale = faceResult.FaceAttributes.Smile;

        HappyScaleLabel.Text = $"You are {smileScale * 100}% happy!";

        //var detectResult = await faceClient.Face.DetectWithUrlAsync(url, returnFaceAttributes: faceAttributes);
        //foreach (var face in detectResult)
        //{
        //    var attr = face.FaceAttributes;
        //}
    }
}

using QRCoder;

namespace QRCG.Core
{
    public static class TextQr
    {
        public static bool FromTextToQr(string text, string path)
        {
            var qrGenerator = new QRCodeGenerator();
            var qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);

            BitmapByteQRCode qrCodeImage = new(qrCodeData);
            var imageBytes = qrCodeImage.GetGraphic(20);

            File.WriteAllBytes(path, imageBytes);

            return true;
        }
    }
}

using Cocona;
using QRCG.Core;

CoconaApp.Run(([Option('t')]string text) =>
{
    Console.WriteLine("Creating QR-Code from Text.");
    Console.WriteLine($"Input: '{text}'");

    string downloadsFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads\\";
    string fileName = "QRCode_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";
    string filePath = Path.Combine(downloadsFolder, fileName);

    try
    {
        var res = TextQr.FromTextToQr(text, filePath);

        if (res)
            Console.WriteLine($"Output: '{filePath}'");
        else
            Console.WriteLine("Conversion failed.");
    }
    catch(Exception ex)
    {
        Console.WriteLine(ex.Message, "Conversion failed.");
    }
});

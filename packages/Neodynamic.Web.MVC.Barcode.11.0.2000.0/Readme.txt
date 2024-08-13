Using Barcode Professional in ASP.NET MVC
=========================================
There are two ways to render barcode images in your Views, one is through the 
BarcodeHtmlHelper class where the barcode image is embedded into the HTML 
returned back to the browser; and the other one is by writing a Controller 
from where you instantiate BarcodeProfessional class to generate the desired
barcode image that will be displayed in the View through an HTML IMG tag.

Barcode Professional can generate most popular Linear (1D), Postal, Component
Composite, MICR & 2D Barcode Symbologies including Code 39, Code 128, GS1-128
GS1 DataBar (RSS-14), EAN 13 & UPC, ISBN, ISBT-128, Postal (USPS, British 
Royal Mail, Mailmark, Australia Post, DHL, FedEx), Data Matrix, QR Code, PDF 417, Aztec
Code, UPS MaxiCode, Chinese Han Xin Code, IFA PPN, MICR E-13-B Characters,
all EAN/UPC Composite Barcodes (CC-A, CC-B & CC-C), DotCode and many more barcode 
standards (http://neodynamic.com/barcodes)

If you need further assistance, please contact our team at 
https://neodynamic.com/support


Using BarcodeHtmlHelper class
=============================

1. In your ASP.NET MVC project, add a reference to 
Neodynamic.Web.MVC.Barcode.dll 

2. Then, in the View where you'd like to display the barcode image, add the 
following using statement at the top:

@using Neodynamic.Web.MVC.Barcode

3. Now, in the View's content, use the BarcodeHtmlHelper.EmbedBarcodeImage 
method to embed the barcode image. In this snipped code, the barcode type,
a.k.a. Symbology, will be QRCode and the value to encode is ABC12345

@Html.Raw(BarcodeHtmlHelper.EmbedBarcodeImage("S="+Symbology.QRCode.ToString()
+"&C=ABC12345", "Sample Barcode", "", ""))

NOTE: Please refer to the Barcode HTML Helper Settings for further details on 
how to contruct the parameter string for BarcodeHtmlHelper.EmbedBarcodeImage 
http://neodynamic.com/Products/Help/BarcodeAspNet110/index.html

4. That's it! Just view the page on a browser and a QR Code symbol will be 
displayed.

NOTE: The BarcodeHtmlHelper.EmbedBarcodeImage method creates and embeds a 
barcode  image in PNG format by leveraging browsers Base64 Data URI feature. 
If you do not want to embed the barcode image or need to render the barcode 
in  another image format like (jpg, gif, etc), then please refer to 
"Using BarcodeProfessional class in a Controller"


Using BarcodeProfessional class in a Controller
===============================================

1. In your ASP.NET MVC project, add a reference to 
Neodynamic.Web.MVC.Barcode.dll

2. Then, create a new Controller called BarcodeGen and paste the following 
code. In this sample code, we create an instance of BarcodeProfessional class 
to create a QR Code based on the specified value to encode:

C#
public void GetBarcodeImage(string valueToEncode)
{
    //Create an instance of BarcodeProfessional class
    using (Neodynamic.Web.MVC.Barcode.BarcodeProfessional bcp = new 
                    Neodynamic.Web.MVC.Barcode.BarcodeProfessional())
    { 
        //Set the desired barcode type or symbology
        bcp.Symbology = Neodynamic.Web.MVC.Barcode.Symbology.QRCode;
        //Set value to encode
        bcp.Code = valueToEncode;
        //Generate barcode image
        byte[] imgBuffer = bcp.GetBarcodeImage(
                     System.Drawing.Imaging.ImageFormat.Png);
        //Write image buffer to Response obj
        System.Web.HttpContext.Current.Response.ContentType = "image/png";
        System.Web.HttpContext.Current.Response.BinaryWrite(imgBuffer);
    }
}

VB.NET
Public Sub GetBarcodeImage(valueToEncode As String)
	'Create an instance of BarcodeProfessional class
	Using bcp As New Neodynamic.Web.MVC.Barcode.BarcodeProfessional()
		'Set the desired barcode type or symbology
		bcp.Symbology = Neodynamic.Web.MVC.Barcode.Symbology.QRCode
		'Set value to encode
		bcp.Code = valueToEncode
		'Generate barcode image
		Dim imgBuffer As Byte() = bcp.GetBarcodeImage(
		                        System.Drawing.Imaging.ImageFormat.Png)
		'Write image buffer to Response obj
		System.Web.HttpContext.Current.Response.ContentType = "image/png"
		System.Web.HttpContext.Current.Response.BinaryWrite(imgBuffer)
	End Using
End Sub

3. Now, in the View, we'll use an IMG tag pointing to the GetBarcodeImage 
method of the BarcodeGen Controller by specifying which value we'd like to
encode into a QR Code image. For instance:

<img src='@Url.Action("GetBarcodeImage", "BarcodeGen", new 
 {valueToEncode="ABC12345"}, Request.Url.Scheme)' alt='Sample Barcode' />

4. That's it! Just view the page on a browser and a QR Code symbol will be 
displayed.
using System.IO;
using SelectPdf;
using System.Net;
using System.Web.Http;
using System.Net.Http.Headers;

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log)
{
    HtmlToPdf pdfConverter = new HtmlToPdf();
    string content = @"<html><body><div style=""width=50%;border=1px solid darkgray"">Sample Certificate</div><hr/><div><p>More content here</p></div></body></html>";
    var outPdf = pdfConverter.ConvertHtmlString(content);
    Stream stream = new MemoryStream(outPdf.Save());

    var r = new HttpResponseMessage();
    r.StatusCode = HttpStatusCode.OK;
    r.Content = new StreamContent(stream);
    r.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
    {
        FileName = "foo.pdf"
    };
    return r;

}

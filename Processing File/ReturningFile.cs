public FileResult GetFile(string fileName)
{
    // Force the pdf document to be displayed in the browser
    Response.AppendHeader("Content-Disposition", "inline; filename=" + fileName + ";");
 
    string path = AppDomain.CurrentDomain.BaseDirectory + "App_Data/";            
    return File(path + fileName, System.Net.Mime.MediaTypeNames.Application.Pdf, fileName);
}
-----------------------------------------------------------------------------------------
public FileResult GetFile(string fileName)
{
    string path = AppDomain.CurrentDomain.BaseDirectory + "App_Data/";            
    return File(path + fileName, System.Net.Mime.MediaTypeNames.Application.Pdf, fileName);
}
----------------------------------------------------------------------------------------
public ActionResult Download()
{
  var fileStream = new FileStream(@"c:\PATH\TO\PDF\ON\THE\SERVER.pdf", FileMode.Open);
  var mimeType = "application/pdf";
  var fileDownloadName = "download.pdf";
  return File(fileStream, mimeType, fileDownloadName);
}
----------------------------------------------------------------------------------------
<h2>Basic File Upload</h2>  
@using (Html.BeginForm ("Index",  
                        "Home",  
                        FormMethod.Post,  
                        new { enctype = "multipart/form-data" }))  
{                    
    <label for="file">Upload Image:</label>  
    <input type="file" name="file" id="file"/><br><br>  
    <input type="submit" value="Upload Image"/>  
    <br><br>  
    @ViewBag.Message  
}
--------------------------------------------------------------------------------------
[HttpPost]  
public ActionResult Index(HttpPostedFileBase file)  
{  
    if (file != null && file.ContentLength > 0)  
        try 
        {  
            string path = Path.Combine(Server.MapPath("~/Images"),  
                                       Path.GetFileName(file.FileName));  
            file.SaveAs(path);  
            ViewBag.Message = "File uploaded successfully";  
        }  
        catch (Exception ex)  
        {  
            ViewBag.Message = "ERROR:" + ex.Message.ToString();  
        }  
    else 
    {  
        ViewBag.Message = "You have not specified a file.";  
    }  
    return View();  
}
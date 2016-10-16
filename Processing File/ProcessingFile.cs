@using (Html.BeginForm("Upload", "Home", FormMethod.Post, 
    new { enctype = "multipart/form-data" } )) 
 {
    @Html.AntiForgeryToken()

    <fieldset>
        <legend>Upload a file</legend>
        <div class="editor-field">
            @Html.TextBox("file", "", new { type = "file" })    
        </div>
       <div class="editor-field">
            <input type="submit" value="Upload" />
        </div>
    </fieldset>
}
---------------------------------------------------------------------
    @using(Html.BeginForm("Upload", "Home", FormMethod.Post,
    new { enctype = "multipart/form-data" } )) 
 {
    @Html.AntiForgeryToken()

    <fieldset>
        <legend>Upload a file</legend>
        <div class="editor-field">
            @Html.TextBox("file", "", new { type = "file" })    
        </div>
       <div class="editor-field">
            <input type = "submit" value="Upload" />
        </div>
    </fieldset>
}
----------------------------------------------------------------------
[HttpPost]
public ActionResult Upload(HttpPostedFileBase file)
{
    try
    {
        if (file.ContentLength > 0)
        {
            var fileName = Path.GetFileName(file.FileName);
            var path = Path.Combine(Server.MapPath("~/App_Data/Images"), fileName);
            file.SaveAs(path);
        }
        ViewBag.Message = "Upload successful";
        return RedirectToAction("Index");
    }
    catch
    {
        ViewBag.Message = "Upload failed";
        return RedirectToAction("Uploads");
    }
}
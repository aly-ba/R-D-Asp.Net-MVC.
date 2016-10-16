Controller - PartialViewC#

PUBLIC PARTIALVIEWRESULT CREATEPARTIALVIEW()
{
    RETURN PARTIALVIEW("MYPARTIALVIEW");
}

public PartialViewResult CreatePartialView()
{
    return PartialView("MyPartialView");
}

------------------------------------------------------
<p>
    This is a very nice web page.
</p>

-------------------------------------------------------------
1
<button id="NewBrowserBtn">New Browser</button>
------------------------------------------------------------

$(document).ready(function () {
    $('#NewBrowserBtn').on('click', function () {
        window.open('/Home/CreatePartialView', '_blank', 'left=100,top=100,width=400,height=300,toolbar=1,resizable=0');
    });
});
---------------------------------------------------------
<input type="button" id="btnpopu" value="Open Modeless popup" onclick="ShowPopup();" />
<script type="text/javascript">
 
    ShowPopup = function () {
        window.open('/Home/OpenPopup', "PopupWindow", 'width=400px,height=400px,top=150,left=250');
    }
  
    </script>
----------------------------------------------------------
public string OpenPopup()
    {
        return "<h1> This Is Modeless Popup Window</h1>";
    }
----------------------------------------------------------
<input type="button" id="btnpopup" value="Open Modal Popup" onclick="ShowModelPopUp();" />

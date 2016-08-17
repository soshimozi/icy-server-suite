AjaxPro.onLoading = function(b) {
	window.status = b ? "Loading..." : "";
	
//	if( !g_HideLoadDiv )
//	{
//	    $("loading").style.display = b ? "inline" : "none";
//    }
//    else
//    {
//	    $("loading").style.display = "none";
//    }
}


AjaxPro.onError = function() {
    $("divListContent").innerText = "An error occured while loading playlist.";
    $("divListContent").className = "error";
}

AjaxPro.onTimeout = function() {
    $("divListContent").innerText = "The server timed out while loading playlist.";
    $("divListContent").className = "error";
}
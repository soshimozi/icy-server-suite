// Author: Matt Kruse <matt@mattkruse.com>
// WWW: http://www.mattkruse.com/

/* 
Modified by: Robert Darrell - http://www.ironspider.ca

Modifications made: 
	1) Applied background color to DHTML DIV to keep anything from showing through in between table cells.
	2) All hexadecimal color codes were changed into recognized color names (140 in total).
	3) Number of columns in color table changed to 10 (width = 18 changed to width = 10)
	4) Colspans on two bottom table row cells (preview and color name) changed to 1 and 9 respectively
	5) class attribute added to table to allow sizing of text of color name
	6) Function previewChoices added to allow for instant and separate preview of each instance of ColorPicker
	7) Left margin of Popup DIV set to -160px (find 'writeDiv')

*/

/* SOURCE FILE: AnchorPosition.js */
function getAnchorPosition(anchorname){var useWindow=false;var coordinates=new Object();var x=0,y=0;var use_gebi=false, use_css=false, use_layers=false;if(document.getElementById){use_gebi=true;}else if(document.all){use_css=true;}else if(document.layers){use_layers=true;}if(use_gebi && document.all){x=AnchorPosition_getPageOffsetLeft(document.all[anchorname]);y=AnchorPosition_getPageOffsetTop(document.all[anchorname]);}else if(use_gebi){var o=document.getElementById(anchorname);x=AnchorPosition_getPageOffsetLeft(o);y=AnchorPosition_getPageOffsetTop(o);}else if(use_css){x=AnchorPosition_getPageOffsetLeft(document.all[anchorname]);y=AnchorPosition_getPageOffsetTop(document.all[anchorname]);}else if(use_layers){var found=0;for(var i=0;i<document.anchors.length;i++){if(document.anchors[i].name==anchorname){found=1;break;}}if(found==0){coordinates.x=0;coordinates.y=0;return coordinates;}x=document.anchors[i].x;y=document.anchors[i].y;}else{coordinates.x=0;coordinates.y=0;return coordinates;}coordinates.x=x;coordinates.y=y;return coordinates;}
function getAnchorWindowPosition(anchorname){var coordinates=getAnchorPosition(anchorname);var x=0;var y=0;if(document.getElementById){if(isNaN(window.screenX)){x=coordinates.x-document.body.scrollLeft+window.screenLeft;y=coordinates.y-document.body.scrollTop+window.screenTop;}else{x=coordinates.x+window.screenX+(window.outerWidth-window.innerWidth)-window.pageXOffset;y=coordinates.y+window.screenY+(window.outerHeight-24-window.innerHeight)-window.pageYOffset;}}else if(document.all){x=coordinates.x-document.body.scrollLeft+window.screenLeft;y=coordinates.y-document.body.scrollTop+window.screenTop;}else if(document.layers){x=coordinates.x+window.screenX+(window.outerWidth-window.innerWidth)-window.pageXOffset;y=coordinates.y+window.screenY+(window.outerHeight-24-window.innerHeight)-window.pageYOffset;}coordinates.x=x;coordinates.y=y;return coordinates;}
function AnchorPosition_getPageOffsetLeft(el){var ol=el.offsetLeft;while((el=el.offsetParent) != null){ol += el.offsetLeft;}return ol;}
function AnchorPosition_getWindowOffsetLeft(el){return AnchorPosition_getPageOffsetLeft(el)-document.body.scrollLeft;}
function AnchorPosition_getPageOffsetTop(el){var ot=el.offsetTop;while((el=el.offsetParent) != null){ot += el.offsetTop;}return ot;}
function AnchorPosition_getWindowOffsetTop(el){return AnchorPosition_getPageOffsetTop(el)-document.body.scrollTop;}

/* SOURCE FILE: PopupWindow.js */
function PopupWindow_getXYPosition(anchorname){var coordinates;if(this.type == "WINDOW"){coordinates = getAnchorWindowPosition(anchorname);}else{coordinates = getAnchorPosition(anchorname);}this.x = coordinates.x;this.y = coordinates.y;}
function PopupWindow_setSize(width,height){this.width = width;this.height = height;}
function PopupWindow_populate(contents){this.contents = contents;this.populated = false;}
function PopupWindow_setUrl(url){this.url = url;}
function PopupWindow_setWindowProperties(props){this.windowProperties = props;}
function PopupWindow_refresh(){if(this.divName != null){if(this.use_gebi){document.getElementById(this.divName).innerHTML = this.contents;}else if(this.use_css){document.all[this.divName].innerHTML = this.contents;}else if(this.use_layers){var d = document.layers[this.divName];d.document.open();d.document.writeln(this.contents);d.document.close();}}else{if(this.popupWindow != null && !this.popupWindow.closed){if(this.url!=""){this.popupWindow.location.href=this.url;}else{this.popupWindow.document.open();this.popupWindow.document.writeln(this.contents);this.popupWindow.document.close();}this.popupWindow.focus();}}}
function PopupWindow_showPopup(anchorname){this.getXYPosition(anchorname);this.x += this.offsetX;this.y += this.offsetY;if(!this.populated &&(this.contents != "")){this.populated = true;this.refresh();}if(this.divName != null){if(this.use_gebi){document.getElementById(this.divName).style.left = this.x + "px";document.getElementById(this.divName).style.top = this.y;document.getElementById(this.divName).style.visibility = "visible";}else if(this.use_css){document.all[this.divName].style.left = this.x;document.all[this.divName].style.top = this.y;document.all[this.divName].style.visibility = "visible";}else if(this.use_layers){document.layers[this.divName].left = this.x;document.layers[this.divName].top = this.y;document.layers[this.divName].visibility = "visible";}}else{if(this.popupWindow == null || this.popupWindow.closed){if(this.x<0){this.x=0;}if(this.y<0){this.y=0;}if(screen && screen.availHeight){if((this.y + this.height) > screen.availHeight){this.y = screen.availHeight - this.height;}}if(screen && screen.availWidth){if((this.x + this.width) > screen.availWidth){this.x = screen.availWidth - this.width;}}var avoidAboutBlank = window.opera ||( document.layers && !navigator.mimeTypes['*']) || navigator.vendor == 'KDE' ||( document.childNodes && !document.all && !navigator.taintEnabled);this.popupWindow = window.open(avoidAboutBlank?"":"about:blank","window_"+anchorname,this.windowProperties+",width="+this.width+",height="+this.height+",screenX="+this.x+",left="+this.x+",screenY="+this.y+",top="+this.y+"");}this.refresh();}}
function PopupWindow_hidePopup(){if(this.divName != null){if(this.use_gebi){document.getElementById(this.divName).style.visibility = "hidden";}else if(this.use_css){document.all[this.divName].style.visibility = "hidden";}else if(this.use_layers){document.layers[this.divName].visibility = "hidden";}}else{if(this.popupWindow && !this.popupWindow.closed){this.popupWindow.close();this.popupWindow = null;}}}
function PopupWindow_isClicked(e){if(this.divName != null){if(this.use_layers){var clickX = e.pageX;var clickY = e.pageY;var t = document.layers[this.divName];if((clickX > t.left) &&(clickX < t.left+t.clip.width) &&(clickY > t.top) &&(clickY < t.top+t.clip.height)){return true;}else{return false;}}else if(document.all){var t = window.event.srcElement;while(t.parentElement != null){if(t.id==this.divName){return true;}t = t.parentElement;}return false;}else if(this.use_gebi && e){var t = e.originalTarget;while(t.parentNode != null){if(t.id==this.divName){return true;}t = t.parentNode;}return false;}return false;}return false;}
function PopupWindow_hideIfNotClicked(e){if(this.autoHideEnabled && !this.isClicked(e)){this.hidePopup();}}
function PopupWindow_autoHide(){this.autoHideEnabled = true;}
function PopupWindow_hidePopupWindows(e){for(var i=0;i<popupWindowObjects.length;i++){if(popupWindowObjects[i] != null){var p = popupWindowObjects[i];p.hideIfNotClicked(e);}}}
function PopupWindow_attachListener(){if(document.layers){document.captureEvents(Event.MOUSEUP);}window.popupWindowOldEventListener = document.onmouseup;if(window.popupWindowOldEventListener != null){document.onmouseup = new Function("window.popupWindowOldEventListener();PopupWindow_hidePopupWindows();");}else{document.onmouseup = PopupWindow_hidePopupWindows;}}
function PopupWindow(){if(!window.popupWindowIndex){window.popupWindowIndex = 0;}if(!window.popupWindowObjects){window.popupWindowObjects = new Array();}if(!window.listenerAttached){window.listenerAttached = true;PopupWindow_attachListener();}this.index = popupWindowIndex++;popupWindowObjects[this.index] = this;this.divName = null;this.popupWindow = null;this.width=0;this.height=0;this.populated = false;this.visible = false;this.autoHideEnabled = false;this.contents = "";this.url="";this.windowProperties="toolbar=no,location=no,status=no,menubar=no,scrollbars=auto,resizable,alwaysRaised,dependent,titlebar=no";if(arguments.length>0){this.type="DIV";this.divName = arguments[0];}else{this.type="WINDOW";}this.use_gebi = false;this.use_css = false;this.use_layers = false;if(document.getElementById){this.use_gebi = true;}else if(document.all){this.use_css = true;}else if(document.layers){this.use_layers = true;}else{this.type = "WINDOW";}this.offsetX = 0;this.offsetY = 0;this.getXYPosition = PopupWindow_getXYPosition;this.populate = PopupWindow_populate;this.setUrl = PopupWindow_setUrl;this.setWindowProperties = PopupWindow_setWindowProperties;this.refresh = PopupWindow_refresh;this.showPopup = PopupWindow_showPopup;this.hidePopup = PopupWindow_hidePopup;this.setSize = PopupWindow_setSize;this.isClicked = PopupWindow_isClicked;this.autoHide = PopupWindow_autoHide;this.hideIfNotClicked = PopupWindow_hideIfNotClicked;}


/* SOURCE FILE: ColorPicker2.js */

ColorPicker_targetInput = null;
function ColorPicker_writeDiv(){document.writeln("<DIV ID=\"colorPickerDiv\" STYLE=\"position:absolute;visibility:hidden; background-color: #faf9ef; margin-left: -110px;\"> </DIV>");}
function ColorPicker_show(anchorname){this.showPopup(anchorname);}
function ColorPicker_pickColor(color,obj){obj.hidePopup();pickColor(color);
previewChoices();
}

var browserSniff = navigator.userAgent;
if (browserSniff.indexOf("MSIE") != -1) {
	browserType = "ie";
	} else if (browserSniff.indexOf("Gecko") != -1) {
	browserType = "mozilla";
	} else if (browserSniff.indexOf("Opera") != -1) {
	browserType = "opera";
	} else {
	browserType = "notKnown";
}

function previewChoices() {
var layoutForm = document.FormId;
var newVar = layoutForm.backcolor.value;
var newVar2 = layoutForm.linkcolor.value;
var newVar3 = layoutForm.fontcolor.value;
var newVar4 = layoutForm.fontfamily.value;
var newVar5 = layoutForm.fontsize1.value;
var newVar6 = layoutForm.tablecolor.value;
var newVar7 = layoutForm.bordercolor.value;
var newVar8 = layoutForm.bordersize.value;
var newVar9 = layoutForm.borderstyle1.value;
var newVar0 = layoutForm.navbarlinkcolor.value;
var newVar11 = layoutForm.bgimage.value;

// Test for HTML
var valueString = newVar + "," + newVar2 + "," + newVar3 + "," + newVar4 + "," + newVar5 + "," + newVar6 + newVar7 + "," + newVar8 + "," + newVar9 + "," + newVar0 + "," + newVar11;
var valueArray = valueString.split(",");

var htmlCheck = "okay";
for (var k = 0; k < valueArray.length; k++) {
/* alert(valueArray[k]); */
if (valueArray[k].indexOf("<") > -1 || valueArray[k].indexOf(">") > -1) {
	var htmlCheck = "nogood";
	alert("DO NOT ENTER HTML TAGS INTO THE TEXT BOXES!!!\n\n The following characters are not permitted:  '<, >'.\n\n The form will be RESET.\n\n");
	layoutForm.reset();
	return false;
	break;
	} //End IF ELSE (Test for HTML)
} //End FOR (Test for HTML)

if(htmlCheck == "okay") {

document.getElementById("PrvHeadLink").style.color = newVar0;
document.getElementById("PrvHeadLink").style.fontFamily = newVar4;
document.getElementById("QuickPreview").style.backgroundColor = newVar;

var LinkStyles = document.getElementById("PreviewLink");
var FontStyles = document.getElementById("PreviewFont");
var TableStyles = document.getElementById("PreviewTable");

LinkStyles.style.color = newVar2;
LinkStyles.style.fontFamily = newVar4;
LinkStyles.style.fontSize = newVar5;

FontStyles.style.color = newVar3;
FontStyles.style.fontFamily = newVar4;
FontStyles.style.fontSize = newVar5;

TableStyles.style.backgroundColor = newVar6;
TableStyles.style.borderColor = newVar7;
TableStyles.style.borderWidth = newVar8;
TableStyles.style.borderStyle = newVar9;

document.getElementById("prvnavbarlinkcolor").style.backgroundColor = newVar0;
document.getElementById("prvBackcolor").style.backgroundColor = newVar;
document.getElementById("prvLinkcolor").style.backgroundColor = newVar2;
document.getElementById("prvFontcolor").style.backgroundColor = newVar3;
document.getElementById("prvTablecolor").style.backgroundColor = newVar6;
document.getElementById("prvBordercolor").style.backgroundColor = newVar7;

if(newVar11=="none") {
	document.getElementById("QuickPreview").style.backgroundImage = "none";
	} else {
	document.getElementById("QuickPreview").style.backgroundImage = 'url(' + newVar11 + ')';
	} 

// Update Quick Preview in IE only for bgrepeat, bgattach.
if (browserType == "ie" || browserType == "ie7") {
var newVar12 = document.getElementsByName("bgimageRepeat");
	for (var i = 0; i < newVar12.length; i++) {
			if (newVar12[i].checked) {
			document.getElementById("QuickPreview").style.backgroundRepeat = newVar12[i].value;
		} //End IF
	} //End FOR

var newVar13 = document.getElementsByName("bgimageAttach");
	for (var z = 0; z < newVar13.length; z++) {
			if (newVar13[z].checked) {
			document.getElementById("QuickPreview").style.backgroundAttachment = newVar13[z].value;
		} //End IF
	} //End FOR
}

// Preview Hide Header-Footer links
if (layoutForm.hideHFlinks.checked == true) {
document.getElementById("PrvHeadLink").style.visibility = "hidden";
} else {
document.getElementById("PrvHeadLink").style.visibility = "visible";
}

// Preview Opacity Filter tweak
var opacityIndex = "alpha(opacity=" + layoutForm.opacityFilter.value + ")";
var mozOpacityIndex = "0." + layoutForm.opacityFilter.value;
if (layoutForm.opacityFilterON.checked == true) {
	TableStyles.style.filter = opacityIndex;
	TableStyles.style.MozOpacity = mozOpacityIndex;
	} else {
	TableStyles.style.filter = "none";
	TableStyles.style.MozOpacity = "1";
}

} //End IF (htmlCheck)

} //End function previewChoices()


function pickColor(color){if(ColorPicker_targetInput==null){alert("Target Input is null, which means you either didn't use the 'select' function or you have no defined your own 'pickColor' function to handle the picked color!");return;}ColorPicker_targetInput.value = color;}
function ColorPicker_select(inputobj,linkname){if(inputobj.type!="text" && inputobj.type!="hidden" && inputobj.type!="textarea"){alert("colorpicker.select: Input object passed is not a valid form input object");window.ColorPicker_targetInput=null;return;}window.ColorPicker_targetInput = inputobj;this.show(linkname);}
function ColorPicker_highlightColor(c){var thedoc =(arguments.length>1)?arguments[1]:window.document;var d = thedoc.getElementById("colorPickerSelectedColor");d.style.backgroundColor = c;d = thedoc.getElementById("colorPickerSelectedColorValue");d.innerHTML = c;}
function ColorPicker(){var windowMode = false;if(arguments.length==0){var divname = "colorPickerDiv";}else if(arguments[0] == "window"){var divname = '';windowMode = true;}else{var divname = arguments[0];}if(divname != ""){var cp = new PopupWindow(divname);}else{var cp = new PopupWindow();cp.setSize(225,250);}cp.currentValue = " ";cp.writeDiv = ColorPicker_writeDiv;cp.highlightColor = ColorPicker_highlightColor;cp.show = ColorPicker_show;cp.select = ColorPicker_select;var colors = new Array("black","dimgray","gray","darkgray","silver","lightgrey","gainsboro","whitesmoke","snow","white",
"maroon","darkred","brown","firebrick","red","indianred","rosybrown","lightcoral","salmon","mistyrose",
"saddlebrown","sienna","chocolate","orangered","tomato","coral","darksalmon","lightsalmon","sandybrown","seashell","peru","darkorange","tan","burlywood","peachpuff","navajowhite","bisque","blanchedalmond","antiquewhite","linen","darkgoldenrod","goldenrod","orange","gold","wheat","moccasin","papayawhip","oldlace","floralwhite","cornsilk","olive","darkkhaki","yellow","khaki","palegoldenrod","lemonchiffon","beige","lightyellow","lightgoldenrodyellow","ivory","darkolivegreen","olivedrab","darkseagreen","yellowgreen","lawngreen","chartreuse","greenyellow","lightgreen","palegreen","honeydew","darkgreen","green","seagreen","forestgreen","mediumseagreen","limegreen","lime","mediumspringgreen","springgreen","mintcream","darkslategray","teal","lightseagreen","mediumaquamarine","mediumturquoise","turquoise","aquamarine","paleturquoise","lightcyan","azure",
"darkcyan","cadetblue","darkturquoise","deepskyblue","skyblue","lightskyblue","lightblue","aqua","cyan","powderblue","royalblue","slategray","lightslategray","steelblue","dodgerblue","cornflowerblue","lightsteelblue","lavender","aliceblue","ghostwhite","midnightblue","navy","darkblue","mediumblue","blue","darkslateblue","blueviolet","slateblue","mediumslateblue","mediumpurple","indigo","purple","darkmagenta","darkviolet","darkorchid","mediumorchid","fuchsia","violet","plum","thistle","crimson","mediumvioletred","deeppink","palevioletred","magenta","orchid","hotpink","pink","lightpink","lavenderblush");var total = colors.length;var width = 10;var cp_contents = "";var windowRef =(windowMode)?"window.opener.":"";if(windowMode){cp_contents += "<HTML><HEAD><TITLE>Select Color</TITLE></HEAD>";cp_contents += "<BODY MARGINWIDTH=0 MARGINHEIGHT=0 LEFTMARGIN=0 TOPMARGIN=0><CENTER>";}cp_contents += "<TABLE class='ColorPickTable' BORDER=1 CELLSPACING=1 CELLPADDING=0>";var use_highlight =(document.getElementById || document.all)?true:false;for(var i=0;i<total;i++){if((i % width) == 0){cp_contents += "<TR>";}if(use_highlight){var mo = 'onMouseOver="'+windowRef+'ColorPicker_highlightColor(\''+colors[i]+'\',window.document)"';}else{mo = "";}cp_contents += '<TD BGCOLOR="'+colors[i]+'"><A HREF="#" onClick="'+windowRef+'ColorPicker_pickColor(\''+colors[i]+'\','+windowRef+'window.popupWindowObjects['+cp.index+']);return false;" '+mo+' STYLE="text-decoration:none;">&nbsp;&nbsp;&nbsp;</A></TD>';if( ((i+1)>=total) ||(((i+1) % width) == 0)){cp_contents += "</TR>";}}if(document.getElementById){var width1 = Math.floor(width/2);var width2 = width = width1;cp_contents += "<TR><TD COLSPAN='1' BGCOLOR='#ffffff' ID='colorPickerSelectedColor' style='border: 1px solid dimgray';>&nbsp;</TD><TD COLSPAN='9' ALIGN='CENTER' ID='colorPickerSelectedColorValue'>white</TD></TR>";}cp_contents += "</TABLE>";if(windowMode){cp_contents += "</CENTER></BODY></HTML>";}cp.populate(cp_contents+"\n");cp.offsetY = 25;cp.autoHide();return cp;}


/*  
++++++++++++++++++++++++
IronSpider_adv_laygen v. 1.1
++++++++++++++++++++++++

© Copyright 2006 - Robert Darrell 
http://www.ironspider.ca

The sale, lease, reproduction, distribution and/or use of this Javascript code 
on any other web site is STRICTLY FORBIDDEN.

Contact: myspacelayout (at) secure.ironspider.ca 

 */

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

function specialOnload() {
	if (browserType == "opera") { // disable Resize Comments Pics tweak in Opera onload 
		document.getElementById("resizePicsID").style.display = "none";
	}
}

var ctonoff;
var controlName2;

function showRGB(parentDivID,linkID,controlName) {
/* 	alert(browserType); */
	function confirmCToff() {
		var ctRadio = document.getElementsByName("contactTable")[1];
			if (ctRadio.checked == false) {
				if (useSlider = confirm("Using the Color Slider to select a Table Link color will not generate a customized contact table (see Tables section). \n\nProceed anyway? (Click 'Cancel' to launch the Color PICKER instead.)")) {
			ctRadio.checked = true;
				} // End nested IF
			ctonoff = useSlider;
/* 			alert(useSlider); */
			} // End outer IF
		} // End confirmCToff
	if (linkID == "linkcolorID") {
	confirmCToff();
	
}
	if (ctonoff == false) {
	/* 	Launch Color PICKER instead */
	cp2.select(document.forms[0].linkcolor,'pick3');
	/* Close Color SLIDER */
	ctonoff = true;
	return;
	}  

	var linkOffset = document.getElementById(linkID).offsetTop;
	document.getElementById(parentDivID).className = "ShowRGB";
/* 	document.getElementById("holder").value = controlName; */
	if (browserType == "ie") {
 	document.getElementById("RGBdivID").style.top = linkOffset + 110; 
	} else if (browserType == "mozilla") {
 	document.getElementById("RGBdivID").style.top = linkOffset + 85;
	} else if (browserType == "opera") {
	document.getElementById("SetPosition").style.position = "relative";
 	document.getElementById("RGBdivID").style.top = linkOffset + 95;
	} else {
	document.getElementById("RGBdivID").style.top = linkOffset + 120;
	} 
	controlName2 = controlName;
}

function hideRGB(parentDivID) {
	document.getElementById(parentDivID).className = "HideRGB";
	if (browserType == "opera") {
	document.getElementById("SetPosition").style.position = "static";
	} 
}

function setRGB(parentDivID) {
	var rgbvalue = document.getElementById("colorCode2").innerHTML;
	document.getElementById(parentDivID).className = "HideRGB";
	document.getElementsByName(controlName2)[0].value = rgbvalue;
}

function checkLinkColor() {
	var ctRadio2 = document.getElementsByName("contactTable")[1];
	var linkColorValue = document.getElementsByName("linkcolor")[0].value;
	if (linkColorValue.indexOf('rgb') != -1) {
	alert("The Color SLIDER was used to select the Table Link color (see Text section). Please use the Color PICKER to select the Link color if you wish to generate a customized contact table with this layout.");
	ctRadio2.checked = true;
	}
}

function ddOpen(wrapID,menuID) {
	document.getElementById(wrapID).style.position = "relative";
	document.getElementById(menuID).style.display = "block";
}
function ddClose(wrapID,menuID) {
	document.getElementById(wrapID).style.position = "static";
	document.getElementById(menuID).style.display = "none";
}

function ddCloseAnywhere() {
	var menuWraps = "ffddWrap,fsddWrap,bsizeddWrap,bstyleddWrap";
	var dropdowns = "ffDropdown,fsDropdown,bsizeDropdown,bstyleDropdown";
	var mw = menuWraps.split(",");
	var dd = dropdowns.split(",");
	for (var i=0; i<mw.length; i++) {
		if(document.getElementById(mw[i]).style.position == "relative") {
		document.getElementById(mw[i]).style.position = "static";
		document.getElementById(dd[i]).style.display = "none";
		}
	}
} 

function setValue(inputName,valuePassed) {
	document.getElementsByName(inputName)[0].value = valuePassed;
}

function rgbhelpOn() {
document.getElementById("rgbHelper").style.display = "block";
}

function rgbhelpOff() {
document.getElementById("rgbHelper").style.display = "none";
}

function enableBGinputs() {
document.getElementsByName('bgimageRepeat')[0].disabled = false;
document.getElementsByName('bgimageRepeat')[1].disabled = false;
document.getElementsByName('bgimageRepeat')[2].disabled = false;
document.getElementsByName('bgimageRepeat')[3].disabled = false;
document.getElementsByName('bgimageAttach')[0].disabled = false;
document.getElementsByName('bgimageAttach')[1].disabled = false;
previewChoices();
}

function resetToNone(inputID) {
document.getElementById(inputID).value = "none";
document.getElementsByName('bgimageRepeat')[0].disabled = true;
document.getElementsByName('bgimageRepeat')[1].disabled = true;
document.getElementsByName('bgimageRepeat')[2].disabled = true;
document.getElementsByName('bgimageRepeat')[3].disabled = true;
document.getElementsByName('bgimageAttach')[0].disabled = true;
document.getElementsByName('bgimageAttach')[1].disabled = true;
}

function clearInput(inputIDX) {
inputIDX.value = "";
}

var sectionArray = new Array("bgSection","tableSection","textSection","tweakSection");
var sectionTabArray = new Array("bgSectionLink","tableSectionLink","textSectionLink","tweakSectionLink");
var sectionTitleArray = new Array("Background","Tables","Text","Tweaks");
var sectionTitleArray2 = new Array(" (Set background image and color)"," (Set table color and border styles)"," (Set text and link colors, font type and font size)"," (Apply various tweaks to your layout)");
var currentSelected = "bgSection";

function showSection(sectionName) {
	currentSelected = sectionName;
/* If IE7 is in effect then browserType is already changed to ie7 during rolloverTab function */
	if(browserType == "ie" || browserType == "ie7") {
	document.getElementById(sectionName).style.display = "block";
	} else {
	document.getElementById(sectionName).style.display = "table";
	}
	for (var i=0; i<=3; i++) {
		if(sectionArray[i] != sectionName) {
		document.getElementById(sectionArray[i]).style.display = "none";
		document.getElementById(sectionTabArray[i]).style.backgroundImage = "none";
		document.getElementById(sectionTabArray[i]).style.borderBottom = "1px solid whitesmoke";
		document.getElementById(sectionTabArray[i]).firstChild.style.fontStyle = "normal";
		document.getElementById(sectionTabArray[i]).firstChild.hideFocus = true; // Hide dotted line
		}
		if(sectionArray[i] == sectionName) {
		document.getElementById(sectionTabArray[i]).style.backgroundImage = "url(~/images/sectionlinkbg06.gif)";
		document.getElementById(sectionTabArray[i]).style.borderBottom = "none";
		document.getElementById(sectionTabArray[i]).firstChild.style.fontStyle = "italic";
		document.getElementById(sectionTabArray[i]).firstChild.hideFocus = true; // Hide dotted line
		}
	}
}

var newDiv = document.createElement("DIV");
newDiv.className = "Tooltip";
var newDivExists = "no";
if (browserType == "ie") {
newDiv.style.left = "0px";
}

function rolloverTab(sectionName) {
	if (browserType == "ie" && browserSniff.indexOf("MSIE 7") != -1) {
		browserType = "ie7";
	}
	for (var i=0; i<sectionArray.length; i++) {
		if(sectionArray[i] == sectionName && sectionName != currentSelected) {
		document.getElementById(sectionTabArray[i]).style.backgroundColor = "#ffcc00";
			if (browserType != "opera" && browserType != "ie7") {
/*  			alert(browserType);  */
			document.getElementById(sectionTabArray[i]).appendChild(newDiv);
			newDiv.style.marginLeft = "0px";
				if (sectionName == "tweakSection" && browserType == "ie") {
				newDiv.style.marginLeft = "-80px";
				} 
				if (sectionName == "tweakSection" && browserType == "mozilla") {
				newDiv.style.marginLeft = "-90px";
				} 
			newDiv.innerHTML = "Click here to load the " + sectionTitleArray[i] + " section. <br>" + sectionTitleArray2[i];
			newDivExists = "yes";
			} // End Nested IF (browserType)
		} // End IF
		if (browserType != "opera" && browserType != "ie7") {
			if(sectionArray[i] == sectionName && sectionName == currentSelected) {
			document.getElementById(sectionTabArray[i]).appendChild(newDiv);
			newDiv.style.marginLeft = "0px";
				if (sectionName == "tweakSection" && browserType == "ie") {
				newDiv.style.marginLeft = "-80px";
				} 
				if (sectionName == "tweakSection" && browserType == "mozilla") {
				newDiv.style.marginLeft = "-90px";
				} 
			newDiv.innerHTML = "The " + sectionTitleArray[i] + " section is currently loaded.";
			newDivExists = "yes";
			}
		} // End Nested IF (browserType)
	} // End FOR
}

function mouseoffTab(sectionName) {
	for (var i=0; i<sectionArray.length; i++) {
		if(sectionArray[i] == sectionName) {
		document.getElementById(sectionTabArray[i]).style.backgroundColor = "transparent";
			if (browserType != "opera") {
				if (newDivExists == "yes") {
				document.getElementById(sectionTabArray[i]).removeChild(newDiv);
				newDivExists = "no";
				}
			}  // End Nested IF (browserType)
		} // End IF
	} // End FOR
}

var hideExtendedFlag = "Off";

function hideExtendedSwitch() {
	if (hideExtendedFlag == "Off") {
		hideExtendedFlag = "On";
		document.getElementById("selectExtendedBGlink").innerHTML = "Select Background";
		document.getElementById("selectExtendedBGlink").style.fontWeight = "bold";
		document.getElementById("extNetHelp").innerHTML = "";
		document.FormId.hideExtended.style.visibility = "visible";
		document.FormId.hideExtended.disabled = false;
		document.FormId.hideExtendedOnline.disabled = false;
		} else {
		hideExtendedSwitchOff();
		}
}

function hideExtendedSwitchOff() {
/* 		Reset hideExtendedFlag, etc */
		hideExtendedFlag = "Off";
		document.getElementById("selectExtendedBGlink").innerHTML = "";
		document.getElementById("selectExtendedBGlink").style.fontWeight = "normal";
		document.getElementById("extNetHelp").innerHTML = "What's this?";
		document.FormId.hideExtended.value = "none";
		document.FormId.hideExtended.style.visibility = "hidden";
		document.FormId.hideExtended.disabled = true;
		document.FormId.hideExtendedOnline.disabled = true;
}

var onlineNowFlag = "Off";

function onlineNowSwitch() {
	if (onlineNowFlag == "Off") {
		onlineNowFlag = "On";
		document.getElementById("selectOnlineNowBGlink").innerHTML = "Select Online Now pic";
		document.getElementById("selectOnlineNowBGlink").style.fontWeight = "bold";
		document.getElementById("onlineNowHelp").innerHTML = "";
		document.FormId.onlineNow.style.visibility = "visible";
		document.FormId.onlineNow.disabled = false;
		document.FormId.onlineNowOnline.disabled = false;
		} else {
		onlineNowSwitchOff();
		}
}

function onlineNowSwitchOff() {
/* 		Reset onlineNowFlag, etc */
		onlineNowFlag = "Off";
		document.getElementById("selectOnlineNowBGlink").innerHTML = "";
		document.getElementById("selectOnlineNowBGlink").style.fontWeight = "normal";
		document.getElementById("onlineNowHelp").innerHTML = "What's this?";
		document.FormId.onlineNow.value = "none";
		document.FormId.onlineNow.style.visibility = "hidden";
		document.FormId.onlineNow.disabled = true;
		document.FormId.onlineNowOnline.disabled = true;
}

var opacityFilterFlag = "Off";

function opacityFilterSwitch() {
	if (opacityFilterFlag == "Off") {
if (useOpacityFilter = confirm("Using the opacity filter will disable the mouseover effect on the customized contact table \n(see Tables section for more information). \n\nOkay to proceed?")) {
		opacityFilterFlag = "On";
		document.getElementById("opacityFilterID").style.display = "block";
		document.getElementById("opacityFilterHelp").innerHTML = "";
		document.FormId.opacityFilter.value = "70";
			} else {
			document.FormId.opacityFilterON.checked = false;
			}
		} else {
	opacityFilterSwitchOff();
	} // End IF flag off
}

function opacityFilterSwitchOff() {
/* 		Reset opacityFilterFlag, etc */
		opacityFilterFlag = "Off";
		document.getElementById("opacityFilterID").style.display = "none";
		document.getElementById("opacityFilterHelp").innerHTML = "What's this?";
}


function resetSpecial() {
/* 	hideExtendedSwitchOff();
	onlineNowSwitchOff(); */
	showSection('bgSection');
	opacityFilterSwitchOff();
	resetToNone('bgimageID');
}

function resizePicsHelpON() {
	if (browserType == "ie" || browserType == "ie7") {
		document.getElementById("resizePics").title = "Check this box to insert a big test image in the preview Comments section to see if it will mess up your layout...";
	} else {
		document.getElementById("resizePics").title = "Check this box to insert a big test image in the preview Comments section...";
	}
}

function resizePicsHelpOFF() {
	document.getElementById("resizePics").title = "";
}

var helpText = parent.document.getElementById("editorHelpID");
var editorHelpFlag = "Off";

function opencloseEditorHelp() {
	var screenHeight = screen.height;
	var screenWidth = screen.width;
	var frameOffset = parent.document.getElementById("formFrame").offsetTop;
	if (editorHelpFlag == "Off") {
		helpText.style.display = "block";
		helpText.style.top = frameOffset + 7 + "px";
 		if (browserType != "ie" && browserType != "ie7") {
			helpText.style.top = frameOffset - 12 + "px";
		}
 		if (browserType == "opera") {
			helpText.style.width = "220px";
		}
		helpText.style.left = "595px";
		editorHelpFlag = "On";
		if (screenWidth < 1024) {
			helpText.style.left = "30px";
			helpText.style.width = "530px";
			helpText.style.height = "150px";
			helpText.style.top = frameOffset + 310 + "px";
	 		if (browserType != "ie") {
				helpText.style.top = frameOffset + 290 + "px";
			}
			if (browserType == "opera") {
				helpText.style.top = frameOffset + 250 + "px";
			}
		}
	} else {
	closeEditorHelp();
	}
}

function closeEditorHelp() {
		helpText.style.display = "none";
		editorHelpFlag = "Off";
}

function presetTransparentLight() {
	FormId.tablecolor.value = "transparent";
	FormId.navbarlinkcolor.value = "gainsboro";
	FormId.fontcolor.value = "gainsboro";
	FormId.linkcolor.value = "lightgoldenrodyellow";
	FormId.bordersize.value = "0px";
	previewChoices();
}
function presetTransparentDark() {
	FormId.tablecolor.value = "transparent";
	FormId.navbarlinkcolor.value = "midnightblue";
	FormId.fontcolor.value = "black";
	FormId.linkcolor.value = "navy";
	FormId.bordersize.value = "0px";
	previewChoices();
}
function presetTablesLight() {
	FormId.tablecolor.value = "dimgray";
	FormId.navbarlinkcolor.value = "gainsboro";
	FormId.fontcolor.value = "gainsboro";
	FormId.linkcolor.value = "lightgoldenrodyellow";
	FormId.bordersize.value = "2px";
	previewChoices();
}
function presetTablesDark() {
	FormId.tablecolor.value = "white";
	FormId.navbarlinkcolor.value = "midnightblue";
	FormId.fontcolor.value = "black";
	FormId.linkcolor.value = "navy";
	FormId.bordersize.value = "2px";
	previewChoices();
}

/* 
Opens select background page to page containing last used image.
Default select background page can also be set here.
*/
function openSelectBGPage() {
	var selectBGIDArray = new Array ("a","m","p","xa","xm","xp");
	var selectBGPageArray = new Array ("selectbackground_abstract.htm","selectbackground_music.htm","selectbackground_pretty.htm","selectbackground_artistic2.htm","selectbackground_music2.htm","selectbackground_pretty2.htm");
	var bgimagevalue = document.FormId.bgimageOnline.value;
	var bgimageID = "a";
	if (bgimagevalue == 0) {
		bgimageID = "xa"; // Set default page ID here
		} else {
		bgimageID = bgimagevalue.charAt(0);
		if (bgimageID == "x") {
			bgimageID = "x" + bgimagevalue.charAt(1);
		}
	}
		for (var i=0; i<selectBGIDArray.length; i++) {
			if(bgimageID == selectBGIDArray[i]) {
				bgimagePage = selectBGPageArray[i];
	openWindow(bgimagePage,'select_background','resizable=1,menubar=0,toolbar=0,scrollbars=1,top=10,left=10,height=550,width=650');
		} // End if 
	} // End for 
} // End function
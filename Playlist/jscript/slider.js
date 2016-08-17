/********************************************************************
                        Slider control creator
                 By Mark Wilton-Jones 12-13/10/2002
Version 1.2 updated 13/04/2005 to protect against multi-button clicks
*********************************************************************

Please see http://www.howtocreate.co.uk/jslibs/ for details and a demo of this script
Please see http://www.howtocreate.co.uk/jslibs/termsOfUse.html for terms of use

To use:

Inbetween the <head> tags, put:

	<script src="PATH TO SCRIPT/slider.js" type="text/javascript" language="javascript1.2"></script>

To create a slider put:

	var myslider = new slider(
		24,             //height of track (excluding border)
		150,            //width of track (excluding border)
		'#9999bb',      //colour of track
		2,              //thickness of track border
		'#770000',      //colour of track border
		2,              //thickness of runner (in the middle of the track)
		'#000000',      //colour of runner
		16,             //height of button (excluding border)
		16,             //width of button (excluding border)
		'#dfdfdf',      //colour of button
		2,              //thickness of button border (shaded to give 3D effect)
		'<font size="2">&lt;&gt;</font>', //text of button (if any) - the font declaration is important - size it to suit your slider
		true,           //direction of travel (true = horizontal [0 is left], false = vertical [0 is top])
		'moveFunction', //the name of the function to execute as the slider moves (null if none)
		'stopFunction', //the name of the function to execute when the slider stops (null if none)
		                //the functions must have already been defined (or use null for none)
		false           //optional, false = use default cursor over button, true = use hand cursor
	);

You can then set the position of the slider after the page has loaded using:

	myslider.setPosition(numberBetween0and1)
___________________________________________________________________________________________*/

var MWJ_slider_controls = 0;

function getRefToDivNest( divID, oDoc ) {
	//get a reference to the slider control, even through nested layers
	if( !oDoc ) { oDoc = document; }
	if( document.layers ) {
		if( oDoc.layers[divID] ) { return oDoc.layers[divID]; } else {
			for( var x = 0, y; !y && x < oDoc.layers.length; x++ ) {
				y = getRefToDivNest(divID,oDoc.layers[x].document); }
			return y; } }
	if( document.getElementById ) { return document.getElementById(divID); }
	if( document.all ) { return document.all[divID]; }
	return document[divID];
}

function sliderMousePos(e) {
	//get the position of the mouse
	if( !e ) { e = window.event; } if( !e || ( typeof( e.pageX ) != 'number' && typeof( e.clientX ) != 'number' ) ) { return [0,0]; }
	if( typeof( e.pageX ) == 'number' ) { var xcoord = e.pageX; var ycoord = e.pageY; } else {
		var xcoord = e.clientX; var ycoord = e.clientY;
		if( !( ( window.navigator.userAgent.indexOf( 'Opera' ) + 1 ) || ( window.ScriptEngine && ScriptEngine().indexOf( 'InScript' ) + 1 ) || window.navigator.vendor == 'KDE' ) ) {
			if( document.documentElement && ( document.documentElement.scrollTop || document.documentElement.scrollLeft ) ) {
				xcoord += document.documentElement.scrollLeft; ycoord += document.documentElement.scrollTop;
			} else if( document.body && ( document.body.scrollTop || document.body.scrollLeft ) ) {
				xcoord += document.body.scrollLeft; ycoord += document.body.scrollTop; } } }
	return [xcoord,ycoord];
}

function slideIsDown(e) {
	if( document.onmousemove == slideIsMove ) { return false; } //protect against multi-button click
	//make note of starting positions and detect mouse movements
	window.msStartCoord = sliderMousePos(e); window.lyStartCoord = this.style?[parseInt(this.style.left),parseInt(this.style.top)]:[parseInt(this.left),parseInt(this.top)];
	if( document.captureEvents && Event.MOUSEMOVE ) { document.captureEvents(Event.MOUSEMOVE); document.captureEvents(Event.MOUSEUP); }
	window.storeMOUSEMOVE = document.onmousemove; window.storeMOUSEUP = document.onmouseup; window.storeLayer = this;
	document.onmousemove = slideIsMove; document.onmouseup = slideIsMove; return false;
}

function slideIsMove(e) {
	//move the slider to its newest position
	var msMvCo = sliderMousePos(e); if( !e ) { e = window.event ? window.event : ( new Object() ); }
	var theLayer = window.storeLayer.style ? window.storeLayer.style : window.storeLayer; var oPix = document.childNodes ? 'px' : 0;
	if( window.storeLayer.hor ) {
		var theNewPos = window.lyStartCoord[0] + ( msMvCo[0] - window.msStartCoord[0] );
		if( theNewPos < 0 ) { theNewPos = 0; } if( theNewPos > window.storeLayer.maxLength ) { theNewPos = window.storeLayer.maxLength; }
		theLayer.left = theNewPos + oPix;
	} else {
		var theNewPos = window.lyStartCoord[1] + ( msMvCo[1] - window.msStartCoord[1] );
		if( theNewPos < 0 ) { theNewPos = 0; } if( theNewPos > window.storeLayer.maxLength ) { theNewPos = window.storeLayer.maxLength; }
		theLayer.top = theNewPos + oPix;
	}
	//run the user's functions and reset the mouse monitoring as before
	if( e.type && e.type.toLowerCase() == 'mousemove' ) {
		if( window.storeLayer.moveFunc ) { window.storeLayer.moveFunc(theNewPos/window.storeLayer.maxLength); }
	} else {
		document.onmousemove = storeMOUSEMOVE; document.onmouseup = window.storeMOUSEUP;
		if( window.storeLayer.stopFunc ) { window.storeLayer.stopFunc(theNewPos/window.storeLayer.maxLength); }
	}
}

function setSliderPosition(oPortion) {
	//set the slider's position
	if( isNaN( oPortion ) || oPortion < 0 ) { oPortion = 0; } if( oPortion > 1 ) { oPortion = 1; }
	var theDiv = getRefToDivNest(this.id); if( theDiv.style ) { theDiv = theDiv.style; }
	oPortion = Math.round( oPortion * this.maxLength ); var oPix = document.childNodes ? 'px' : 0;
	if( this.align ) { theDiv.left = oPortion + oPix; } else { theDiv.top = oPortion + oPix; }
}

function slider(oThght,oTwdth,oTcol,oTBthk,oTBcol,oTRthk,oTRcol,oBhght,oBwdth,oBcol,oBthk,oBtxt,oAlgn,oMf,oSf,oCrs) {
	//draw the slider using huge amounts of nested layers (makes the borders look normal in as many browsers as possible)
	if( document.layers ) {
		document.write(
			'<ilayer left="0" top="0" height="'+(oThght+(2*oTBthk))+'" width="'+(oTwdth+(2*oTBthk))+'" bgcolor="'+oTBcol+'">'+
			'<ilayer left="'+oTBthk+'" top="'+oTBthk+'" height="'+oThght+'" width="'+oTwdth+'" bgcolor="'+oTcol+'">'+
			'<layer left="'+(oAlgn?0:Math.floor((oTwdth-oTRthk)/2))+'" top="'+(oAlgn?Math.floor((oThght-oTRthk)/2):0)+'" height="'+(oAlgn?oTRthk:oThght)+'" width="'+(oAlgn?oTwdth:oTRthk)+'" bgcolor="'+oTRcol+'"><\/layer>'+
			'<layer left="'+(oAlgn?0:Math.floor((oTwdth-(oBwdth+(2*oBthk)))/2))+'" top="'+(oAlgn?Math.floor((oThght-(oBhght+(2*oBthk)))/2):0)+'" height="'+(oBhght+(2*oBthk))+'" width="'+(oBwdth+(2*oBthk))+'" bgcolor="#000000" onmouseover="this.captureEvents(Event.MOUSEDOWN);this.hor='+oAlgn+';this.maxLength='+((oAlgn?oTwdth:oThght)-((oAlgn?oBwdth:oBhght)+(2*oBthk)))+';this.moveFunc='+oMf+';this.stopFunc='+oSf+';this.onmousedown=slideIsDown;" name="MWJ_slider_controls'+MWJ_slider_controls+'">'+
			'<layer left="0" top="0" height="'+(oBhght+oBthk)+'" width="'+(oBwdth+oBthk)+'" bgcolor="#ffffff"><\/layer>'+
			'<layer left="'+oBthk+'" top="'+oBthk+'" height="'+oBhght+'" width="'+oBwdth+'" bgcolor="'+oBcol+'">'+
			oBtxt+'<\/layer><\/layer><\/ilayer><\/ilayer>'
		);
	} else {
		document.write(
			'<div style="position:relative;left:0px;top:0px;height:'+(oThght+(2*oTBthk))+'px;width:'+(oTwdth+(2*oTBthk))+'px;background-color:'+oTBcol+';font-size:0px;">'+
			'<div style="position:relative;left:'+oTBthk+'px;top:'+oTBthk+'px;height:'+oThght+'px;width:'+oTwdth+'px;background-color:'+oTcol+';font-size:0px;">'+
			'<div style="position:absolute;left:'+(oAlgn?0:Math.floor((oTwdth-oTRthk)/2))+'px;top:'+(oAlgn?Math.floor((oThght-oTRthk)/2):0)+'px;height:'+(oAlgn?oTRthk:oThght)+'px;width:'+(oAlgn?oTwdth:oTRthk)+'px;background-color:'+oTRcol+';font-size:0px;"><\/div>'+
			'<div style="position:absolute;left:'+(oAlgn?0:Math.floor((oTwdth-(oBwdth+(2*oBthk)))/2))+'px;top:'+(oAlgn?Math.floor((oThght-(oBhght+(2*oBthk)))/2):0)+'px;height:'+(oBhght+(2*oBthk))+'px;width:'+(oBwdth+(2*oBthk))+'px;font-size:0px;" ondragstart="return false;" onselectstart="return false;" onmouseover="this.hor='+oAlgn+';this.maxLength='+((oAlgn?oTwdth:oThght)-((oAlgn?oBwdth:oBhght)+(2*oBthk)))+';this.moveFunc='+oMf+';this.stopFunc='+oSf+';this.onmousedown=slideIsDown;" id="MWJ_slider_controls'+MWJ_slider_controls+'">'+
			'<div style="border-top:'+oBthk+'px solid #ffffff;border-left:'+oBthk+'px solid #ffffff;border-right:'+oBthk+'px solid #000000;border-bottom:'+oBthk+'px solid #000000;">'+
			'<div style="height:'+oBhght+'px;width:'+oBwdth+'px;font-size:0px;background-color:'+oBcol+';cursor:'+(oCrs?'pointer;cursor:hand':'default')+';">'+
			'<span style="width:100%;text-align:center;">'+oBtxt+'<\/span><\/div><\/div><\/div><\/div><\/div>'
		);
	}
	this.id = 'MWJ_slider_controls'+MWJ_slider_controls; this.maxLength = (oAlgn?oTwdth:oThght)-((oAlgn?oBwdth:oBhght)+(2*oBthk));
	this.align = oAlgn; this.setPosition = setSliderPosition; MWJ_slider_controls++;
}
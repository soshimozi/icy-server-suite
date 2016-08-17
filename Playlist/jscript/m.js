function specialStyles() {
	var browserSniff = navigator.userAgent;
	if (browserSniff.indexOf("MSIE 7.0") != -1) {
		/* ======= IE7 STYLES ======= */
		/* Fix margin-top above main menu (begins at HTML heading block) */
		document.getElementById("mx").style.marginTop = "15px";
		/* Fix padding-top in menu list items */
		menuLI_Array = document.getElementById("mx").getElementsByTagName('li');
		for (var i=0; i<menuLI_Array.length; i++) {
			menuLI_Array[i].style.paddingTop = "0px";
		}
	}
}

var mItem = [];
var mTime = [];
var mWait = 250;

function mSet(ul, c) {
if (document.getElementById) {

	ul = document.getElementById(ul).getElementsByTagName('ul');
	var i, j, e, a, f, b;
	var m = mItem.length;
	for (i = 0; i < ul.length; i++) {
		if (e = ul[i].getAttribute('id')) {
			mItem[m] = e;
			e = ul[i].parentNode;
			e.className = c;

			f = new Function('mShow(\'' + mItem[m] + '\');');
			b = new Function('mBlur(\'' + mItem[m] + '\');');
			e.onmouseover = f;
			e.onmouseout = b;
			a = e.getElementsByTagName('a');
			for (j = 0; j < a.length; j++) {
				a[j].onfocus = f;
				a[j].onblur = b;
			}
			m++;
		}
	}
}
specialStyles();
}


function mShow(id) {
	for (var i = 0; i < mItem.length; i++) {
		if (document.getElementById(mItem[i]).style.display != 'none') {
			if (mItem[i] != id) mHide(mItem[i]);
			else mClear(mItem[i]);
		}
	}
	document.getElementById(id).style.display = 'block';
}


function mHide(id) {
	mClear(id);
	document.getElementById(id).style.display = 'none';
}


function mBlur(id) {
	mTime[id] = setTimeout('mHide(\'' + id + '\');', mWait);
}


function mClear(id) {
	if (mTime[id]) {
		clearTimeout(mTime[id]);
		mTime[id] = null;
	}
}
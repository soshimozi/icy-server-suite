/////////////////////////////////////////////////////////////////////

function detect_browser()
{
	var ua = navigator.userAgent.toLowerCase();
	var browser;

	if ( ua.indexOf("safari")!=-1 ) browser = "safari";
	else if ( ua.indexOf("firefox")!=-1 ) browser = "firefox";
	else if ( ua.indexOf("opera")!=-1 )	browser = "opera";
	else if ( ua.indexOf("webtv")!=-1 )	browser = "webtv";
	else if ( ua.indexOf("konqueror")!=-1 ) browser = "konqueror";
	else if ( ua.indexOf("omniweb")!=-1 ) browser = "omniweb";
	else if ( ua.indexOf("icab")!=-1 ) browser = "icab";
	else if ( ua.indexOf("msie")!=-1 ) browser = "msie";
	else if ( ua.indexOf("compatible")==-1 ) browser = "netscape";
	else browser = "";

	return browser;
}

/////////////////////////////////////////////////////////

function swap_node(node1,node2)
{
	var nextsibling = node1.nextSibling;	
	if ( nextsibling==node2 )
		nextsibling=node1;

	var parentnode = node1.parentNode;
	
	node2.parentNode.replaceChild(node1,node2);

	parentnode.insertBefore(node2,nextsibling);  
}	

/////////////////////////////////////////////////////////

function url_encode(s)
{
	s = escape(s);
	s = s.replace(/\+/g,'%2B');
	
	return s;
}

function url_decode(s)
{
    s = s.replace(/%27/g, "'");
    return s;
}

/////////////////////////////////////////////////////////
function highlight_item(objItem,objCurrentHighlightedItem)
{		
	var objItemText = null;		

	// change classname on old itemtext
	if ( objCurrentHighlightedItem!=null )
	{
		objItemText = get_itemchild3(objCurrentHighlightedItem);		
		if ( objItemText!=null ) {			
			objItemText.className = 'itemchild3_normal';		
		}
	}
	
	if ( objItem==null ) {
		return;
	}		

	// set classname on new itemtext
	objItemText = get_itemchild3(objItem);
	
	if ( objItemText!=null ) {
		objItemText.className = 'itemchild3_active';
	}
}

/////////////////////////////////////////////////////////
	
function toggle_folder_images(objItem)
{
	if ( objItem==null ) {
		return;
	}

	var objItemChild1 = null;
	var objItemChild2 = null;
	
	// get folder image
	objItemChild2 = get_itemchild2(objItem);

	// if item container is expanded
	if ( objItem.getAttribute('containerExpanded')=='yes' )
	{
		if ( objItemChild2!=null ) {
			objItemChild2.className='itemchild2_folder_open';
		}
	}
	else
	{
		if ( objItemChild2!=null ) {
			objItemChild2.className='itemchild2_folder_closed';
		}
	}
}

/////////////////////////////////////////////////////////
	
function get_itemchild1(objItem)
{
    return;
}

/////////////////////////////////////////////////////////
	
function get_itemchild2(objItem)
{
	if ( objItem==null ) {
		return;
	}

    return objItem.childNodes[0];
}

/////////////////////////////////////////////////////////
	
function get_itemchild3(objItem)
{
	if ( objItem==null ) {
		return;
	}

	if ( objItem.childNodes!=null )
	{
        return objItem.childNodes[0].childNodes[0];
	}
}
/////////////////////////////////////////////////////////
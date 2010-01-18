<%@ Page Language="C#" UICulture="auto" ContentType="text/css" %>

<%@ Import Namespace="System.Threading" %>
<%@ Import Namespace="System.Globalization" %>

<script runat="server">
    protected override void InitializeCulture()
    {
        if (Session["culture"] != null)
        {
            String selectedCulture = Session["culture"].ToString();
            UICulture = selectedCulture;
            Culture = selectedCulture;

            Thread.CurrentThread.CurrentCulture =
                CultureInfo.CreateSpecificCulture(selectedCulture);
            Thread.CurrentThread.CurrentUICulture = new
                CultureInfo(selectedCulture);
        }
        base.InitializeCulture();
    }
</script>
html { 
direction : <%= Mytrip_Mvc_Language.text_direction%>; 
} 
div { 
text-align : <%= Mytrip_Mvc_Language.text_align_css%>;
direction  : <%= Mytrip_Mvc_Language.text_direction%>;
} 
DIV.button {
<% if (Mytrip_Mvc_Language.text_align_css == "left")
   { %>
text-align :   left;
<% }
   else
   { %>
text-align :   right;
<% } %>
} 
DIV.button1 {
text-align : <%= Mytrip_Mvc_Language.text_align_css%>;
<% if (Mytrip_Mvc_Language.text_align_css == "left")
   { %>
text-align :   left;
margin-left: 190px;
<% }
   else
   { %>
text-align :   right;
margin-right: 190px;
<% } %>
}

DIV.news_data
{
    font-style: italic;
    <% if (Mytrip_Mvc_Language.text_align_css == "left")
   { %>
text-align :   left;
margin-left: 5px;
<% }
   else
   { %>
text-align :   right;
margin-right: 5px;
<% } %>

}

table th
{
    padding: 6px 5px;
     <% if (Mytrip_Mvc_Language.text_align_css == "left")
   { %>
text-align :   left;
<% }
   else
   { %>
text-align :   right;
<% } %>
    background-color: #BCC7D8;
    color: #2E2633;
}

DIV.right_title
{
    padding: 2px;    
    color: #fff;
    background-color: #495D7F; 
    text-align : <%= Mytrip_Mvc_Language.text_align_css%>;    
}

DIV.pager{
text-align : <%= Mytrip_Mvc_Language.text_align_css%>;    
}  

DIV.pagercount{
    <% if (Mytrip_Mvc_Language.text_align_css == "left")
   { %>
text-align :   right;
<% }
   else
   { %>
text-align :   left;
<% } %>
} 


DIV.edit_content{
position: relative; 
    <% if (Mytrip_Mvc_Language.text_align_css == "left")
   { %>
   float: right;
text-align :   right;
<% }
   else
   { %>
   float: left;
text-align :   left;
<% } %>

} 
DIV.teg
{    
    padding: 10px;
    text-align : <%= Mytrip_Mvc_Language.text_align_css%>;
    background-color: #BCC7D8;
     margin-bottom: 6px;
}

#mainright
{
    position: relative;
    width: 220px;
     <% if (Mytrip_Mvc_Language.text_align_css == "left")
   { %>
    float: right;
    <%}else{%>
    float: left;
    <%} %>
     
    text-align : <%= Mytrip_Mvc_Language.text_align_css%>;
    margin-bottom: 4px;
    /*margin-top: -18px;*/
}
#maincenter
{
    padding: 10px;
    background: #fff;
     <% if (Mytrip_Mvc_Language.text_align_css == "left")
   { %>
    margin-right: 226px;
    <%}else{%>
    margin-left: 226px;
    <%} %>
    margin-bottom: 4px;
    margin-top: 91px;
    border-top-style: solid;
    border-top-width: 4px;
    border-top-color: #FFE8A6;
}
DIV.title_small
{
    position: absolute;
    <% if (Mytrip_Mvc_Language.text_align_css == "left")
   { %>
    float: left;
    margin-left: 127px;
    <%}else{%>
    float: right;
    margin-right: 55px;
    <%} %>
    margin-top: 45px;    
    font-size: 12px;
    color: #CED4DD;
    font-family: Arial, Helvetica, sans-serif;
}  
DIV.title_big
{
    font-size: 48px;
    font-weight: bold;
    font-family: Arial, Helvetica, sans-serif;
    color: #CED4DD;
    position: relative;
     <% if (Mytrip_Mvc_Language.text_align_css == "left")
   { %>
    float: left;
    <%}else{%>
    float: right;
    <%} %>
}
DIV.title_logo
{
position: relative; 
<% if (Mytrip_Mvc_Language.text_align_css == "left")
   { %>
    float: left;
    margin-right: 5px;
    <%}else{%>
    float: right;
    margin-left: 5px;
    <%} %>  
    width: 50px; 
    height: 50px; 
    top: 5px; 
    }
    #logindisplay
{    
    display: block;
    <% if (Mytrip_Mvc_Language.text_align_css == "left")
   { %>
    text-align: right;
    <%}else{%>
    text-align: left;
    <%} %> 
    
    
}
DIV.topmenu
{
    position: absolute;
     <% if (Mytrip_Mvc_Language.text_align_css == "left")
   { %>
    float: left;
    <%}else{%>
    float: right;
    <%} %> 
    
    margin-top: 66px;
    z-index: 10;
}
.topmenu ul, .topmenu ul li{
	margin: 0;	
	padding: 0;	
	display: inline;
	 <% if (Mytrip_Mvc_Language.text_align_css == "left")
   { %>
    margin-right:1px;
    <%}else{%>
    margin-left:1px;
    <%} %> 
	
}
.topmenu ul li {
<% if (Mytrip_Mvc_Language.text_align_css == "left")
   { %>
    float: left;
    <%}else{%>
    float: right;
    <%} %> 
	
	position:relative;	
	/*width:140px;*/
}
.topmenu ul li a{
	display: block;
	padding: 2px 5px;
	background:#495D7F;
    /*margin:1px 0px;*/
	color: #fff;
	border: 1px solid #495D7F;
		text-decoration: none;
    white-space: nowrap;
    
}
.topmenu ul li a:HOVER
{
    text-decoration: none;
    color: #2E2633;
    background-image: url(/content/images/4.png);    
    border: 1px solid #E5C365;
}
 
.topmenu ul li ul {
    display: none;
    position:absolute;
    top:25px;
}
 
.topmenu ul li ul li {
    display:block;    
    border: 0px;
}
.active {
    border: 0px;
}
DIV.login
{
    display: block;
    <% if (Mytrip_Mvc_Language.text_align_css == "left")
   { %>
    text-align: right;
    <%}else{%>
    text-align: left;
    <%} %> 
    
    color: #FFFFFF;
}
DIV.login_text{}
.login ul, .login ul li{
	margin: 0;	
	padding: 0;	
	display: inline;
	<% if (Mytrip_Mvc_Language.text_align_css == "left")
   { %>
    margin-right:1px;
    <%}else{%>
    margin-left:1px;
    <%} %> 
	
}
.login ul li {
<% if (Mytrip_Mvc_Language.text_align_css == "left")
   { %>
    float: right;
    <%}else{%>
    float: left;
    <%} %>
	
	position:relative;	
	/*width:140px;*/
}
.login ul li a{
	display: block;
	padding: 2px 5px;
	background:#495D7F;
    /*margin:1px 0px;*/
	color: #fff;
	border: 1px solid #495D7F;
		text-decoration: none;
    white-space: nowrap;
    
}
.login ul li a:HOVER
{
    text-decoration: none;
    color: #2E2633;
    background-image: url(/content/images/4.png);    
    border: 1px solid #E5C365;
}
 
.login ul li ul {
    display: none;
    position:absolute;
    top:25px;
}
 
.login ul li ul li {
    display:block;    
    border: 0px;
}
a.active_linq
{    color: #2E2633;
    background-image: url(/content/images/4.png);    
    border: 1px solid #E5C365;
    }

div#title
{
<% if (Mytrip_Mvc_Language.text_align_css == "left")
   { %>
    float: left;
    text-align: left;
    <%}else{%>
    float: right;
    text-align: right;
    <%} %>
    display: block;
        
} 
input.search_text1
{
    border-style: solid;
    <% if (Mytrip_Mvc_Language.text_align_css == "left")
   { %>
    float: left;
    <%}else{%>
    float: right;
    <%} %>
    border-width: 1px 1px 1px 1px;
    border-color: #495D7F;
    width: 160px;
    position: relative;
    
} 
input.search_bottom1
{
    border-style: solid;
     <% if (Mytrip_Mvc_Language.text_align_css == "left")
   { %>
    float: right;
    <%}else{%>
    
    float: left;
    <%} %>
    border-width: 1px 1px 1px 1px;
    border-color: #495D7F;
    background: #ffffff url(/content/images/search.png);
    width: 20px;
    height: 20px;
    position: relative;
}   
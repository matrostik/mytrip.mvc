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
direction : <%= Mytrip_Mvc_Language_1.text_direction%>; 
} 
div { 
text-align : <%= Mytrip_Mvc_Language_1.text_align_css%>;
direction  : <%= Mytrip_Mvc_Language_1.text_direction%>;
} 
DIV.button {
<% if (Mytrip_Mvc_Language_1.text_align_css == "left")
   { %>
text-align :   left;
<% }
   else
   { %>
text-align :   right;
<% } %>
} 
DIV.button1 {
text-align : <%= Mytrip_Mvc_Language_1.text_align_css%>;
<% if (Mytrip_Mvc_Language_1.text_align_css == "left")
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
    <% if (Mytrip_Mvc_Language_1.text_align_css == "left")
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
     <% if (Mytrip_Mvc_Language_1.text_align_css == "left")
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
    text-align : <%= Mytrip_Mvc_Language_1.text_align_css%>;    
}

DIV.pager{
text-align : <%= Mytrip_Mvc_Language_1.text_align_css%>;    
}  

DIV.pagercount{
    <% if (Mytrip_Mvc_Language_1.text_align_css == "left")
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
    <% if (Mytrip_Mvc_Language_1.text_align_css == "left")
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
    text-align : <%= Mytrip_Mvc_Language_1.text_align_css%>;
    background-color: #BCC7D8;
     margin-bottom: 6px;
}



#mainright
{
    position: relative;
    width: 220px;
    float: right; 
    text-align : <%= Mytrip_Mvc_Language_1.text_align_css%>;
    margin-bottom: 4px;
    margin-top: -18px;
}
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

.accordion {
	/*width: 230px;*/
	border-bottom: solid 1px #c4c4c4;
}
.accordion div.right_title {
    <%if (Mytrip_Mvc_Language.text_align_css == "left") {%>
	background: #495D7F url(images/arrow-square.gif) no-repeat right -51px;
	<% }
   else
   { %>
   background: #495D7F url(images/arrow-square1.gif) no-repeat left -51px;
   <% } %>
   
	padding: 7px 15px;
	margin: 0;
	/*font: bold 120%/100% Arial, Helvetica, sans-serif;*/
	/*border: solid 1px #495D7F;*/
	border-bottom:solid 1px #c4c4c4;
	cursor: pointer;
}
.accordion div.right_title:hover {
	background-color: #495D7F;
}
.accordion div.right_title.active {
    <%if (Mytrip_Mvc_Language.text_align_css == "left") {%>
	background-position: right 5px;
	<% }
   else
   { %>
   background-position: left 5px;
   <% } %>
	
}
.accordion div.right_content {
	background: #f7f7f7;
	margin: 0;
	padding:  10px 15px 20px;
	border-left: solid 1px #c4c4c4;
	border-right: solid 1px #c4c4c4;
	display: none;
}
p.teg
{    
    padding: 10px;
    text-align : <%= Mytrip_Mvc_Language.text_align_css%>;
    background-color: #BCC7D8;
     margin-bottom: 6px;
}

<!--/*   Mytrip.Mvc.Web.Linq2sql   Copyright �  2009 ������ ���� ����������� oleg@stuh.in 
��� ��������� �������� ��������� ����������� ���������; �� ������ �������������� �/��� �������� 
�� � ������������ � ��������� ����������� ������������ �������� GNU � ��� ����, ��� ��� ���� ������������ 
������ ���������� ��; ���� ������ 2 �������� ���� (�� ������ �������) ����� ����� ������� ������.
��������� ���������������� � �������, ��� ��� ����� ��������, �� ��� ����� �� �� �� ���� ����������� ������������;
���� ��� ��������� ����������� ������������, ��������� � ���������������� ���������� � ������������ ��� ������������ �����. 
��� ������������ �������� ����������� ������������ �������� GNU.
�� ������ ���� �������� ����� ����������� ������������ �������� GNU ������ � ���� ����������; 
���� ��� �� ���, �������� � ���� ���������� �� 
(Free Software Foundation, Inc., 675 Mass Ave, Cambridge, MA 02139, USA).  */-->
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

 <% if (HttpContext.Current.User.Identity.IsAuthenticated)
    { %>     
  
  <%if ((bool)ViewData["abstract_model_blog"] == true)
    {%> <div class="right_title"> 
    &nbsp;&nbsp;<%=Mytrip_Mvc_Language.right_menu_user%>&nbsp;<%= Html.Encode(Page.User.Identity.Name)%></div><div class="right_content"><%
string date = "date";
int abcd = 0;
foreach (mt_artycle_comment x in (IEnumerable<mt_artycle_comment>)ViewData["helper_comment_user"])
{
    if (date != x.AddedDate.DayOfYear.ToString())
    {
        abcd++;
        date = x.AddedDate.DayOfYear.ToString();
    }

}

if (abcd >= (int)ViewData["abstract_model_comment"])
{
    if ((int)ViewData["helper_blog_user_count"] == 0)
    {
        %><a href="<%=Url.Action("ZF", "C")%>">
    <%=Mytrip_Mvc_Language.right_create_blog%></a><%
}
    else
    {
        foreach (mt_artycle_category x in (IEnumerable<mt_artycle_category>)ViewData["helper_blog_user"])
        {
        %><a href="<%=Url.Action("B", "C", new { a = x.Id, b = 1,c=10,d=x.Path })%>">
    <%= x.Title%></a><% break;
        }
    }
      %><br /><br /><% }
else
{
    abcd = (int)ViewData["abstract_model_comment"] - abcd;
        %><%=Html.Language(Mytrip_Mvc_Language.right_activation_blog, abcd.ToString())%>
        
        <br /><br /><% }
    } 
    %></div><%} %>
<!--/*   Mytrip.Mvc.Web.Linq2sql   Copyright ©  2009 Стюхин Олег Анатольевич oleg@stuh.in   */-->
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%string urla = (string)ViewData["menu_url"]; 
  bool news = (bool)ViewData["model_news"];
  bool artycles = (bool)ViewData["model_artycle"];
  bool blogs = (bool)ViewData["model_blog"];
  bool captcha = (bool)ViewData["captcha"]; %>


<%=Html.Menu(urla,news,artycles,blogs,captcha) %>
        
      

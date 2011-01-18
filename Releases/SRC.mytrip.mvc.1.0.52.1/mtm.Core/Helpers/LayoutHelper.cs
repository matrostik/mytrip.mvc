using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mtm.Core.Repository;
using System.Text;

namespace mtm.Core.Helpers
{
    public static class LayoutHelper
    {
        public static void PositionSideBar(bool sidbar, string position)
        {
            string path = "/Views/Shared/_Layout.cshtml";
            string[] page = EditePageRepository.WritePage(path);
            StringBuilder result = new StringBuilder();
            string small ="<div id=\"mainSmall\">";
            string big="<div id=\"mainBig\">";
            string smallR ="<div id=\"mainSmallR\">";
            string bigR="<div id=\"mainBigR\">";
            string smallL ="<div id=\"mainSmallL\">";
            string bigL="<div id=\"mainBigL\">";
            bool create = false;
            foreach (var x in page)
            {
                if (sidbar&&position=="right")
                {
                    if (x.Contains(small) || x.Contains(smallL))
                    { result.AppendLine(smallR); create = true; }
                    else if (x.Contains(big) || x.Contains(bigL))
                    { result.AppendLine(bigR); create = true; }
                    else
                        result.AppendLine(x.Trim());
                }
                else if (sidbar && position == "left")
                {
                    if (x.Contains(small) || x.Contains(smallR))
                    { result.AppendLine(smallL); create = true; }
                    else if (x.Contains(big) || x.Contains(bigR))
                    { result.AppendLine(bigL); create = true; }
                    else
                        result.AppendLine(x.Trim());
                }
                else if (!sidbar)
                {
                    if (x.Contains(smallL) || x.Contains(smallR))
                    { result.AppendLine(small); create = true; }
                    else if (x.Contains(bigL) || x.Contains(bigR))
                    { result.AppendLine(big); create = true; }
                    else
                        result.AppendLine(x.Trim());
                }
            }
            if(create)
            EditePageRepository.CreatePage(path, result.ToString());
        }
        public static void PositionSideBar(string position)
        {
            string path = "/Views/Shared/_Layout.cshtml";
            string[] page = EditePageRepository.WritePage(path);
            StringBuilder result = new StringBuilder();
            string smallR = "<div id=\"mainSmallR\">";
            string bigR = "<div id=\"mainBigR\">";
            string smallL = "<div id=\"mainSmallL\">";
            string bigL = "<div id=\"mainBigL\">";
            bool create = false;
            foreach (var x in page)
            {
                if (position == "right")
                {
                    if (x.Contains(smallL))
                    { result.AppendLine(smallR); create = true; }
                    else if (x.Contains(bigL))
                    { result.AppendLine(bigR); create = true; }
                    else
                        result.AppendLine(x.Trim());
                }
                else if (position == "left")
                {
                    if (x.Contains(smallR))
                    { result.AppendLine(smallL); create = true; }
                    else if (x.Contains(bigR))
                    { result.AppendLine(bigL); create = true; }
                    else
                        result.AppendLine(x.Trim());
                }
            }
            if (create)
                EditePageRepository.CreatePage(path, result.ToString());
        }
        public static void PositionMenu(string position)
        {
            string path = "/Views/Shared/_Layout.cshtml";
            string[] page = EditePageRepository.WritePage(path);
            StringBuilder result = new StringBuilder();
            string smallR = "<div id=\"menucontainerR\">";
            string smallL = "<div id=\"menucontainerL\">";
            bool create = false;
            foreach (var x in page)
            {
                if (position == "right")
                {
                    if (x.Contains(smallL))
                    { result.AppendLine(smallR); create = true; }
                    else
                        result.AppendLine(x.Trim());
                }
                else if (position == "left")
                {
                    if (x.Contains(smallR))
                    { result.AppendLine(smallL); create = true; }
                    else
                        result.AppendLine(x.Trim());
                }
            }
            if (create)
                EditePageRepository.CreatePage(path, result.ToString());
        }
        public static IDictionary<string, string> PositionHtmlElements()
        {
            IDictionary<string, string> result = new Dictionary<string, string>();
            result.Add("left", CoreLanguage.left);
            result.Add("right", CoreLanguage.right);
            return result;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using mtm.Core.Settings;

namespace mtm.Core.Helpers
{
     public static class TwitterHelper
        {
         public static HtmlString AccordionTwitter(this HtmlHelper html)
         {
             HtmlString htmlresult = new HtmlString(GeneralMethods.Accordion(html, "twitter", Profile().ToString()));
             return htmlresult;
         }
         public static IHtmlString Search(string searchQuery,
                                            int width = 200,
                                            int height = 300,
                                            string title = null,
                                            string caption = null,
                                            string backgroundShellColor = "#8ec1da",
                                            string shellColor = "#ffffff",
                                            string tweetsBackgroundColor = "#ffffff",
                                            string tweetsColor = "#444444",
                                            string tweetsLinksColor = "#1985b5",
                                            bool scrollBar = false,
                                            bool loop = true,
                                            bool live = true,
                                            bool hashTags = true,
                                            bool timestamp = true,
                                            bool avatars = true,
                                            string behavior = "default",
                                            int searchInterval = 6000)
            {
                if (string.IsNullOrEmpty(searchQuery))
                {
                    throw new ArgumentException(String.Format(CultureInfo.CurrentUICulture, "Value cannot be null or an empty string.", "searchQuery"), "searchQuery");
                }
                searchQuery = HttpUtility.HtmlEncode(searchQuery);
                title = ((title == null) ? searchQuery : HttpUtility.HtmlEncode(title));
                caption = ((caption == null) ? searchQuery : HttpUtility.HtmlEncode(caption));

                string preFix = string.Format(CultureInfo.InvariantCulture, _TwitterSearchPrefixTemplate,
                    HttpUtility.JavaScriptStringEncode(searchQuery, addDoubleQuotes: true),
                    HttpUtility.JavaScriptStringEncode(title, addDoubleQuotes: true),
                    HttpUtility.JavaScriptStringEncode(caption, addDoubleQuotes: true));
                string middleString = GetTwitterOptionsString(width, height, backgroundShellColor, shellColor,
                                                        tweetsBackgroundColor, tweetsColor, tweetsLinksColor,
                                                        scrollBar, loop, live, hashTags, timestamp, avatars,
                                                        behavior, searchInterval);
                return new HtmlString(preFix + middleString + _TwitterSearchPostfix);
            }

            public static HtmlString Profile(
                                             int height = 300,
                                             string backgroundShellColor = "none",
                                             string shellColor = "none",
                                             string tweetsBackgroundColor = "none",
                                             string tweetsColor = "none",
                                             string tweetsLinksColor = "none",
                                             bool scrollBar = false,
                                             bool loop = false,
                                             bool live = false,
                                             bool hashTags = true,
                                             bool timestamp = true,
                                             bool avatars = false,
                                             string behavior = "all",
                                             int searchInterval = 6000)
            {
                string prefix = string.Format(CultureInfo.InvariantCulture, _TwitterProfilePrefixTemplate, ProfileSetting.twitterCount().ToString(CultureInfo.InvariantCulture));
                string middleString = GetTwitterOptionsString(ProfileSetting.twitterWidth(), height, backgroundShellColor, shellColor,
                                                        tweetsBackgroundColor, tweetsColor, tweetsLinksColor,
                                                        scrollBar, loop, live, hashTags, timestamp, avatars,
                                                        behavior, searchInterval);
                string postFix = string.Format(CultureInfo.InvariantCulture, _TwitterProfilePostfixTemplate, HttpUtility.JavaScriptStringEncode(ProfileSetting.twitterLogin(), addDoubleQuotes: true));
                return new HtmlString(prefix + middleString + postFix);
            }

            private static string GetTwitterOptionsString(
                int width, int height,
                string backgroundShellColor, string shellColor,
                string tweetsBackgroundColor, string tweetsColor, string tweetsLinksColor,
                bool scrollBar, bool loop, bool live, bool hashTags,
                bool timestamp, bool avatars, string behavior, int searchInterval)
            {
                if (width < 0)
                {
                    throw new ArgumentOutOfRangeException("width", String.Format(CultureInfo.CurrentCulture, "", 0));
                }
                if (height < 0)
                {
                    throw new ArgumentOutOfRangeException("height", String.Format(CultureInfo.CurrentCulture, "", 0));
                }
                if (searchInterval < 1)
                {
                    throw new ArgumentOutOfRangeException("searchInterval", String.Format(CultureInfo.CurrentCulture, "", 1));
                }

                if (string.IsNullOrEmpty(backgroundShellColor))
                {
                    throw new ArgumentException(String.Format(CultureInfo.CurrentUICulture, "", "backgroundShellColor"), "backgroundShellColor");
                }
                if (string.IsNullOrEmpty(shellColor))
                {
                    throw new ArgumentException(String.Format(CultureInfo.CurrentUICulture, "", "shellColor"), "shellColor");
                }
                if (string.IsNullOrEmpty(tweetsBackgroundColor))
                {
                    throw new ArgumentException(String.Format(CultureInfo.CurrentUICulture, "", "tweetsBackgroundColor"), "tweetsBackgroundColor");
                }
                if (string.IsNullOrEmpty(tweetsColor))
                {
                    throw new ArgumentException(String.Format(CultureInfo.CurrentUICulture, "", "tweetsColor"), "tweetsColor");
                }
                if (string.IsNullOrEmpty(tweetsLinksColor))
                {
                    throw new ArgumentException(String.Format(CultureInfo.CurrentUICulture, "", "tweetsLinksColor"), "tweetsLinksColor");
                }
                if (string.IsNullOrEmpty(behavior))
                {
                    throw new ArgumentException(String.Format(CultureInfo.CurrentUICulture, "", "behavior"), "behavior");
                }
                return string.Format(CultureInfo.InvariantCulture, _TwitterCommonTemplate,
                                    searchInterval.ToString(CultureInfo.InvariantCulture),
                                    (width == 0) ? "'auto'" : width.ToString(CultureInfo.InvariantCulture),
                                    height.ToString(CultureInfo.InvariantCulture),
                                    HttpUtility.JavaScriptStringEncode(backgroundShellColor, addDoubleQuotes: true),
                                    HttpUtility.JavaScriptStringEncode(shellColor, addDoubleQuotes: true),
                                    HttpUtility.JavaScriptStringEncode(tweetsBackgroundColor, addDoubleQuotes: true),
                                    HttpUtility.JavaScriptStringEncode(tweetsColor, addDoubleQuotes: true),
                                    HttpUtility.JavaScriptStringEncode(tweetsLinksColor, addDoubleQuotes: true),
                                    scrollBar ? "true" : "false",
                                    loop ? "true" : "false",
                                    live ? "true" : "false",
                                    hashTags ? "true" : "false",
                                    timestamp ? "true" : "false",
                                    avatars ? "true" : "false",
                                    HttpUtility.JavaScriptStringEncode(behavior, addDoubleQuotes: true));
            }
            const string _TwitterCommonTemplate = @"
        interval: {0},
        width: {1},
        height: {2},
        theme: {{
            shell: {{
                background: {3},
                color: {4}
            }},
            tweets: {{
                background: {5},
                color: {6},
                links: {7}
            }}
        }},
        features: {{
            scrollbar: {8},
            loop: {9},
            live: {10},
            hashtags: {11},
            timestamp: {12},
            avatars: {13},
            behavior: {14}
        }}";

            const string _TwitterSearchPrefixTemplate =
    "<script src=\"http://widgets.twimg.com/j/2/widget.js\" type=\"text/javascript\" " + @"></script>
<script " + "type=\"text/javascript\" " + @">
    new TWTR.Widget({{
        version: 2,
        type: ""search"",
        search: {0},
        title: {1},
        subject: {2},";

            const string _TwitterSearchPostfix = @"
    }).render().start();
</script>";

            const string _TwitterProfilePrefixTemplate =
    "<script src=\"http://widgets.twimg.com/j/2/widget.js\" type=\"text/javascript\" " + @"></script>
<script " + "type=\"text/javascript\" " + @">
    new TWTR.Widget({{
        version: 2,
        type: ""profile"",
        rpp: {0},";

            const string _TwitterProfilePostfixTemplate = @"
    }}).render().setUser({0}).start();
</script>";

        }
    }

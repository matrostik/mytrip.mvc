using System;
using System.Web;
using System.Web.Mvc;
using System.Linq.Expressions;
using System.Web.Mvc.Html;
using System.Text;

namespace Mytrip.Mvc.Helpers
{
    public static class MytripTextBoxHelper
    {
        public static string MytripTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression)
        {
            StringBuilder result=new StringBuilder();
            result.AppendLine("<div class=\"textbox\">");
            result.AppendLine("<div><div class=\"texttopright\"></div><div class=\"texttopleft\"></div>");
            result.AppendLine("<div class=\"texttopcon\"></div></div><div class=\"textcontent\">");          
            result.AppendLine(InputExtensions.TextBoxFor(html,expression).ToString());
           result.AppendLine("</div><div><div class=\"textbottomright\"></div><div class=\"textbottomleft\"></div>");
           result.AppendLine("<div class=\"textbottomcon\"></div></div></div>");
           return result.ToString();
        }
        public static string MytripPasswordFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression)
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine("<div class=\"textbox\">");
            result.AppendLine("<div><div class=\"texttopright\"></div><div class=\"texttopleft\"></div>");
            result.AppendLine("<div class=\"texttopcon\"></div></div><div class=\"textcontent\">");
            result.AppendLine(InputExtensions.PasswordFor(html, expression).ToString());
            result.AppendLine("</div><div><div class=\"textbottomright\"></div><div class=\"textbottomleft\"></div>");
            result.AppendLine("<div class=\"textbottomcon\"></div></div></div>");
            return result.ToString();
        }
        //[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        //public static MvcHtmlString TextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
        //{
        //    return htmlHelper.TextBoxFor(expression, new RouteValueDictionary(htmlAttributes));
        //}

        //[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        //public static MvcHtmlString TextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> htmlAttributes)
        //{
        //    return TextBoxHelper(htmlHelper,
        //                         ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData).Model,
        //                         ExpressionHelper.GetExpressionText(expression),
        //                         htmlAttributes);
        //}

        //private static MvcHtmlString TextBoxHelper(this HtmlHelper htmlHelper, object model, string expression, IDictionary<string, object> htmlAttributes)
        //{
        //    return InputHelper(htmlHelper, InputType.Text, expression, model, false /* useViewData */, false /* isChecked */, true /* setId */, true /* isExplicitValue */, htmlAttributes);
        //}

        //// Helper methods

        //private static MvcHtmlString InputHelper(HtmlHelper htmlHelper, InputType inputType, string name, object value, bool useViewData, bool isChecked, bool setId, bool isExplicitValue, IDictionary<string, object> htmlAttributes)
        //{
        //    name = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);
        //    if (String.IsNullOrEmpty(name))
        //    {
        //        throw new ArgumentException(System.Web.Mvc.MvcResources.Common_NullOrEmpty, "name");
        //    }

        //    TagBuilder tagBuilder = new TagBuilder("input");
        //    tagBuilder.MergeAttributes(htmlAttributes);
        //    tagBuilder.MergeAttribute("type", HtmlHelper.GetInputTypeString(inputType));
        //    tagBuilder.MergeAttribute("name", name, true);

        //    string valueParameter = Convert.ToString(value, CultureInfo.CurrentCulture);
        //    bool usedModelState = false;

        //    switch (inputType)
        //    {
        //        case InputType.CheckBox:
        //            bool? modelStateWasChecked = htmlHelper.GetModelStateValue(name, typeof(bool)) as bool?;
        //            if (modelStateWasChecked.HasValue)
        //            {
        //                isChecked = modelStateWasChecked.Value;
        //                usedModelState = true;
        //            }
        //            goto case InputType.Radio;
        //        case InputType.Radio:
        //            if (!usedModelState)
        //            {
        //                string modelStateValue = htmlHelper.GetModelStateValue(name, typeof(string)) as string;
        //                if (modelStateValue != null)
        //                {
        //                    isChecked = String.Equals(modelStateValue, valueParameter, StringComparison.Ordinal);
        //                    usedModelState = true;
        //                }
        //            }
        //            if (!usedModelState && useViewData)
        //            {
        //                isChecked = htmlHelper.EvalBoolean(name);
        //            }
        //            if (isChecked)
        //            {
        //                tagBuilder.MergeAttribute("checked", "checked");
        //            }
        //            tagBuilder.MergeAttribute("value", valueParameter, isExplicitValue);
        //            break;
        //        case InputType.Password:
        //            if (value != null)
        //            {
        //                tagBuilder.MergeAttribute("value", valueParameter, isExplicitValue);
        //            }
        //            break;
        //        default:
        //            string attemptedValue = (string)htmlHelper.GetModelStateValue(name, typeof(string));
        //            tagBuilder.MergeAttribute("value", attemptedValue ?? ((useViewData) ? htmlHelper.EvalString(name) : valueParameter), isExplicitValue);
        //            break;
        //    }

        //    if (setId)
        //    {
        //        tagBuilder.GenerateId(name);
        //    }

        //    // If there are any errors for a named field, we add the css attribute.
        //    ModelState modelState;
        //    if (htmlHelper.ViewData.ModelState.TryGetValue(name, out modelState))
        //    {
        //        if (modelState.Errors.Count > 0)
        //        {
        //            tagBuilder.AddCssClass(HtmlHelper.ValidationInputCssClassName);
        //        }
        //    }

        //    if (inputType == InputType.CheckBox)
        //    {
        //        // Render an additional <input type="hidden".../> for checkboxes. This
        //        // addresses scenarios where unchecked checkboxes are not sent in the request.
        //        // Sending a hidden input makes it possible to know that the checkbox was present
        //        // on the page when the request was submitted.
        //        StringBuilder inputItemBuilder = new StringBuilder();
        //        inputItemBuilder.Append(tagBuilder.ToString(TagRenderMode.SelfClosing));

        //        TagBuilder hiddenInput = new TagBuilder("input");
        //        hiddenInput.MergeAttribute("type", HtmlHelper.GetInputTypeString(InputType.Hidden));
        //        hiddenInput.MergeAttribute("name", name);
        //        hiddenInput.MergeAttribute("value", "false");
        //        inputItemBuilder.Append(hiddenInput.ToString(TagRenderMode.SelfClosing));
        //        return MvcHtmlString.Create(inputItemBuilder.ToString());
        //    }

        //    return tagBuilder.ToMvcHtmlString(TagRenderMode.SelfClosing);
        //}
    }
}
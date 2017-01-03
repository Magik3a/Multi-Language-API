using System;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;

public static class HtmlHelperExtensions
{
    private static HttpClient client = new HttpClient();

    public static string MultiLanguage(this HtmlHelper htmlHelper, int phrase)
    {
        var language = CultureInfo.CurrentUICulture;
        HttpResponseMessage response = Task.Run(() => client.GetAsync($"http://localhost:44113/api/Phrases/{phrase}/{language.TwoLetterISOLanguageName}")).Result;

        if (response.IsSuccessStatusCode)
        {
            var task = Json.Decode(Task.Run(() => response.Content.ReadAsStringAsync()).Result);
            return task.PhraseText;
        }
        else
        {
            return response.StatusCode.ToString();
        }

    }

    public static MvcHtmlString RawActionLink(this AjaxHelper ajaxHelper, string linkText, string icon, string actionName, string controllerName, object routeValues, AjaxOptions ajaxOptions, object htmlAttributes, bool clearPage = false)
    {
        var repID = Guid.NewGuid().ToString();

        InitAjaxOptions(icon, actionName, controllerName, ajaxOptions, clearPage);

        var lnk = ajaxHelper.ActionLink(repID, actionName, controllerName, routeValues, ajaxOptions, htmlAttributes);

        linkText = $"<i class='fa {icon}' aria-hidden='true'></i>{linkText}";


        return MvcHtmlString.Create(lnk.ToString().Replace(repID, linkText));

        // return $" <a href='{destination}'></a>";
    }


    public static MvcHtmlString RawActionLinkWithoutIcon(this AjaxHelper ajaxHelper, string linkText, string icon, string actionName, string controllerName, object routeValues, AjaxOptions ajaxOptions, object htmlAttributes, bool clearPage = false)
    {
        var repID = Guid.NewGuid().ToString();

        InitAjaxOptions(icon, actionName, controllerName, ajaxOptions, clearPage);

        var lnk = ajaxHelper.ActionLink(repID, actionName, controllerName, routeValues, ajaxOptions, htmlAttributes);

        return MvcHtmlString.Create(lnk.ToString().Replace(repID, linkText));

        // return $" <a href='{destination}'></a>";
    }

    public static MvcForm RawAjaxBeginForm(this AjaxHelper ajaxHelper,
        string icon,
        string actionName,
        string controllerName,
        object routeValues,
        AjaxOptions ajaxOptions,
        object htmlAttributes,
        bool clearPage = false)
    {
        InitAjaxOptions(icon, actionName, controllerName, ajaxOptions, clearPage);

        return ajaxHelper.BeginForm(actionName, controllerName, routeValues, ajaxOptions, htmlAttributes);
    }

    private static void InitAjaxOptions(
        string icon,
        string actionName,
        string controllerName,
        AjaxOptions ajaxOptions,
        bool clearPage)
    {
        ajaxOptions.OnBegin = string.IsNullOrWhiteSpace(ajaxOptions.OnBegin)
            ? "$.MltApi.AjaxClickBegin(this)"
            : ajaxOptions.OnBegin;
        if (!clearPage)
        {

            ajaxOptions.OnComplete = string.IsNullOrWhiteSpace(ajaxOptions.OnComplete) ? $"$.MltApi.AjaxClickComplete('{actionName}', '{controllerName}', 'fa {icon}', false)" : ajaxOptions.OnComplete;

        }
        else
        {
            ajaxOptions.OnComplete = string.IsNullOrWhiteSpace(ajaxOptions.OnComplete) ? $"$.MltApi.AjaxClickComplete('{actionName}', '{controllerName}', 'fa {icon}', true)" : ajaxOptions.OnComplete;

        }
        ajaxOptions.OnSuccess = string.IsNullOrWhiteSpace(ajaxOptions.OnSuccess) ? "$.MltApi.AjaxClickSuccess" : ajaxOptions.OnSuccess;
        ajaxOptions.UpdateTargetId = string.IsNullOrWhiteSpace(ajaxOptions.UpdateTargetId) ? "page-content" : ajaxOptions.UpdateTargetId;
        ajaxOptions.InsertionMode = InsertionMode.Replace;
    }
}

using System;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

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

    public static MvcHtmlString RawActionLink(this AjaxHelper ajaxHelper, string linkText, string icon, string actionName, string controllerName, object routeValues, AjaxOptions ajaxOptions, object htmlAttributes)
    {
        var repID = Guid.NewGuid().ToString();

        ajaxOptions.OnBegin = "MainSidebarLinkClickBegin(this)";
        ajaxOptions.OnComplete = $"MainSidebarLinkClickComplete('{actionName}', '{controllerName}', 'fa {icon}')";
        ajaxOptions.OnSuccess = "MainSidebarLinkClickSuccess";
        ajaxOptions.UpdateTargetId = "page-content";
        ajaxOptions.InsertionMode = InsertionMode.Replace;

        var lnk = ajaxHelper.ActionLink(repID, actionName, controllerName, routeValues, ajaxOptions, htmlAttributes);

        linkText = $"<i class='fa {icon}' aria-hidden='true'></i><span>{linkText}</span>";
        return MvcHtmlString.Create(lnk.ToString().Replace(repID, linkText));

        // return $" <a href='{destination}'></a>";
    }


    public static MvcHtmlString RawActionLinkWithoutIcon(this AjaxHelper ajaxHelper, string linkText, string icon, string actionName, string controllerName, object routeValues, AjaxOptions ajaxOptions, object htmlAttributes)
    {
        var repID = Guid.NewGuid().ToString();

        ajaxOptions.OnBegin = "MainSidebarLinkClickBegin(this)";
        ajaxOptions.OnComplete = $"MainSidebarLinkClickComplete('{actionName}', '{controllerName}', 'fa {icon}')";
        ajaxOptions.OnSuccess = "MainSidebarLinkClickSuccess";
        ajaxOptions.UpdateTargetId = "page-content";
        ajaxOptions.InsertionMode = InsertionMode.Replace;

        var lnk = ajaxHelper.ActionLink(repID, actionName, controllerName, routeValues, ajaxOptions, htmlAttributes);

        return MvcHtmlString.Create(lnk.ToString().Replace(repID, linkText));

        // return $" <a href='{destination}'></a>";
    }
}

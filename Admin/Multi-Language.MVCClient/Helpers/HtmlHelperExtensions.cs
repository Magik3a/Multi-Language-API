﻿using System;
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

    public static MvcHtmlString RawActionLink(this AjaxHelper ajaxHelper, string linkText, string icon, string actionName, string controllerName, object routeValues, AjaxOptions ajaxOptions, object htmlAttributes)
    {
        var repID = Guid.NewGuid().ToString();

        InitAjaxOptions(icon, actionName, controllerName, ajaxOptions);

        var lnk = ajaxHelper.ActionLink(repID, actionName, controllerName, routeValues, ajaxOptions, htmlAttributes);

        linkText = $"<i class='fa {icon}' aria-hidden='true'></i><span>{linkText}</span>";
        return MvcHtmlString.Create(lnk.ToString().Replace(repID, linkText));

        // return $" <a href='{destination}'></a>";
    }


    public static MvcHtmlString RawActionLinkWithoutIcon(this AjaxHelper ajaxHelper, string linkText, string icon, string actionName, string controllerName, object routeValues, AjaxOptions ajaxOptions, object htmlAttributes)
    {
        var repID = Guid.NewGuid().ToString();

        InitAjaxOptions(icon, actionName, controllerName, ajaxOptions);

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
        object htmlAttributes)
    {
        InitAjaxOptions(icon, actionName, controllerName, ajaxOptions);

        return ajaxHelper.BeginForm(actionName, controllerName, routeValues, ajaxOptions, htmlAttributes);
    }

    private static void InitAjaxOptions(
        string icon,
        string actionName,
        string controllerName,
        AjaxOptions ajaxOptions)
    {
        ajaxOptions.OnBegin = "$.MltApi.AjaxClickBegin(this)";
        ajaxOptions.OnComplete = $"$.MltApi.AjaxClickComplete('{actionName}', '{controllerName}', 'fa {icon}')";
        ajaxOptions.OnSuccess = "$.MltApi.AjaxClickSuccess";
        ajaxOptions.UpdateTargetId = "page-content";
        ajaxOptions.InsertionMode = InsertionMode.Replace;
    }
}

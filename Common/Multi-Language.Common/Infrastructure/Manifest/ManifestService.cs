using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Multi_language.Common.Infrastructure.Manifest
{
    public class ManifestService : IManifestService
    {

        /// <summary>
        /// Gets the manifest JSON for the current site. This allows you to customize the icon and other browser
        /// settings for Chrome/Android and FireFox (FireFox support is coming). See https://w3c.github.io/manifest/
        /// for the official W3C specification. See http://html5doctor.com/web-manifest-specification/ for more
        /// information. See https://developer.chrome.com/multidevice/android/installtohomescreen for Chrome's
        /// implementation.
        /// </summary>
        /// <returns>The manifest JSON for the current site.</returns>
        public string GetManifestJson(string siteShortTitle, string siteTitle, string imageFolderPath)
        {
            JObject document = new JObject(
                new JProperty("short_name", siteShortTitle),
                new JProperty("display", "standalone"),
                new JProperty("name", siteTitle),
                new JProperty("icons",
                    new JArray(
                        GetIconJObject(imageFolderPath + "/android-chrome-36x36.png", "36x36", "image/png", "0.75"),
                        GetIconJObject(imageFolderPath + "/android-chrome-48x48.png", "48x48", "image/png", "1.0"),
                        GetIconJObject(imageFolderPath + "/android-chrome-72x72.png", "72x72", "image/png", "1.5"),
                        GetIconJObject(imageFolderPath + "/android-chrome-96x96.png", "96x96", "image/png", "2.0"),
                        GetIconJObject(imageFolderPath + "/android-chrome-144x144.png", "144x144", "image/png", "3.0"),
                        GetIconJObject(imageFolderPath + "/android-chrome-192x192.png", "192x192", "image/png", "4.0"))));

            return document.ToString(Formatting.Indented);
        }

        /// <summary>
        /// Gets a <see cref="JObject"/> containing the specified image details.
        /// </summary>
        /// <param name="iconPath">The path to the icon image.</param>
        /// <param name="sizes">The size of the image in the format AxB.</param>
        /// <param name="type">The MIME type of the image.</param>
        /// <param name="density">The pixel density of the image.</param>
        /// <returns>A <see cref="JObject"/> containing the image details.</returns>
        private JObject GetIconJObject(string iconPath, string sizes, string type, string density)
        {
            return new JObject(
                new JProperty("src", iconPath),
                new JProperty("sizes", sizes),
                new JProperty("type", type),
                new JProperty("density", density));
        }
    }
}

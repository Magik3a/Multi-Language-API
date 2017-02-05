using Multi_language.Common.Infrastructure.Sitemap;

namespace WebApplication1.Services
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    public class SitemapPingerService : ISitemapPingerService
    {
        #region Fields

        private readonly HttpClient httpClient;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly ILogger<SitemapPingerService> logger;
        private readonly IOptions<SitemapSettings> sitemapSettings;
        private readonly IUrlHelper urlHelper;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SitemapPingerService"/> class.
        /// </summary>
        /// <param name="hostingEnvironment">The environment the application is running under. This can be Development,
        /// Staging or Production by default.</param>
        /// <param name="logger">The <see cref="SitemapPingerService"/> logger.</param>
        /// <param name="sitemapSettings">The sitemap settings.</param>
        /// <param name="urlHelper">The URL helper.</param>
        public SitemapPingerService(
            IHostingEnvironment hostingEnvironment,
            ILogger<SitemapPingerService> logger,
            IOptions<SitemapSettings> sitemapSettings,
            IUrlHelper urlHelper)
        {
            this.hostingEnvironment = hostingEnvironment;
            this.logger = logger;
            this.sitemapSettings = sitemapSettings;
            this.urlHelper = urlHelper;

            this.httpClient = new HttpClient();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Send (or 'ping') the URL of this sites sitemap.xml file to search engines like Google, Bing and Yahoo,
        /// This method should be called each time the sitemap changes. Google says that 'We recommend that you
        /// resubmit a Sitemap no more than once per hour.' The way we 'ping' our sitemap to search engines is
        /// actually an open standard See
        /// http://www.sitemaps.org/protocol.html#submit_ping
        /// You can read the sitemap ping documentation for the top search engines below:
        /// Google - http://googlewebmastercentral.blogspot.co.uk/2014/10/best-practices-for-xml-sitemaps-rssatom.html
        /// Bing - http://www.bing.com/webmaster/help/how-to-submit-sitemaps-82a15bd4.
        /// Yahoo - https://developer.yahoo.com/search/siteexplorer/V1/ping.html
        /// </summary>
        public async Task PingSearchEngines()
        {

            if (this.hostingEnvironment.IsProduction())
            {
                foreach (string sitemapPingLocation in this.sitemapSettings.Value.SitemapPingLocations)
                {
                    string sitemapUrl = this.urlHelper.RouteUrl("sitemap").TrimEnd('/');
                    string url = sitemapPingLocation + WebUtility.UrlEncode(sitemapUrl);
                    HttpResponseMessage response = await this.httpClient.GetAsync(url);
                    if (!response.IsSuccessStatusCode)
                    {
                        HttpRequestException exception = new HttpRequestException(string.Format(
                            CultureInfo.InvariantCulture,
                            "Pinging search engine {0}. Response status code does not indicate success: {1} ({2}).",
                            url,
                            (int)response.StatusCode,
                            response.ReasonPhrase));
                        this.logger.LogError("Error while pinging site-map to search engines.", exception);
                    }
                }
            }
        }

        #endregion
    }
}
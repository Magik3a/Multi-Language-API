﻿using Microsoft.AspNetCore.Mvc;

namespace Multi_language.Common.Infrastructure
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    /// <summary>
    /// The caching settings for the application.
    /// </summary>
    public class CacheProfileSettings
    {
        /// <summary>
        /// Gets or sets the cache profiles (How long to cache things for).
        /// </summary>
        public Dictionary<string, CacheProfile> CacheProfiles { get; set; }
    }
}

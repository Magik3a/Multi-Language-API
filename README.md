# Multi-Language-API
[![Stories in Progress](https://badge.waffle.io/Magik3a/Multi-Language-API.png?label=In%20Progress&title=In%20Progress)](http://waffle.io/Magik3a/Multi-Language-API)
[![Reported Bugs](https://badge.waffle.io/Magik3a/Multi-Language-API.png?label=bug&title=Reported%20Bugs)](http://waffle.io/Magik3a/Multi-Language-API)
[![Build status](https://ci.appveyor.com/api/projects/status/8q2gd8ldexwl595q?svg=true)](https://ci.appveyor.com/project/Magik3a/multi-language-api)

Hi developers, let me introduce my admin panel. It's a single page application (SPA) with Oauth 2.0 Security (bearer token) and it's fully mobile-web-app-capable. It uses SignalR for real time communication and server stability statistics. You can use it for maintaining one database for your multilingual applications. Integration in SaaS apps is easy and can be done in every modern language:

Example in Jquery - $.get("http://api.s2kdesign.com/Project/2/Context/2", function (data) { console.log(data); });

For all open APIs, you can check swagger: http://api.s2kdesign.com/swagger

Yea, and so on, the data API uses background worker process with the help of HangFire, and it's used to check the server processor and ram usage.

So far, so good, but soon as i created this, it's already deprecated. :( Let's call this version Beta and move on. For the next major improvements, i decided that there is no need to invent the hot water so i will start high!!!

For the next release, i will use ASP.NET Core Boilerplate with Identity Server 4 (Bearer token/OpenID connect). Of course, the API's will be extended with minimum 6 new major functionalities (SEO system, Email system, Event system, Payment system, License system, LUIS Bot system ). For client interface will be used Angular2 (TypeScript) and the project will be easy to install on docker. I will try to maintain clean and bug(features) free code, but most of this technologies are still in Beta and I guess , problems will exist :D 

<img src="https://cdn.colorlib.com/wp/wp-content/uploads/sites/2/adminlte-free-bootstrap-admin-template.jpg" alt=""/>


## Technologies Used
- ASP.NET MVC
- ASP.NET Web API
- Entity Framework (Repository Pattern)
- DI/IOC (Ninject)
- AutoMapper 
- AngularJS

### Nuget Packages 
- AdminLteMvc" version="1.0.0" 
- angularjs" version="1.5.8"
- AutoMapper" version="5.1.1"
- bootstrap" version="3.3.7"
- EntityFramework" version="6.1.3"
- jQuery" version="3.1.0"
- Newtonsoft.Json" version="9.0.1"
- Ninject" version="3.2.2.0"
- Ninject.Extensions.Conventions" version="3.2.0.0"
- Ninject.MVC4" version="3.2.1.0"
- Ninject.Web.Common" version="3.2.3.0"
- Ninject.Web.Common.WebHost" version="3.2.3.0"
- WebActivatorEx" version="2.0"

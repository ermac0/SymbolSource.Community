using System;
using System.Configuration;
using System.Web;
using System.Web.Hosting;

namespace SymbolSource.Gateway.NuGet.Core
{
    public class PackageUtility
    {
        private static readonly Lazy<string> _packagePhysicalPath = new Lazy<string>(ResolvePackagePath);

        public static string PackagePhysicalPath
        {
            get
            {
                return _packagePhysicalPath.Value;
            }
        }

        public static string GetPackageDownloadUrl(Package package)
        {
            var context = HttpContext.Current;

            var applicationPath = EnsureTrailingSlash(context.Request.ApplicationPath);


            //http://hague/NuGet/FeedService.mvc
            //http://hague/NuGet/FeedService.mvc/FindPackagesById
            //http://hague/packages/Packages(Id='DAIS.Common',Version='1.0.0.0')/$value

            return applicationPath + string.Format("NuGet/FeedService.mvc/Packages(Id='{0}',Version='{1}')/$value", package.Id, package.Version);
        }

        private static string EnsureTrailingSlash(string input)
        {
            input = (input ?? string.Empty).Trim();

            if (!input.EndsWith("/"))
            {
                input += "/";
            }

            return input;
        }

        private static string ResolvePackagePath()
        {
            // The packagesPath could be an absolute path (rooted and use as is)
            // or a virtual path (and use as a virtual path)
            var path = ConfigurationManager.AppSettings["packagesPath"];

            if (String.IsNullOrEmpty(path))
            {
                // Default path
                return HostingEnvironment.MapPath("~/Packages");
            }

            if (path.StartsWith("~/"))
            {
                return HostingEnvironment.MapPath(path);
            }

            return path;
        }
    }
}
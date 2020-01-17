using Google.Cloud.Diagnostics.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using Microsoft.AspNetCore;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Storage.v1;
using Google.Cloud.Storage.V1;
using Google.Cloud.Language.V1;
using Grpc.Auth;
using System.Net.Http;
using CommandLine;

namespace Gryffindor
{
    public class Program
    {
        public static IHostingEnvironment HostingEnvironment { get; private set; }
        public static IConfiguration Configuration { get; private set; }

        public static string GcpProjectId { get; private set; }
        public static bool HasGcpProjectId => !string.IsNullOrEmpty(GcpProjectId);
        /// <summary>
        /// Get the Google Cloud Platform Project ID from the platform it is running on,
        /// or from the appsettings.json configuration if not running on Google Cloud Platform.
        /// </summary>
        /// <param name="config">The appsettings.json configuration.</param>
        /// <returns>
        /// The ID of the GCP Project this service is running on, or the Google:ProjectId
        /// from the configuration if not running on GCP.
        /// </returns>
        private static string GetProjectId(IConfiguration config)
        {
            var instance = Google.Api.Gax.Platform.Instance();
            var projectId = instance?.ProjectId ?? config["Google:ProjectId"];
            if (string.IsNullOrEmpty(projectId))
            {
                // Set Google:ProjectId in appsettings.json to enable stackdriver logging outside of GCP.
                return null;
            }
            return projectId;
        }

        /// <summary>
        /// Gets a service name for error reporting.
        /// </summary>
        /// <param name="config">The appsettings.json configuration to read a service name from.</param>
        /// <returns>
        /// The name of the Google App Engine service hosting this application,
        /// or the Google:ErrorReporting:ServiceName configuration field if running elsewhere.
        /// </returns>
        /// <seealso href="https://cloud.google.com/error-reporting/docs/formatting-error-messages#FIELDS.service"/>
        private static string GetServiceName(IConfiguration config)
        {
            var instance = Google.Api.Gax.Platform.Instance();
            var serviceName = instance?.GaeDetails?.ServiceId ?? config["Google:ErrorReporting:ServiceName"];
            if (string.IsNullOrEmpty(serviceName))
            {
                throw new InvalidOperationException(
                    "The error reporting library requires a service name. " +
                    "Update appsettings.json by setting the Google:ErrorReporting:ServiceName property with your " +
                    "Service Id, then recompile.");
            }
            return serviceName;
        }

        /// <summary>
        /// Gets a version id for error reporting.
        /// </summary>
        /// <param name="config">The appsettings.json configuration to read a version id from.</param>
        /// <returns>
        /// The version of the Google App Engine service hosting this application,
        /// or the Google:ErrorReporting:Version configuration field if running elsewhere.
        /// </returns>
        /// <seealso href="https://cloud.google.com/error-reporting/docs/formatting-error-messages#FIELDS.version"/>
        private static string GetVersion(IConfiguration config)
        {
            var instance = Google.Api.Gax.Platform.Instance();
            var versionId = instance?.GaeDetails?.VersionId ?? config["Google:ErrorReporting:Version"];
            if (string.IsNullOrEmpty(versionId))
            {
                throw new InvalidOperationException(
                    "The error reporting library requires a version id. " +
                    "Update appsettings.json by setting the Google:ErrorReporting:Version property with your " +
                    "service version id, then recompile.");
            }
            return versionId;
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

        class BaseOptions
        {
            string _projectId;
            [Option('p', Default = null, HelpText = "Your Google project id.")]
            public string ProjectId
            {
                get
                {
                    if (null == _projectId)
                    {
                        _projectId = Environment.GetEnvironmentVariable("GOOGLE_PROJECT_ID");
                        if (null == _projectId)
                        {
                            _projectId = Google.Api.Gax.Platform.Instance()?.GceDetails?.ProjectId;
                            if (null == _projectId)
                            {
                                throw new ArgumentNullException("ProjectId");
                            }
                        }
                    }
                    return _projectId;
                }
                set
                {
                    _projectId = value;
                }
            }

            [Option('j', Default = null, HelpText = "Path to a credentials json file.")]
            public string JsonPath { get; set; }

            [Option('c', Default = false, HelpText = "Pull credentials from compute engine metadata.")]
            public bool Compute { get; set; }
        }

        [Verb("hand", HelpText = "Authenticate using the Google.Cloud.Storage library.  "
            + "The preferred way of authenticating hand-coded wrapper libraries.")]
        class HandOptions : BaseOptions { }

        [Verb("cloud", HelpText = "Authenticate using the Google.Cloud.Language library.  "
            + "The preferred way of authenticating gRPC-based libraries.")]
        class CloudOptions : BaseOptions { }

        [Verb("api", HelpText = "Authenticate using the Google.Apis.Storage library.")]
        class ApiOptions : BaseOptions { }

        [Verb("http", HelpText = "Authenticate using and make a rest HTTP call.")]
        class HttpOptions : BaseOptions { }

        /// <summary>
        /// Each library supports 3 methods of authenticating.
        /// </summary>
        interface AuthLibrary
        {
            object AuthImplicit(string projectId);
            object AuthExplicit(string projectId, string jsonPath);
            object AuthExplicitComputeEngine(string projectId);
        }

        /// <summary>
        /// Some APIs like storage have hand-coded libraries.  They auth like this.
        /// </summary>
        public class HandCodedLibrary : AuthLibrary
        {
            ///////////////////////////////////////////////
            // This is the preferred way of authenticating.
            ///////////////////////////////////////////////
            // [START auth_cloud_implicit]
            public object AuthImplicit(string projectId)
            {
                // If you don't specify credentials when constructing the client, the
                // client library will look for credentials in the environment.
                var credential = GoogleCredential.GetApplicationDefault();
                var storage = StorageClient.Create(credential);
                // Make an authenticated API request.
                var buckets = storage.ListBuckets(projectId);
                foreach (var bucket in buckets)
                {
                    Console.WriteLine(bucket.Name);
                }
                return null;
            }
            // [END auth_cloud_implicit]

            // [START auth_cloud_explicit]
            // Some APIs, like Storage, accept a credential in their Create()
            // method.
            public object AuthExplicit(string projectId, string jsonPath)
            {
                // Explicitly use service account credentials by specifying 
                // the private key file.
                var credential = GoogleCredential.FromFile(jsonPath);
                var storage = StorageClient.Create(credential);
                // Make an authenticated API request.
                var buckets = storage.ListBuckets(projectId);
                foreach (var bucket in buckets)
                {
                    Console.WriteLine(bucket.Name);
                }
                return null;
            }
            // [END auth_cloud_explicit]

            // [START auth_cloud_explicit_compute_engine]
            // Some APIs, like Storage, accept a credential in their Create()
            // method.
            public object AuthExplicitComputeEngine(string projectId)
            {
                // Explicitly request service account credentials from the compute
                // engine instance.
                GoogleCredential credential =
                    GoogleCredential.FromComputeCredential();
                var storage = StorageClient.Create(credential);
                // Make an authenticated API request.
                var buckets = storage.ListBuckets(projectId);
                foreach (var bucket in buckets)
                {
                    Console.WriteLine(bucket.Name);
                }
                return null;
            }
            // [END auth_cloud_explicit_compute_engine]
        }

        /// <summary>
        /// Authenticates with Google.Apis.* libraries.
        /// </summary>
        class ApiLibrary : AuthLibrary
        {
            // [START auth_api_implicit]
            public object AuthImplicit(string projectId)
            {
                GoogleCredential credential =
                    GoogleCredential.GetApplicationDefault();
                // Inject the Cloud Storage scope if required.
                if (credential.IsCreateScopedRequired)
                {
                    credential = credential.CreateScoped(new[]
                    {
                    StorageService.Scope.DevstorageReadOnly
                });
                }
                var storage = new StorageService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "DotNet Google Cloud Platform Auth Sample",
                });
                var request = new BucketsResource.ListRequest(storage, projectId);
                var requestResult = request.Execute();
                foreach (var bucket in requestResult.Items)
                {
                    Console.WriteLine(bucket.Name);
                }
                return null;
            }
            // [END auth_api_implicit]

            // [START auth_api_explicit]
            public object AuthExplicit(string projectId, string jsonPath)
            {
                var credential = GoogleCredential.FromFile(jsonPath);
                // Inject the Cloud Storage scope if required.
                if (credential.IsCreateScopedRequired)
                {
                    credential = credential.CreateScoped(new[]
                    {
                    StorageService.Scope.DevstorageReadOnly
                });
                }
                var storage = new StorageService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "DotNet Google Cloud Platform Auth Sample",
                });
                var request = new BucketsResource.ListRequest(storage, projectId);
                var requestResult = request.Execute();
                foreach (var bucket in requestResult.Items)
                {
                    Console.WriteLine(bucket.Name);
                }
                return null;
            }
            // [END auth_api_explicit]

            // [START auth_api_explicit_compute_engine]
            public object AuthExplicitComputeEngine(string projectId)
            {
                // Explicitly use service account credentials by specifying the 
                // private key file.
                GoogleCredential credential =
                    GoogleCredential.FromComputeCredential();
                // Inject the Cloud Storage scope if required.
                if (credential.IsCreateScopedRequired)
                {
                    credential = credential.CreateScoped(new[]
                    {
                    StorageService.Scope.DevstorageReadOnly
                });
                }
                var storage = new StorageService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "DotNet Google Cloud Platform Auth Sample",
                });
                var request = new BucketsResource.ListRequest(storage, projectId);
                var requestResult = request.Execute();
                foreach (var bucket in requestResult.Items)
                {
                    Console.WriteLine(bucket.Name);
                }
                return null;
            }
            // [END auth_api_explicit_compute_engine]
        }

        class HttpLibrary : AuthLibrary
        {
            // [START auth_http_implicit]
            public object AuthImplicit(string projectId)
            {
                GoogleCredential credential =
                    GoogleCredential.GetApplicationDefault();
                // Inject the Cloud Storage scope if required.
                if (credential.IsCreateScopedRequired)
                {
                    credential = credential.CreateScoped(new[]
                    {
                    "https://www.googleapis.com/auth/devstorage.read_only"
                });
                }
                HttpClient http = new Google.Apis.Http.HttpClientFactory()
                    .CreateHttpClient(
                    new Google.Apis.Http.CreateHttpClientArgs()
                    {
                        ApplicationName = "Google Cloud Platform Auth Sample",
                        GZipEnabled = true,
                        Initializers = { credential },
                    });
                UriBuilder uri = new UriBuilder(
                    "https://www.googleapis.com/storage/v1/b");
                uri.Query = "project=" +
                    System.Web.HttpUtility.UrlEncode(projectId);
                var resultText = http.GetAsync(uri.Uri).Result.Content
                    .ReadAsStringAsync().Result;
                dynamic result = Newtonsoft.Json.JsonConvert
                    .DeserializeObject(resultText);
                foreach (var bucket in result.items)
                {
                    Console.WriteLine(bucket.name);
                }
                return null;
            }
            // [END auth_http_implicit]

            // [START auth_http_explicit]
            public object AuthExplicit(string projectId, string jsonPath)
            {
                var credential = GoogleCredential.FromFile(jsonPath);
                // Inject the Cloud Storage scope if required.
                if (credential.IsCreateScopedRequired)
                {
                    credential = credential.CreateScoped(new[]
                    {
                    "https://www.googleapis.com/auth/devstorage.read_only"
                });
                }
                HttpClient http = new Google.Apis.Http.HttpClientFactory()
                    .CreateHttpClient(
                    new Google.Apis.Http.CreateHttpClientArgs()
                    {
                        ApplicationName = "Google Cloud Platform Auth Sample",
                        GZipEnabled = true,
                        Initializers = { credential },
                    });
                UriBuilder uri = new UriBuilder(
                    "https://www.googleapis.com/storage/v1/b");
                uri.Query = "project=" +
                    System.Web.HttpUtility.UrlEncode(projectId);
                var resultText = http.GetAsync(uri.Uri).Result.Content
                    .ReadAsStringAsync().Result;
                dynamic result = Newtonsoft.Json.JsonConvert
                    .DeserializeObject(resultText);
                foreach (var bucket in result.items)
                {
                    Console.WriteLine(bucket.name);
                }
                return null;
            }

            public object AuthExplicitComputeEngine(string projectId)
            {
                throw new NotImplementedException();
            }
            // [END auth_http_explicit]
        }

        /// <summary>
        /// Authenticates with Google.Cloud.* libraries.
        /// Specifically calls the language API, but all the machine learning APIs,
        /// And some other APIs like Pub/Sub also follow this pattern.
        /// </summary>
        public class CloudLibrary : AuthLibrary
        {
            // [START auth_cloud_explicit]
            // Other APIs, like Language, accept a channel in their Create()
            // method.
            public object AuthExplicit(string projectId, string jsonPath)
            {
                var credential = GoogleCredential.FromFile(jsonPath)
                    .CreateScoped(LanguageServiceClient.DefaultScopes);
                var channel = new Grpc.Core.Channel(
                    LanguageServiceClient.DefaultEndpoint.ToString(),
                    credential.ToChannelCredentials());
                var client = LanguageServiceClient.Create(channel);
                AnalyzeSentiment(client);
                return 0;
            }
            // [END auth_cloud_explicit]

            // [START auth_cloud_explicit_compute_engine]
            // Other APIs, like Language, accept a channel in their Create()
            // method.
            public object AuthExplicitComputeEngine(string projectId)
            {
                var credential = GoogleCredential.FromComputeCredential();
                var channel = new Grpc.Core.Channel(
                    LanguageServiceClient.DefaultEndpoint.ToString(),
                    credential.ToChannelCredentials());
                var client = LanguageServiceClient.Create(channel);
                AnalyzeSentiment(client);
                return 0;
            }
            // [END auth_cloud_explicit_compute_engine]

            public object AuthImplicit(string projectId)
            {
                var client = LanguageServiceClient.Create();
                AnalyzeSentiment(client);
                return 0;
            }

            void AnalyzeSentiment(LanguageServiceClient client)
            {
                string text = "Hello World!";
                var response = client.AnalyzeSentiment(new Document()
                {
                    Content = text,
                    Type = Document.Types.Type.PlainText
                });
                var sentiment = response.DocumentSentiment;
                Console.WriteLine($"Score: {sentiment.Score}");
                Console.WriteLine($"Magnitude: {sentiment.Magnitude}");
            }
        }

        public class AuthSample
        {
            static object ChooseAuthMethodAndInvoke(BaseOptions options, AuthLibrary library)
            {
                if (!string.IsNullOrEmpty(options.JsonPath))
                {
                    return library.AuthExplicit(options.ProjectId, options.JsonPath);
                }
                if (options.Compute)
                {
                    return library.AuthExplicitComputeEngine(options.ProjectId);
                }
                return library.AuthImplicit(options.ProjectId);
            }

            public static void Main(string[] args)
            {
                CreateWebHostBuilder(args).Build().Run();

                var host = new WebHostBuilder()
                    .UseKestrel()
                    .UseContentRoot(Directory.GetCurrentDirectory())
                    .UseIISIntegration()
                    .ConfigureAppConfiguration((context, configBuilder) =>
                    {
                        HostingEnvironment = context.HostingEnvironment;

                        configBuilder.SetBasePath(HostingEnvironment.ContentRootPath)
                            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                            .AddJsonFile($"appsettings.{HostingEnvironment.EnvironmentName}.json", optional: true)
                            .AddEnvironmentVariables();

                        Configuration = configBuilder.Build();
                        GcpProjectId = GetProjectId(Configuration);
                    })
                    .ConfigureServices(services =>
                    {
                    // Add framework services.Microsoft.VisualStudio.ExtensionManager.ExtensionManagerService
                    services.AddMvc();

                        if (HasGcpProjectId)
                        {
                        // Enables Stackdriver Trace.
                        services.AddGoogleTrace(options => options.ProjectId = GcpProjectId);
                        // Sends Exceptions to Stackdriver Error Reporting.
                        services.AddGoogleExceptionLogging(
                                options =>
                                {
                                    options.ProjectId = GcpProjectId;
                                    options.ServiceName = GetServiceName(Configuration);
                                    options.Version = GetVersion(Configuration);
                                });
                            services.AddSingleton<ILoggerProvider>(sp => GoogleLoggerProvider.Create(GcpProjectId));
                        }
                    })
                    .ConfigureLogging(loggingBuilder =>
                    {
                        loggingBuilder.AddConfiguration(Configuration.GetSection("Logging"));
                        if (HostingEnvironment.IsDevelopment())
                        {
                        // Only use Console and Debug logging during development.
                        loggingBuilder.AddConsole(options =>
                                options.IncludeScopes = Configuration.GetValue<bool>("Logging:IncludeScopes"));
                            loggingBuilder.AddDebug();
                        }
                    })
                    .Configure((app) =>
                    {
                        var logger = app.ApplicationServices.GetService<ILoggerFactory>().CreateLogger("Startup");
                        if (HasGcpProjectId)
                        {
                        // Sends logs to Stackdriver Error Reporting.
                        app.UseGoogleExceptionLogging();
                        // Sends logs to Stackdriver Trace.
                        app.UseGoogleTrace();

                            logger.LogInformation(
                                "Stackdriver Logging enabled: https://console.cloud.google.com/logs/");
                            logger.LogInformation(
                                "Stackdriver Error Reporting enabled: https://console.cloud.google.com/errors/");
                            logger.LogInformation(
                                "Stackdriver Trace enabled: https://console.cloud.google.com/traces/");
                        }
                        else
                        {
                            logger.LogWarning(
                                "Stackdriver Logging not enabled. Missing Google:ProjectId in configuration.");
                            logger.LogWarning(
                                "Stackdriver Error Reporting not enabled. Missing Google:ProjectId in configuration.");
                            logger.LogWarning(
                                "Stackdriver Trace not enabled. Missing Google:ProjectId in configuration.");
                        }

                        if (HostingEnvironment.IsDevelopment())
                        {
                            app.UseDeveloperExceptionPage();
                            app.UseStaticFiles(new StaticFileOptions
                            {
                                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "node_modules")),
                                RequestPath = new PathString("/lib")
                            });
                        }
                        else
                        {
                            app.UseExceptionHandler("/Home/Error");
                        }

                        app.UseStaticFiles();

                        app.UseMvc(routes =>
                        {
                            routes.MapRoute(
                                name: "default",
                                template: "{controller=Home}/{action=Index}/{id?}");
                        });
                    })
                    .Build();

                host.Run();

                Parser.Default.ParseArguments<CloudOptions, ApiOptions, HttpOptions, HandOptions>(args)
                     .MapResult(
                       (HandOptions opts) => ChooseAuthMethodAndInvoke(opts, new HandCodedLibrary()),
                       (ApiOptions opts) => ChooseAuthMethodAndInvoke(opts, new ApiLibrary()),
                       (HttpOptions opts) => ChooseAuthMethodAndInvoke(opts, new HttpLibrary()),
                       (CloudOptions opts) => ChooseAuthMethodAndInvoke(opts, new CloudLibrary()),
                       errs => 1);
            }
        }
    }
}

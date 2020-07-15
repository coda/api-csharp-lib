/* 
 * Coda API
 *
 * # Introduction  The Coda API is a RESTful API that lets you programmatically interact with Coda docs:   * List and search Coda docs  * Create new docs and copy existing ones  * Share and publish docs  * Discover pages, tables, formulas, and controls  * Read, insert, upsert, update, and delete rows  Version 1 of the API will be supported until at least January 15, 2021. As we update and release newer versions of the API, we reserve the right to remove older APIs and functionality with a 3-month deprecation notice. We will post about such changes as well as announce new features in the [Developers Central](https://community.coda.io/c/developers-central) section of our Community, and update the [API updates](https://coda.io/api-updates) doc.  # Getting Started  Our [Getting Started Guide](https://coda.io/t/Getting-Started-Guide-Coda-API_toujpmwflfy) helps you learn the basic of working with the API and shows a few ways you can use it. Check it out, and learn how to:  - Read data from Coda tables and write back to them - Build a one-way sync from one Coda doc to another - Automate reminders - Sync your Google Calendar to Coda  # Using the API  Coda's REST API is designed to be straightforward to use. You can use the language and platform of your choice to make requests. To get a feel for the API, you can also use a tool like [Postman](https://www.getpostman.com/) or [Insomnia](https://insomnia.rest/).  ## API Endpoint  This API uses a base path of `https://coda.io/apis/v1`.  ## Resource IDs and Links  Each resource instance retrieved via the API has the following fields:    - `id`: The resource's immutable ID, which can be used to refer to it within its context   - `type`: The type of resource, useful for identifying it in a heterogenous collection of results   - `href`: A fully qualified URI that can be used to refer to and get the latest details on the resource  Most resources can be queried by their name or ID. We recommend sticking with IDs where possible, as names are fragile and prone to being changed by your doc's users.  ### List Endpoints  Endpoints supporting listing of resources have the following fields:    - `items`: An array containing the listed resources, limited by the `limit` and `pageToken` query parameters   - `nextPageLink`: If more results are available, an API link to the next page of results   - `nextPageToken`: If more results are available, a page token that can be passed into the `pageToken` query parameter  **The maximum page size may change at any time, and may be different for different endpoints.** Please do not rely on it for any behavior of your application. If you pass a `limit` parameter that is larger than our maximum allowed limit, we will only return as many results as our maximum limit. You should look for the presence of the `nextPageToken` on the response to see if there are more results available, rather than relying on a result set that matches your provided limit.  ### Doc IDs  While most object IDs will have to be discovered via the API, you may find yourself frequently wanting to get the ID of a specific Coda doc.  Here's a handy tool that will extract it for you. (See if you can find the pattern!)  <form>   <fieldset style=\"margin: 0px 25px 25px 25px; display: inline;\">     <legend>Doc ID Extractor</legend>     <input type=\"text\" id=\"de_docUrl\" placeholder=\"Paste in a Coda doc URL\"            style=\"width: 250px; padding: 8px; margin-right: 20px;\" />     <span>       Your doc ID is:&nbsp;&nbsp;&nbsp;       <input id=\"de_docId\" readonly=\"true\"              style=\"width: 150px; padding: 8px; font-family: monospace; border: 1px dashed gray;\" />   </fieldset> </form>  <script>   (() => {     const docUrl = document.getElementById('de_docUrl');     const docId = document.getElementById('de_docId');     docUrl.addEventListener('input', () => {       docId.value = (docUrl.value.match(/_d([\\w-]+)/) || [])[1] || '';     });     docId.addEventListener('mousedown', () => docId.select());     docId.addEventListener('click', () => docId.select());   })(); </script>  ## Rate Limiting  The Coda API sets a reasonable limit on the number of requests that can be made per minute. Once this limit is reached, calls to the API will start returning errors with an HTTP status code of 429. If you find yourself hitting rate limits and would like your individual rate to be raised, please contact us at <help+api@coda.io>.  ## Consistency  While edits made in Coda are shared with other collaborators in real-time, it can take a few seconds for them to become available via the API. You may also notice that changes made via the API, such as updating a row, are not immediate. These endpoints all return an HTTP 202 status code, instead of a standard 200, indicating that the edit has been accepted and queued for processing. This generally takes a few seconds, and the edit may fail if invalid. Each such edit will return a `requestId` in the response, and you can pass this `requestId` to the [`#getMutationStatus`](#operation/getMutationStatus) endpoint to find out if it has been applied.  ## Volatile Formulas  Coda exposes a number of \"volatile\" formulas, as as `Today()`, `Now()`, and `User()`. When used in a live Coda doc, these formulas affect what's visible in realtime, tailored to the current user.  Such formulas behave differently with the API. Time-based values may only be current to the last edit made to the doc. User-based values may be blank or invalid.  ## Free and Paid Workspaces  We make the Coda API available to all of our users free of charge, in both free and paid workspaces. However, API usage is subject to the role of the user associated with the API token in the workspace applicable to each API request. What this means is:  - For the [`#createDoc`](#operation/createDoc) endpoint specifically, the owner of the API token must be a Doc   Maker (or Admin) in the workspace. If the \"Any member can create docs\" option in enabled in the workspace   settings, they can be an Editor and will get auto-promoted to Doc Maker upon using this endpoint. Lastly, if in   addition, the API key owner matches the \"Approved email domains\" setting, they will be auto-added to the   workspace and promoted to Doc Maker upon using this endpoint  This behavior applies to the API as well as any integrations that may use it, such as Zapier.  ## Examples  To help you get started, this documentation provides code examples in Python, Unix shell, and Google Apps Script. These examples are based on a simple doc that looks something like this:  ![](https://cdn.coda.io/external/img/api_example_doc.png)  ### Python examples  These examples use Python 3.6+. If you don't already have the `requests` module, use `pip` or `easy_install` to get it.  ### Shell examples  The shell examples are intended to be run in a Unix shell. If you're on Windows, you will need to install [WSL](https://docs.microsoft.com/en-us/windows/wsl/install-win10).  These examples use the standard cURL utility to pull from the API, and then process it with `jq` to extract and format example output. If you don't already have it, you can either [install it](https://stedolan.github.io/jq/) or run the command without it to see the raw JSON output.  ### Google Apps Script examples  ![](https://cdn.coda.io/external/img/api_gas.png)  [Google Apps Script](https://script.google.com/) makes it easy to write code in a JavaScript-like syntax and easily access many Google products with built-in libraries. You can set up your scripts to run periodically, which makes it a good environment for writing tools without maintaining your own server.  Coda provides a library for Google Apps Script. To use it, go into `Resources -> Libraries...` and enter the following library ID: `15IQuWOk8MqT50FDWomh57UqWGH23gjsWVWYFms3ton6L-UHmefYHS9Vl`. If you want to see the library's source code, it's available [here](https://script.google.com/d/15IQuWOk8MqT50FDWomh57UqWGH23gjsWVWYFms3ton6L-UHmefYHS9Vl/edit).  Google provides autocomplete for API functions as well as generated docs. You can access these docs via the Libraries dialog by clicking on the library name. Required parameters that would be included in the URL path are positional arguments in each of these functions, followed by the request body, if applicable. All remaining parameters can be specified in the options object.  ## OpenAPI/Swagger Spec  In an effort to standardize our API and make it accessible, we offer an OpenAPI 3.0 specification:  - [OpenAPI 3.0 spec - YAML](https://coda.io/apis/v1/openapi.yaml) - [OpenAPI 3.0 spec - JSON](https://coda.io/apis/v1/openapi.json)  ### Swagger 2.0  We also offer a downgraded Swagger 2.0 version of our specification. This may be useful for a number of tools that haven't yet been adapted to OpenAPI 3.0. Here are the links:  - [Swagger 2.0 spec - YAML](https://coda.io/apis/v1/swagger.yaml) - [Swagger 2.0 spec - JSON](https://coda.io/apis/v1/swagger.json)  #### Postman collection  To get started with prototyping the API quickly in Postman, you can use one of links above to import the Coda API into a collection. You'll then need to set the [appropriate header](#section/Authentication) and environment variables.  ## Client libraries  We do not currently support client libraries apart from Google Apps Script. To work with the Coda API, you can either use standard network libraries for your language, or use the appropriate Swagger Generator tool to auto-generate Coda API client libraries for your language of choice. We do not provide any guarantees that these autogenerated libraries are compatible with our API (e.g., some libraries may not work with Bearer authentication).  ### OpenAPI 3.0  [Swagger Generator 3](https://generator3.swagger.io/) (that link takes you to the docs for the generator API) can generate client libraries for [these languages](https://generator3.swagger.io/v2/clients). It's relatively new and thus only has support for a limited set of languages at this time.  ### Swagger 2.0  [Swagger Generator](https://generator.swagger.io/) takes in a legacy Swagger 2.0 specification, but can generate client libraries for [more languages](http://generator.swagger.io/api/gen/clients). You can also use local [CLI tools](https://swagger.io/docs/open-source-tools/swagger-codegen/) to generate these libraries.  ### Third-party client libraries  Some members of our amazing community have written libraries to work with our API. These aren't officially supported by Coda, but are listed here for convenience. (Please let us know if you've written a library and would like to have it included here.)  - [PHP](https://github.com/danielstieber/CodaPHP) by Daniel Stieber - [Node-RED](https://github.com/serene-water/node-red-contrib-coda-io) by Mori Sugimoto - [NodeJS](https://www.npmjs.com/package/coda-js) by Parker McMullin - [Ruby](https://rubygems.org/gems/coda_docs/) by Carlos Muñoz at Monday.vc - [Python](https://github.com/Blasterai/codaio) by Mikhail Beliansky 
 *
 * OpenAPI spec version: 1.0.0
 * Contact: help+api@coda.io
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using RestSharp;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace IO.Swagger.Api
{
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
        public interface IPublishingApi : IApiAccessor
    {
        #region Synchronous Operations
        /// <summary>
        /// Get doc categories
        /// </summary>
        /// <remarks>
        /// Gets all available doc categories.
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <returns>DocCategoryList</returns>
        DocCategoryList ListCategories ();

        /// <summary>
        /// Get doc categories
        /// </summary>
        /// <remarks>
        /// Gets all available doc categories.
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <returns>ApiResponse of DocCategoryList</returns>
        ApiResponse<DocCategoryList> ListCategoriesWithHttpInfo ();
        /// <summary>
        /// Publish doc
        /// </summary>
        /// <remarks>
        /// Update publish settings for a doc.
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="body">Parameters for changing publish settings.</param>
        /// <param name="docId">ID of the doc.</param>
        /// <returns>PublishResult</returns>
        PublishResult PublishDoc (DocPublish body, string docId);

        /// <summary>
        /// Publish doc
        /// </summary>
        /// <remarks>
        /// Update publish settings for a doc.
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="body">Parameters for changing publish settings.</param>
        /// <param name="docId">ID of the doc.</param>
        /// <returns>ApiResponse of PublishResult</returns>
        ApiResponse<PublishResult> PublishDocWithHttpInfo (DocPublish body, string docId);
        /// <summary>
        /// Unpublish doc
        /// </summary>
        /// <remarks>
        /// Unpublishes a doc.
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="docId">ID of the doc.</param>
        /// <returns>UnpublishResult</returns>
        UnpublishResult UnpublishDoc (string docId);

        /// <summary>
        /// Unpublish doc
        /// </summary>
        /// <remarks>
        /// Unpublishes a doc.
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="docId">ID of the doc.</param>
        /// <returns>ApiResponse of UnpublishResult</returns>
        ApiResponse<UnpublishResult> UnpublishDocWithHttpInfo (string docId);
        #endregion Synchronous Operations
        #region Asynchronous Operations
        /// <summary>
        /// Get doc categories
        /// </summary>
        /// <remarks>
        /// Gets all available doc categories.
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <returns>Task of DocCategoryList</returns>
        System.Threading.Tasks.Task<DocCategoryList> ListCategoriesAsync ();

        /// <summary>
        /// Get doc categories
        /// </summary>
        /// <remarks>
        /// Gets all available doc categories.
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <returns>Task of ApiResponse (DocCategoryList)</returns>
        System.Threading.Tasks.Task<ApiResponse<DocCategoryList>> ListCategoriesAsyncWithHttpInfo ();
        /// <summary>
        /// Publish doc
        /// </summary>
        /// <remarks>
        /// Update publish settings for a doc.
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="body">Parameters for changing publish settings.</param>
        /// <param name="docId">ID of the doc.</param>
        /// <returns>Task of PublishResult</returns>
        System.Threading.Tasks.Task<PublishResult> PublishDocAsync (DocPublish body, string docId);

        /// <summary>
        /// Publish doc
        /// </summary>
        /// <remarks>
        /// Update publish settings for a doc.
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="body">Parameters for changing publish settings.</param>
        /// <param name="docId">ID of the doc.</param>
        /// <returns>Task of ApiResponse (PublishResult)</returns>
        System.Threading.Tasks.Task<ApiResponse<PublishResult>> PublishDocAsyncWithHttpInfo (DocPublish body, string docId);
        /// <summary>
        /// Unpublish doc
        /// </summary>
        /// <remarks>
        /// Unpublishes a doc.
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="docId">ID of the doc.</param>
        /// <returns>Task of UnpublishResult</returns>
        System.Threading.Tasks.Task<UnpublishResult> UnpublishDocAsync (string docId);

        /// <summary>
        /// Unpublish doc
        /// </summary>
        /// <remarks>
        /// Unpublishes a doc.
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="docId">ID of the doc.</param>
        /// <returns>Task of ApiResponse (UnpublishResult)</returns>
        System.Threading.Tasks.Task<ApiResponse<UnpublishResult>> UnpublishDocAsyncWithHttpInfo (string docId);
        #endregion Asynchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
        public partial class PublishingApi : IPublishingApi
    {
        private IO.Swagger.Client.ExceptionFactory _exceptionFactory = (name, response) => null;

        /// <summary>
        /// Initializes a new instance of the <see cref="PublishingApi"/> class.
        /// </summary>
        /// <returns></returns>
        public PublishingApi(String basePath)
        {
            this.Configuration = new IO.Swagger.Client.Configuration { BasePath = basePath };

            ExceptionFactory = IO.Swagger.Client.Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PublishingApi"/> class
        /// </summary>
        /// <returns></returns>
        public PublishingApi()
        {
            this.Configuration = IO.Swagger.Client.Configuration.Default;

            ExceptionFactory = IO.Swagger.Client.Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PublishingApi"/> class
        /// using Configuration object
        /// </summary>
        /// <param name="configuration">An instance of Configuration</param>
        /// <returns></returns>
        public PublishingApi(IO.Swagger.Client.Configuration configuration = null)
        {
            if (configuration == null) // use the default one in Configuration
                this.Configuration = IO.Swagger.Client.Configuration.Default;
            else
                this.Configuration = configuration;

            ExceptionFactory = IO.Swagger.Client.Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// Gets the base path of the API client.
        /// </summary>
        /// <value>The base path</value>
        public String GetBasePath()
        {
            return this.Configuration.ApiClient.RestClient.BaseUrl.ToString();
        }

        /// <summary>
        /// Sets the base path of the API client.
        /// </summary>
        /// <value>The base path</value>
        [Obsolete("SetBasePath is deprecated, please do 'Configuration.ApiClient = new ApiClient(\"http://new-path\")' instead.")]
        public void SetBasePath(String basePath)
        {
            // do nothing
        }

        /// <summary>
        /// Gets or sets the configuration object
        /// </summary>
        /// <value>An instance of the Configuration</value>
        public IO.Swagger.Client.Configuration Configuration {get; set;}

        /// <summary>
        /// Provides a factory method hook for the creation of exceptions.
        /// </summary>
        public IO.Swagger.Client.ExceptionFactory ExceptionFactory
        {
            get
            {
                if (_exceptionFactory != null && _exceptionFactory.GetInvocationList().Length > 1)
                {
                    throw new InvalidOperationException("Multicast delegate for ExceptionFactory is unsupported.");
                }
                return _exceptionFactory;
            }
            set { _exceptionFactory = value; }
        }

        /// <summary>
        /// Gets the default header.
        /// </summary>
        /// <returns>Dictionary of HTTP header</returns>
        [Obsolete("DefaultHeader is deprecated, please use Configuration.DefaultHeader instead.")]
        public IDictionary<String, String> DefaultHeader()
        {
            return new ReadOnlyDictionary<string, string>(this.Configuration.DefaultHeader);
        }

        /// <summary>
        /// Add default header.
        /// </summary>
        /// <param name="key">Header field name.</param>
        /// <param name="value">Header field value.</param>
        /// <returns></returns>
        [Obsolete("AddDefaultHeader is deprecated, please use Configuration.AddDefaultHeader instead.")]
        public void AddDefaultHeader(string key, string value)
        {
            this.Configuration.AddDefaultHeader(key, value);
        }

        /// <summary>
        /// Get doc categories Gets all available doc categories.
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <returns>DocCategoryList</returns>
        public DocCategoryList ListCategories ()
        {
             ApiResponse<DocCategoryList> localVarResponse = ListCategoriesWithHttpInfo();
             return localVarResponse.Data;
        }

        /// <summary>
        /// Get doc categories Gets all available doc categories.
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <returns>ApiResponse of DocCategoryList</returns>
        public ApiResponse< DocCategoryList > ListCategoriesWithHttpInfo ()
        {

            var localVarPath = "/categories";
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new List<KeyValuePair<String, String>>();
            var localVarHeaderParams = new Dictionary<String, String>(this.Configuration.DefaultHeader);
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            // to determine the Content-Type header
            String[] localVarHttpContentTypes = new String[] {
            };
            String localVarHttpContentType = this.Configuration.ApiClient.SelectHeaderContentType(localVarHttpContentTypes);

            // to determine the Accept header
            String[] localVarHttpHeaderAccepts = new String[] {
                "application/json"
            };
            String localVarHttpHeaderAccept = this.Configuration.ApiClient.SelectHeaderAccept(localVarHttpHeaderAccepts);
            if (localVarHttpHeaderAccept != null)
                localVarHeaderParams.Add("Accept", localVarHttpHeaderAccept);

            // authentication (Bearer) required
            // bearer required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarHeaderParams["Authorization"] = "Bearer " + this.Configuration.AccessToken;
            }

            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse) this.Configuration.ApiClient.CallApi(localVarPath,
                Method.GET, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int) localVarResponse.StatusCode;

            if (ExceptionFactory != null)
            {
                Exception exception = ExceptionFactory("ListCategories", localVarResponse);
                if (exception != null) throw exception;
            }

            return new ApiResponse<DocCategoryList>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => string.Join(",", x.Value)),
                (DocCategoryList) this.Configuration.ApiClient.Deserialize(localVarResponse, typeof(DocCategoryList)));
        }

        /// <summary>
        /// Get doc categories Gets all available doc categories.
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <returns>Task of DocCategoryList</returns>
        public async System.Threading.Tasks.Task<DocCategoryList> ListCategoriesAsync ()
        {
             ApiResponse<DocCategoryList> localVarResponse = await ListCategoriesAsyncWithHttpInfo();
             return localVarResponse.Data;

        }

        /// <summary>
        /// Get doc categories Gets all available doc categories.
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <returns>Task of ApiResponse (DocCategoryList)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<DocCategoryList>> ListCategoriesAsyncWithHttpInfo ()
        {

            var localVarPath = "/categories";
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new List<KeyValuePair<String, String>>();
            var localVarHeaderParams = new Dictionary<String, String>(this.Configuration.DefaultHeader);
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            // to determine the Content-Type header
            String[] localVarHttpContentTypes = new String[] {
            };
            String localVarHttpContentType = this.Configuration.ApiClient.SelectHeaderContentType(localVarHttpContentTypes);

            // to determine the Accept header
            String[] localVarHttpHeaderAccepts = new String[] {
                "application/json"
            };
            String localVarHttpHeaderAccept = this.Configuration.ApiClient.SelectHeaderAccept(localVarHttpHeaderAccepts);
            if (localVarHttpHeaderAccept != null)
                localVarHeaderParams.Add("Accept", localVarHttpHeaderAccept);

            // authentication (Bearer) required
            // bearer required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarHeaderParams["Authorization"] = "Bearer " + this.Configuration.AccessToken;
            }

            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse) await this.Configuration.ApiClient.CallApiAsync(localVarPath,
                Method.GET, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int) localVarResponse.StatusCode;

            if (ExceptionFactory != null)
            {
                Exception exception = ExceptionFactory("ListCategories", localVarResponse);
                if (exception != null) throw exception;
            }

            return new ApiResponse<DocCategoryList>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => string.Join(",", x.Value)),
                (DocCategoryList) this.Configuration.ApiClient.Deserialize(localVarResponse, typeof(DocCategoryList)));
        }

        /// <summary>
        /// Publish doc Update publish settings for a doc.
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="body">Parameters for changing publish settings.</param>
        /// <param name="docId">ID of the doc.</param>
        /// <returns>PublishResult</returns>
        public PublishResult PublishDoc (DocPublish body, string docId)
        {
             ApiResponse<PublishResult> localVarResponse = PublishDocWithHttpInfo(body, docId);
             return localVarResponse.Data;
        }

        /// <summary>
        /// Publish doc Update publish settings for a doc.
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="body">Parameters for changing publish settings.</param>
        /// <param name="docId">ID of the doc.</param>
        /// <returns>ApiResponse of PublishResult</returns>
        public ApiResponse< PublishResult > PublishDocWithHttpInfo (DocPublish body, string docId)
        {
            // verify the required parameter 'body' is set
            if (body == null)
                throw new ApiException(400, "Missing required parameter 'body' when calling PublishingApi->PublishDoc");
            // verify the required parameter 'docId' is set
            if (docId == null)
                throw new ApiException(400, "Missing required parameter 'docId' when calling PublishingApi->PublishDoc");

            var localVarPath = "/docs/{docId}/publish";
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new List<KeyValuePair<String, String>>();
            var localVarHeaderParams = new Dictionary<String, String>(this.Configuration.DefaultHeader);
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            // to determine the Content-Type header
            String[] localVarHttpContentTypes = new String[] {
                "application/json"
            };
            String localVarHttpContentType = this.Configuration.ApiClient.SelectHeaderContentType(localVarHttpContentTypes);

            // to determine the Accept header
            String[] localVarHttpHeaderAccepts = new String[] {
                "application/json"
            };
            String localVarHttpHeaderAccept = this.Configuration.ApiClient.SelectHeaderAccept(localVarHttpHeaderAccepts);
            if (localVarHttpHeaderAccept != null)
                localVarHeaderParams.Add("Accept", localVarHttpHeaderAccept);

            if (docId != null) localVarPathParams.Add("docId", this.Configuration.ApiClient.ParameterToString(docId)); // path parameter
            if (body != null && body.GetType() != typeof(byte[]))
            {
                localVarPostBody = this.Configuration.ApiClient.Serialize(body); // http body (model) parameter
            }
            else
            {
                localVarPostBody = body; // byte array
            }
            // authentication (Bearer) required
            // bearer required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarHeaderParams["Authorization"] = "Bearer " + this.Configuration.AccessToken;
            }

            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse) this.Configuration.ApiClient.CallApi(localVarPath,
                Method.PUT, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int) localVarResponse.StatusCode;

            if (ExceptionFactory != null)
            {
                Exception exception = ExceptionFactory("PublishDoc", localVarResponse);
                if (exception != null) throw exception;
            }

            return new ApiResponse<PublishResult>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => string.Join(",", x.Value)),
                (PublishResult) this.Configuration.ApiClient.Deserialize(localVarResponse, typeof(PublishResult)));
        }

        /// <summary>
        /// Publish doc Update publish settings for a doc.
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="body">Parameters for changing publish settings.</param>
        /// <param name="docId">ID of the doc.</param>
        /// <returns>Task of PublishResult</returns>
        public async System.Threading.Tasks.Task<PublishResult> PublishDocAsync (DocPublish body, string docId)
        {
             ApiResponse<PublishResult> localVarResponse = await PublishDocAsyncWithHttpInfo(body, docId);
             return localVarResponse.Data;

        }

        /// <summary>
        /// Publish doc Update publish settings for a doc.
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="body">Parameters for changing publish settings.</param>
        /// <param name="docId">ID of the doc.</param>
        /// <returns>Task of ApiResponse (PublishResult)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<PublishResult>> PublishDocAsyncWithHttpInfo (DocPublish body, string docId)
        {
            // verify the required parameter 'body' is set
            if (body == null)
                throw new ApiException(400, "Missing required parameter 'body' when calling PublishingApi->PublishDoc");
            // verify the required parameter 'docId' is set
            if (docId == null)
                throw new ApiException(400, "Missing required parameter 'docId' when calling PublishingApi->PublishDoc");

            var localVarPath = "/docs/{docId}/publish";
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new List<KeyValuePair<String, String>>();
            var localVarHeaderParams = new Dictionary<String, String>(this.Configuration.DefaultHeader);
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            // to determine the Content-Type header
            String[] localVarHttpContentTypes = new String[] {
                "application/json"
            };
            String localVarHttpContentType = this.Configuration.ApiClient.SelectHeaderContentType(localVarHttpContentTypes);

            // to determine the Accept header
            String[] localVarHttpHeaderAccepts = new String[] {
                "application/json"
            };
            String localVarHttpHeaderAccept = this.Configuration.ApiClient.SelectHeaderAccept(localVarHttpHeaderAccepts);
            if (localVarHttpHeaderAccept != null)
                localVarHeaderParams.Add("Accept", localVarHttpHeaderAccept);

            if (docId != null) localVarPathParams.Add("docId", this.Configuration.ApiClient.ParameterToString(docId)); // path parameter
            if (body != null && body.GetType() != typeof(byte[]))
            {
                localVarPostBody = this.Configuration.ApiClient.Serialize(body); // http body (model) parameter
            }
            else
            {
                localVarPostBody = body; // byte array
            }
            // authentication (Bearer) required
            // bearer required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarHeaderParams["Authorization"] = "Bearer " + this.Configuration.AccessToken;
            }

            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse) await this.Configuration.ApiClient.CallApiAsync(localVarPath,
                Method.PUT, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int) localVarResponse.StatusCode;

            if (ExceptionFactory != null)
            {
                Exception exception = ExceptionFactory("PublishDoc", localVarResponse);
                if (exception != null) throw exception;
            }

            return new ApiResponse<PublishResult>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => string.Join(",", x.Value)),
                (PublishResult) this.Configuration.ApiClient.Deserialize(localVarResponse, typeof(PublishResult)));
        }

        /// <summary>
        /// Unpublish doc Unpublishes a doc.
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="docId">ID of the doc.</param>
        /// <returns>UnpublishResult</returns>
        public UnpublishResult UnpublishDoc (string docId)
        {
             ApiResponse<UnpublishResult> localVarResponse = UnpublishDocWithHttpInfo(docId);
             return localVarResponse.Data;
        }

        /// <summary>
        /// Unpublish doc Unpublishes a doc.
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="docId">ID of the doc.</param>
        /// <returns>ApiResponse of UnpublishResult</returns>
        public ApiResponse< UnpublishResult > UnpublishDocWithHttpInfo (string docId)
        {
            // verify the required parameter 'docId' is set
            if (docId == null)
                throw new ApiException(400, "Missing required parameter 'docId' when calling PublishingApi->UnpublishDoc");

            var localVarPath = "/docs/{docId}/publish";
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new List<KeyValuePair<String, String>>();
            var localVarHeaderParams = new Dictionary<String, String>(this.Configuration.DefaultHeader);
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            // to determine the Content-Type header
            String[] localVarHttpContentTypes = new String[] {
            };
            String localVarHttpContentType = this.Configuration.ApiClient.SelectHeaderContentType(localVarHttpContentTypes);

            // to determine the Accept header
            String[] localVarHttpHeaderAccepts = new String[] {
                "application/json"
            };
            String localVarHttpHeaderAccept = this.Configuration.ApiClient.SelectHeaderAccept(localVarHttpHeaderAccepts);
            if (localVarHttpHeaderAccept != null)
                localVarHeaderParams.Add("Accept", localVarHttpHeaderAccept);

            if (docId != null) localVarPathParams.Add("docId", this.Configuration.ApiClient.ParameterToString(docId)); // path parameter
            // authentication (Bearer) required
            // bearer required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarHeaderParams["Authorization"] = "Bearer " + this.Configuration.AccessToken;
            }

            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse) this.Configuration.ApiClient.CallApi(localVarPath,
                Method.DELETE, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int) localVarResponse.StatusCode;

            if (ExceptionFactory != null)
            {
                Exception exception = ExceptionFactory("UnpublishDoc", localVarResponse);
                if (exception != null) throw exception;
            }

            return new ApiResponse<UnpublishResult>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => string.Join(",", x.Value)),
                (UnpublishResult) this.Configuration.ApiClient.Deserialize(localVarResponse, typeof(UnpublishResult)));
        }

        /// <summary>
        /// Unpublish doc Unpublishes a doc.
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="docId">ID of the doc.</param>
        /// <returns>Task of UnpublishResult</returns>
        public async System.Threading.Tasks.Task<UnpublishResult> UnpublishDocAsync (string docId)
        {
             ApiResponse<UnpublishResult> localVarResponse = await UnpublishDocAsyncWithHttpInfo(docId);
             return localVarResponse.Data;

        }

        /// <summary>
        /// Unpublish doc Unpublishes a doc.
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="docId">ID of the doc.</param>
        /// <returns>Task of ApiResponse (UnpublishResult)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<UnpublishResult>> UnpublishDocAsyncWithHttpInfo (string docId)
        {
            // verify the required parameter 'docId' is set
            if (docId == null)
                throw new ApiException(400, "Missing required parameter 'docId' when calling PublishingApi->UnpublishDoc");

            var localVarPath = "/docs/{docId}/publish";
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new List<KeyValuePair<String, String>>();
            var localVarHeaderParams = new Dictionary<String, String>(this.Configuration.DefaultHeader);
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            // to determine the Content-Type header
            String[] localVarHttpContentTypes = new String[] {
            };
            String localVarHttpContentType = this.Configuration.ApiClient.SelectHeaderContentType(localVarHttpContentTypes);

            // to determine the Accept header
            String[] localVarHttpHeaderAccepts = new String[] {
                "application/json"
            };
            String localVarHttpHeaderAccept = this.Configuration.ApiClient.SelectHeaderAccept(localVarHttpHeaderAccepts);
            if (localVarHttpHeaderAccept != null)
                localVarHeaderParams.Add("Accept", localVarHttpHeaderAccept);

            if (docId != null) localVarPathParams.Add("docId", this.Configuration.ApiClient.ParameterToString(docId)); // path parameter
            // authentication (Bearer) required
            // bearer required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarHeaderParams["Authorization"] = "Bearer " + this.Configuration.AccessToken;
            }

            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse) await this.Configuration.ApiClient.CallApiAsync(localVarPath,
                Method.DELETE, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int) localVarResponse.StatusCode;

            if (ExceptionFactory != null)
            {
                Exception exception = ExceptionFactory("UnpublishDoc", localVarResponse);
                if (exception != null) throw exception;
            }

            return new ApiResponse<UnpublishResult>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => string.Join(",", x.Value)),
                (UnpublishResult) this.Configuration.ApiClient.Deserialize(localVarResponse, typeof(UnpublishResult)));
        }

    }
}

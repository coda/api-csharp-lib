# IO.Swagger - the C# library for the Coda API

# Introduction  The Coda API is a RESTful API that lets you programmatically interact with Coda docs:   * List and search Coda docs  * Create new docs and copy existing ones  * Share and publish docs  * Discover pages, tables, formulas, and controls  * Read, insert, upsert, update, and delete rows  Version 1 of the API will be supported until at least January 15, 2021. As we update and release newer versions of the API, we reserve the right to remove older APIs and functionality with a 3-month deprecation notice. We will post about such changes as well as announce new features in the [Developers Central](https://community.coda.io/c/developers-central) section of our Community, and update the [API updates](https://coda.io/api-updates) doc.  # Getting Started  Our [Getting Started Guide](https://coda.io/t/Getting-Started-Guide-Coda-API_toujpmwflfy) helps you learn the basic of working with the API and shows a few ways you can use it. Check it out, and learn how to:  - Read data from Coda tables and write back to them - Build a one-way sync from one Coda doc to another - Automate reminders - Sync your Google Calendar to Coda  # Using the API  Coda's REST API is designed to be straightforward to use. You can use the language and platform of your choice to make requests. To get a feel for the API, you can also use a tool like [Postman](https://www.getpostman.com/) or [Insomnia](https://insomnia.rest/).  ## API Endpoint  This API uses a base path of `https://coda.io/apis/v1`.  ## Resource IDs and Links  Each resource instance retrieved via the API has the following fields:    - `id`: The resource's immutable ID, which can be used to refer to it within its context   - `type`: The type of resource, useful for identifying it in a heterogenous collection of results   - `href`: A fully qualified URI that can be used to refer to and get the latest details on the resource  Most resources can be queried by their name or ID. We recommend sticking with IDs where possible, as names are fragile and prone to being changed by your doc's users.  ### List Endpoints  Endpoints supporting listing of resources have the following fields:    - `items`: An array containing the listed resources, limited by the `limit` and `pageToken` query parameters   - `nextPageLink`: If more results are available, an API link to the next page of results   - `nextPageToken`: If more results are available, a page token that can be passed into the `pageToken` query parameter  **The maximum page size may change at any time, and may be different for different endpoints.** Please do not rely on it for any behavior of your application. If you pass a `limit` parameter that is larger than our maximum allowed limit, we will only return as many results as our maximum limit. You should look for the presence of the `nextPageToken` on the response to see if there are more results available, rather than relying on a result set that matches your provided limit.  ### Doc IDs  While most object IDs will have to be discovered via the API, you may find yourself frequently wanting to get the ID of a specific Coda doc.  Here's a handy tool that will extract it for you. (See if you can find the pattern!)  <form>   <fieldset style=\"margin: 0px 25px 25px 25px; display: inline;\">     <legend>Doc ID Extractor</legend>     <input type=\"text\" id=\"de_docUrl\" placeholder=\"Paste in a Coda doc URL\"            style=\"width: 250px; padding: 8px; margin-right: 20px;\" />     <span>       Your doc ID is:&nbsp;&nbsp;&nbsp;       <input id=\"de_docId\" readonly=\"true\"              style=\"width: 150px; padding: 8px; font-family: monospace; border: 1px dashed gray;\" />   </fieldset> </form>  <script>   (() => {     const docUrl = document.getElementById('de_docUrl');     const docId = document.getElementById('de_docId');     docUrl.addEventListener('input', () => {       docId.value = (docUrl.value.match(/_d([\\w-]+)/) || [])[1] || '';     });     docId.addEventListener('mousedown', () => docId.select());     docId.addEventListener('click', () => docId.select());   })(); </script>  ## Rate Limiting  The Coda API sets a reasonable limit on the number of requests that can be made per minute. Once this limit is reached, calls to the API will start returning errors with an HTTP status code of 429. If you find yourself hitting rate limits and would like your individual rate to be raised, please contact us at <help+api@coda.io>.  ## Consistency  While edits made in Coda are shared with other collaborators in real-time, it can take a few seconds for them to become available via the API. You may also notice that changes made via the API, such as updating a row, are not immediate. These endpoints all return an HTTP 202 status code, instead of a standard 200, indicating that the edit has been accepted and queued for processing. This generally takes a few seconds, and the edit may fail if invalid. Each such edit will return a `requestId` in the response, and you can pass this `requestId` to the [`#getMutationStatus`](#operation/getMutationStatus) endpoint to find out if it has been applied.  ## Volatile Formulas  Coda exposes a number of \"volatile\" formulas, as as `Today()`, `Now()`, and `User()`. When used in a live Coda doc, these formulas affect what's visible in realtime, tailored to the current user.  Such formulas behave differently with the API. Time-based values may only be current to the last edit made to the doc. User-based values may be blank or invalid.  ## Free and Paid Workspaces  We make the Coda API available to all of our users free of charge, in both free and paid workspaces. However, API usage is subject to the role of the user associated with the API token in the workspace applicable to each API request. What this means is:  - For the [`#createDoc`](#operation/createDoc) endpoint specifically, the owner of the API token must be a Doc   Maker (or Admin) in the workspace. If the \"Any member can create docs\" option in enabled in the workspace   settings, they can be an Editor and will get auto-promoted to Doc Maker upon using this endpoint. Lastly, if in   addition, the API key owner matches the \"Approved email domains\" setting, they will be auto-added to the   workspace and promoted to Doc Maker upon using this endpoint  This behavior applies to the API as well as any integrations that may use it, such as Zapier.  ## Examples  To help you get started, this documentation provides code examples in Python, Unix shell, and Google Apps Script. These examples are based on a simple doc that looks something like this:  ![](https://cdn.coda.io/external/img/api_example_doc.png)  ### Python examples  These examples use Python 3.6+. If you don't already have the `requests` module, use `pip` or `easy_install` to get it.  ### Shell examples  The shell examples are intended to be run in a Unix shell. If you're on Windows, you will need to install [WSL](https://docs.microsoft.com/en-us/windows/wsl/install-win10).  These examples use the standard cURL utility to pull from the API, and then process it with `jq` to extract and format example output. If you don't already have it, you can either [install it](https://stedolan.github.io/jq/) or run the command without it to see the raw JSON output.  ### Google Apps Script examples  ![](https://cdn.coda.io/external/img/api_gas.png)  [Google Apps Script](https://script.google.com/) makes it easy to write code in a JavaScript-like syntax and easily access many Google products with built-in libraries. You can set up your scripts to run periodically, which makes it a good environment for writing tools without maintaining your own server.  Coda provides a library for Google Apps Script. To use it, go into `Resources -> Libraries...` and enter the following library ID: `15IQuWOk8MqT50FDWomh57UqWGH23gjsWVWYFms3ton6L-UHmefYHS9Vl`. If you want to see the library's source code, it's available [here](https://script.google.com/d/15IQuWOk8MqT50FDWomh57UqWGH23gjsWVWYFms3ton6L-UHmefYHS9Vl/edit).  Google provides autocomplete for API functions as well as generated docs. You can access these docs via the Libraries dialog by clicking on the library name. Required parameters that would be included in the URL path are positional arguments in each of these functions, followed by the request body, if applicable. All remaining parameters can be specified in the options object.  ## OpenAPI/Swagger Spec  In an effort to standardize our API and make it accessible, we offer an OpenAPI 3.0 specification:  - [OpenAPI 3.0 spec - YAML](https://coda.io/apis/v1/openapi.yaml) - [OpenAPI 3.0 spec - JSON](https://coda.io/apis/v1/openapi.json)  ### Swagger 2.0  We also offer a downgraded Swagger 2.0 version of our specification. This may be useful for a number of tools that haven't yet been adapted to OpenAPI 3.0. Here are the links:  - [Swagger 2.0 spec - YAML](https://coda.io/apis/v1/swagger.yaml) - [Swagger 2.0 spec - JSON](https://coda.io/apis/v1/swagger.json)  #### Postman collection  To get started with prototyping the API quickly in Postman, you can use one of links above to import the Coda API into a collection. You'll then need to set the [appropriate header](#section/Authentication) and environment variables.  ## Client libraries  We do not currently support client libraries apart from Google Apps Script. To work with the Coda API, you can either use standard network libraries for your language, or use the appropriate Swagger Generator tool to auto-generate Coda API client libraries for your language of choice. We do not provide any guarantees that these autogenerated libraries are compatible with our API (e.g., some libraries may not work with Bearer authentication).  ### OpenAPI 3.0  [Swagger Generator 3](https://generator3.swagger.io/) (that link takes you to the docs for the generator API) can generate client libraries for [these languages](https://generator3.swagger.io/v2/clients). It's relatively new and thus only has support for a limited set of languages at this time.  ### Swagger 2.0  [Swagger Generator](https://generator.swagger.io/) takes in a legacy Swagger 2.0 specification, but can generate client libraries for [more languages](http://generator.swagger.io/api/gen/clients). You can also use local [CLI tools](https://swagger.io/docs/open-source-tools/swagger-codegen/) to generate these libraries.  ### Third-party client libraries  Some members of our amazing community have written libraries to work with our API. These aren't officially supported by Coda, but are listed here for convenience. (Please let us know if you've written a library and would like to have it included here.)  - [PHP](https://github.com/danielstieber/CodaPHP) by Daniel Stieber - [Node-RED](https://github.com/serene-water/node-red-contrib-coda-io) by Mori Sugimoto - [NodeJS](https://www.npmjs.com/package/coda-js) by Parker McMullin - [Ruby](https://rubygems.org/gems/coda_docs/) by Carlos Muñoz at Monday.vc - [Python](https://github.com/Blasterai/codaio) by Mikhail Beliansky 

This C# SDK is automatically generated by the [Swagger Codegen](https://github.com/swagger-api/swagger-codegen) project:

- API version: 1.0.0
- SDK version: 1.0.0
- Build package: io.swagger.codegen.v3.generators.dotnet.CSharpClientCodegen
    For more information, please visit [https://coda.io](https://coda.io)

<a name="frameworks-supported"></a>
## Frameworks supported
- .NET 4.0 or later
- Windows Phone 7.1 (Mango)

<a name="dependencies"></a>
## Dependencies
- [RestSharp](https://www.nuget.org/packages/RestSharp) - 105.1.0 or later
- [Json.NET](https://www.nuget.org/packages/Newtonsoft.Json/) - 7.0.0 or later
- [JsonSubTypes](https://www.nuget.org/packages/JsonSubTypes/) - 1.2.0 or later

The DLLs included in the package may not be the latest version. We recommend using [NuGet](https://docs.nuget.org/consume/installing-nuget) to obtain the latest version of the packages:
```
Install-Package RestSharp
Install-Package Newtonsoft.Json
Install-Package JsonSubTypes
```

NOTE: RestSharp versions greater than 105.1.0 have a bug which causes file uploads to fail. See [RestSharp#742](https://github.com/restsharp/RestSharp/issues/742)

<a name="installation"></a>
## Installation
Run the following command to generate the DLL
- [Mac/Linux] `/bin/sh build.sh`
- [Windows] `build.bat`

Then include the DLL (under the `bin` folder) in the C# project, and use the namespaces:
```csharp
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;
```
<a name="packaging"></a>
## Packaging

A `.nuspec` is included with the project. You can follow the Nuget quickstart to [create](https://docs.microsoft.com/en-us/nuget/quickstart/create-and-publish-a-package#create-the-package) and [publish](https://docs.microsoft.com/en-us/nuget/quickstart/create-and-publish-a-package#publish-the-package) packages.

This `.nuspec` uses placeholders from the `.csproj`, so build the `.csproj` directly:

```
nuget pack -Build -OutputDirectory out IO.Swagger.csproj
```

Then, publish to a [local feed](https://docs.microsoft.com/en-us/nuget/hosting-packages/local-feeds) or [other host](https://docs.microsoft.com/en-us/nuget/hosting-packages/overview) and consume the new package via Nuget as usual.

<a name="getting-started"></a>
## Getting Started

```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class Example
    {
        public void main()
        {

            var apiInstance = new AccountApi();

            try
            {
                // Get user info
                User result = apiInstance.Whoami();
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling AccountApi.Whoami: " + e.Message );
            }
        }
    }
}
```

<a name="documentation-for-api-endpoints"></a>
## Documentation for API Endpoints

All URIs are relative to *https://coda.io/apis/v1*

Class | Method | HTTP request | Description
------------ | ------------- | ------------- | -------------
*AccountApi* | [**Whoami**](docs/AccountApi.md#whoami) | **GET** /whoami | Get user info
*AclApi* | [**DeletePermission**](docs/AclApi.md#deletepermission) | **DELETE** /docs/{docId}/acl/permissions/{permissionId} | Delete permission
*AclApi* | [**GetAclMetadata**](docs/AclApi.md#getaclmetadata) | **GET** /docs/{docId}/acl/metadata | AclMetadata
*AclApi* | [**GetAclPermissions**](docs/AclApi.md#getaclpermissions) | **GET** /docs/{docId}/acl/permissions | Acl
*AclApi* | [**ShareDoc**](docs/AclApi.md#sharedoc) | **POST** /docs/{docId}/acl/permissions | Add permission
*ColumnsApi* | [**GetColumn**](docs/ColumnsApi.md#getcolumn) | **GET** /docs/{docId}/tables/{tableIdOrName}/columns/{columnIdOrName} | Get a column
*ColumnsApi* | [**ListColumns**](docs/ColumnsApi.md#listcolumns) | **GET** /docs/{docId}/tables/{tableIdOrName}/columns | List columns
*ControlsApi* | [**GetControl**](docs/ControlsApi.md#getcontrol) | **GET** /docs/{docId}/controls/{controlIdOrName} | Get a control
*ControlsApi* | [**ListControls**](docs/ControlsApi.md#listcontrols) | **GET** /docs/{docId}/controls | List controls
*DocsApi* | [**CreateDoc**](docs/DocsApi.md#createdoc) | **POST** /docs | Create doc
*DocsApi* | [**DeleteDoc**](docs/DocsApi.md#deletedoc) | **DELETE** /docs/{docId} | Delete doc
*DocsApi* | [**GetDoc**](docs/DocsApi.md#getdoc) | **GET** /docs/{docId} | Get info about a doc
*DocsApi* | [**ListDocs**](docs/DocsApi.md#listdocs) | **GET** /docs | List available docs
*FormulasApi* | [**GetFormula**](docs/FormulasApi.md#getformula) | **GET** /docs/{docId}/formulas/{formulaIdOrName} | Get a formula
*FormulasApi* | [**ListFormulas**](docs/FormulasApi.md#listformulas) | **GET** /docs/{docId}/formulas | List formulas
*MiscellaneousApi* | [**GetMutationStatus**](docs/MiscellaneousApi.md#getmutationstatus) | **GET** /mutationStatus/{requestId} | Get mutation status
*MiscellaneousApi* | [**ResolveBrowserLink**](docs/MiscellaneousApi.md#resolvebrowserlink) | **GET** /resolveBrowserLink | Resolve browser link
*PagesApi* | [**GetPage**](docs/PagesApi.md#getpage) | **GET** /docs/{docId}/pages/{pageIdOrName} | Get a page
*PagesApi* | [**ListPages**](docs/PagesApi.md#listpages) | **GET** /docs/{docId}/pages | List pages
*PagesApi* | [**UpdatePage**](docs/PagesApi.md#updatepage) | **PUT** /docs/{docId}/pages/{pageIdOrName} | Update a page
*PublishingApi* | [**ListCategories**](docs/PublishingApi.md#listcategories) | **GET** /categories | Get doc categories
*PublishingApi* | [**PublishDoc**](docs/PublishingApi.md#publishdoc) | **PUT** /docs/{docId}/publish | Publish doc
*PublishingApi* | [**UnpublishDoc**](docs/PublishingApi.md#unpublishdoc) | **DELETE** /docs/{docId}/publish | Unpublish doc
*RowsApi* | [**DeleteRow**](docs/RowsApi.md#deleterow) | **DELETE** /docs/{docId}/tables/{tableIdOrName}/rows/{rowIdOrName} | Delete row
*RowsApi* | [**DeleteRows**](docs/RowsApi.md#deleterows) | **DELETE** /docs/{docId}/tables/{tableIdOrName}/rows | Delete multiple rows
*RowsApi* | [**GetRow**](docs/RowsApi.md#getrow) | **GET** /docs/{docId}/tables/{tableIdOrName}/rows/{rowIdOrName} | Get a row
*RowsApi* | [**ListRows**](docs/RowsApi.md#listrows) | **GET** /docs/{docId}/tables/{tableIdOrName}/rows | List table rows
*RowsApi* | [**PushButton**](docs/RowsApi.md#pushbutton) | **POST** /docs/{docId}/tables/{tableIdOrName}/rows/{rowIdOrName}/buttons/{columnIdOrName} | Push a button
*RowsApi* | [**UpdateRow**](docs/RowsApi.md#updaterow) | **PUT** /docs/{docId}/tables/{tableIdOrName}/rows/{rowIdOrName} | Update row
*RowsApi* | [**UpsertRows**](docs/RowsApi.md#upsertrows) | **POST** /docs/{docId}/tables/{tableIdOrName}/rows | Insert/upsert rows
*TablesApi* | [**GetTable**](docs/TablesApi.md#gettable) | **GET** /docs/{docId}/tables/{tableIdOrName} | Get a table
*TablesApi* | [**ListTables**](docs/TablesApi.md#listtables) | **GET** /docs/{docId}/tables | List tables

<a name="documentation-for-models"></a>
## Documentation for Models

 - [Model.AccessType](docs/AccessType.md)
 - [Model.Acl](docs/Acl.md)
 - [Model.AclMetadata](docs/AclMetadata.md)
 - [Model.AddPermission](docs/AddPermission.md)
 - [Model.AddPermissionResult](docs/AddPermissionResult.md)
 - [Model.AllOfAclNextPageLink](docs/AllOfAclNextPageLink.md)
 - [Model.AllOfColumnListNextPageLink](docs/AllOfColumnListNextPageLink.md)
 - [Model.AllOfControlListNextPageLink](docs/AllOfControlListNextPageLink.md)
 - [Model.AllOfDocListNextPageLink](docs/AllOfDocListNextPageLink.md)
 - [Model.AllOfDocSourceDoc](docs/AllOfDocSourceDoc.md)
 - [Model.AllOfFormulaListNextPageLink](docs/AllOfFormulaListNextPageLink.md)
 - [Model.AllOfPageListNextPageLink](docs/AllOfPageListNextPageLink.md)
 - [Model.AllOfRowListNextPageLink](docs/AllOfRowListNextPageLink.md)
 - [Model.AllOfTableFilter](docs/AllOfTableFilter.md)
 - [Model.AllOfTableListNextPageLink](docs/AllOfTableListNextPageLink.md)
 - [Model.AnyonePrincipal](docs/AnyonePrincipal.md)
 - [Model.ApiLink](docs/ApiLink.md)
 - [Model.ApiLinkResolvedResource](docs/ApiLinkResolvedResource.md)
 - [Model.CellEdit](docs/CellEdit.md)
 - [Model.CellValue](docs/CellValue.md)
 - [Model.Column](docs/Column.md)
 - [Model.ColumnDetail](docs/ColumnDetail.md)
 - [Model.ColumnFormat](docs/ColumnFormat.md)
 - [Model.ColumnFormatType](docs/ColumnFormatType.md)
 - [Model.ColumnList](docs/ColumnList.md)
 - [Model.ColumnReference](docs/ColumnReference.md)
 - [Model.Control](docs/Control.md)
 - [Model.ControlList](docs/ControlList.md)
 - [Model.ControlReference](docs/ControlReference.md)
 - [Model.ControlType](docs/ControlType.md)
 - [Model.CurrencyAmount](docs/CurrencyAmount.md)
 - [Model.CurrencyColumnFormat](docs/CurrencyColumnFormat.md)
 - [Model.CurrencyFormatType](docs/CurrencyFormatType.md)
 - [Model.CurrencyValue](docs/CurrencyValue.md)
 - [Model.DateColumnFormat](docs/DateColumnFormat.md)
 - [Model.DateTimeColumnFormat](docs/DateTimeColumnFormat.md)
 - [Model.DeletePermissionResult](docs/DeletePermissionResult.md)
 - [Model.Doc](docs/Doc.md)
 - [Model.DocCategory](docs/DocCategory.md)
 - [Model.DocCategoryList](docs/DocCategoryList.md)
 - [Model.DocCreate](docs/DocCreate.md)
 - [Model.DocDelete](docs/DocDelete.md)
 - [Model.DocList](docs/DocList.md)
 - [Model.DocPublish](docs/DocPublish.md)
 - [Model.DocPublishMode](docs/DocPublishMode.md)
 - [Model.DocPublished](docs/DocPublished.md)
 - [Model.DocReference](docs/DocReference.md)
 - [Model.DocSize](docs/DocSize.md)
 - [Model.DocumentMutateResponse](docs/DocumentMutateResponse.md)
 - [Model.DomainPrincipal](docs/DomainPrincipal.md)
 - [Model.DurationColumnFormat](docs/DurationColumnFormat.md)
 - [Model.DurationUnit](docs/DurationUnit.md)
 - [Model.EmailPrincipal](docs/EmailPrincipal.md)
 - [Model.Formula](docs/Formula.md)
 - [Model.FormulaDetail](docs/FormulaDetail.md)
 - [Model.FormulaList](docs/FormulaList.md)
 - [Model.FormulaReference](docs/FormulaReference.md)
 - [Model.Icon](docs/Icon.md)
 - [Model.IconSet](docs/IconSet.md)
 - [Model.Image](docs/Image.md)
 - [Model.ImageStatus](docs/ImageStatus.md)
 - [Model.ImageUrlValue](docs/ImageUrlValue.md)
 - [Model.InlineResponse400](docs/InlineResponse400.md)
 - [Model.InlineResponse401](docs/InlineResponse401.md)
 - [Model.InlineResponse403](docs/InlineResponse403.md)
 - [Model.InlineResponse404](docs/InlineResponse404.md)
 - [Model.InlineResponse410](docs/InlineResponse410.md)
 - [Model.InlineResponse429](docs/InlineResponse429.md)
 - [Model.Layout](docs/Layout.md)
 - [Model.LinkedDataObject](docs/LinkedDataObject.md)
 - [Model.LinkedDataType](docs/LinkedDataType.md)
 - [Model.MutationStatus](docs/MutationStatus.md)
 - [Model.NumberOrNumberFormula](docs/NumberOrNumberFormula.md)
 - [Model.NumericColumnFormat](docs/NumericColumnFormat.md)
 - [Model.OneOfCellValue](docs/OneOfCellValue.md)
 - [Model.OneOfColumnFormat](docs/OneOfColumnFormat.md)
 - [Model.OneOfCurrencyAmount](docs/OneOfCurrencyAmount.md)
 - [Model.OneOfNumberOrNumberFormula](docs/OneOfNumberOrNumberFormula.md)
 - [Model.OneOfPrincipal](docs/OneOfPrincipal.md)
 - [Model.OneOfRichSingleValue](docs/OneOfRichSingleValue.md)
 - [Model.OneOfRichValue](docs/OneOfRichValue.md)
 - [Model.OneOfScalarValue](docs/OneOfScalarValue.md)
 - [Model.OneOfValue](docs/OneOfValue.md)
 - [Model.Page](docs/Page.md)
 - [Model.PageList](docs/PageList.md)
 - [Model.PageReference](docs/PageReference.md)
 - [Model.PageUpdate](docs/PageUpdate.md)
 - [Model.PageUpdateResult](docs/PageUpdateResult.md)
 - [Model.Permission](docs/Permission.md)
 - [Model.PersonValue](docs/PersonValue.md)
 - [Model.Principal](docs/Principal.md)
 - [Model.PrincipalType](docs/PrincipalType.md)
 - [Model.PublishResult](docs/PublishResult.md)
 - [Model.PushButtonResult](docs/PushButtonResult.md)
 - [Model.ReferenceColumnFormat](docs/ReferenceColumnFormat.md)
 - [Model.RichSingleValue](docs/RichSingleValue.md)
 - [Model.RichValue](docs/RichValue.md)
 - [Model.Row](docs/Row.md)
 - [Model.RowDeleteResult](docs/RowDeleteResult.md)
 - [Model.RowDetail](docs/RowDetail.md)
 - [Model.RowEdit](docs/RowEdit.md)
 - [Model.RowList](docs/RowList.md)
 - [Model.RowUpdate](docs/RowUpdate.md)
 - [Model.RowUpdateResult](docs/RowUpdateResult.md)
 - [Model.RowValue](docs/RowValue.md)
 - [Model.RowsDelete](docs/RowsDelete.md)
 - [Model.RowsDeleteResult](docs/RowsDeleteResult.md)
 - [Model.RowsSortBy](docs/RowsSortBy.md)
 - [Model.RowsUpsert](docs/RowsUpsert.md)
 - [Model.RowsUpsertResult](docs/RowsUpsertResult.md)
 - [Model.ScalarValue](docs/ScalarValue.md)
 - [Model.ScaleColumnFormat](docs/ScaleColumnFormat.md)
 - [Model.SimpleColumnFormat](docs/SimpleColumnFormat.md)
 - [Model.SliderColumnFormat](docs/SliderColumnFormat.md)
 - [Model.Sort](docs/Sort.md)
 - [Model.SortBy](docs/SortBy.md)
 - [Model.SortDirection](docs/SortDirection.md)
 - [Model.Table](docs/Table.md)
 - [Model.TableList](docs/TableList.md)
 - [Model.TableReference](docs/TableReference.md)
 - [Model.TableType](docs/TableType.md)
 - [Model.TimeColumnFormat](docs/TimeColumnFormat.md)
 - [Model.Type](docs/Type.md)
 - [Model.UnpublishResult](docs/UnpublishResult.md)
 - [Model.UrlValue](docs/UrlValue.md)
 - [Model.User](docs/User.md)
 - [Model.Value](docs/Value.md)
 - [Model.ValueFormat](docs/ValueFormat.md)

<a name="documentation-for-authorization"></a>
## Documentation for Authorization

<a name="Bearer"></a>
### Bearer



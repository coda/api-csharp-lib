# IO.Swagger.Api.DocsApi

All URIs are relative to *https://coda.io/apis/v1*

Method | HTTP request | Description
------------- | ------------- | -------------
[**CreateDoc**](DocsApi.md#createdoc) | **POST** /docs | Create doc
[**DeleteDoc**](DocsApi.md#deletedoc) | **DELETE** /docs/{docId} | Delete doc
[**GetDoc**](DocsApi.md#getdoc) | **GET** /docs/{docId} | Get info about a doc
[**ListDocs**](DocsApi.md#listdocs) | **GET** /docs | List available docs

<a name="createdoc"></a>
# **CreateDoc**
> Doc CreateDoc (DocCreate body)

Create doc

Creates a new Coda doc, optionally copying an existing doc. Note that creating a doc requires you to be a Doc Maker in the applicable workspace (or be auto-promoted to one). 

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class CreateDocExample
    {
        public void main()
        {

            var apiInstance = new DocsApi();
            var body = new DocCreate(); // DocCreate | Parameters for creating the doc.

            try
            {
                // Create doc
                Doc result = apiInstance.CreateDoc(body);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DocsApi.CreateDoc: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **body** | [**DocCreate**](DocCreate.md)| Parameters for creating the doc. | 

### Return type

[**Doc**](Doc.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="deletedoc"></a>
# **DeleteDoc**
> DocDelete DeleteDoc (string docId)

Delete doc

Deletes a doc.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class DeleteDocExample
    {
        public void main()
        {

            var apiInstance = new DocsApi();
            var docId = docId_example;  // string | ID of the doc.

            try
            {
                // Delete doc
                DocDelete result = apiInstance.DeleteDoc(docId);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DocsApi.DeleteDoc: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **docId** | **string**| ID of the doc. | 

### Return type

[**DocDelete**](DocDelete.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="getdoc"></a>
# **GetDoc**
> Doc GetDoc (string docId)

Get info about a doc

Returns metadata for the specified doc.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class GetDocExample
    {
        public void main()
        {

            var apiInstance = new DocsApi();
            var docId = docId_example;  // string | ID of the doc.

            try
            {
                // Get info about a doc
                Doc result = apiInstance.GetDoc(docId);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DocsApi.GetDoc: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **docId** | **string**| ID of the doc. | 

### Return type

[**Doc**](Doc.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
<a name="listdocs"></a>
# **ListDocs**
> DocList ListDocs (bool? isOwner = null, string query = null, string sourceDoc = null, bool? isStarred = null, bool? inGallery = null, string workspaceId = null, string folderId = null, int? limit = null, string pageToken = null)

List available docs

Returns a list of Coda docs accessible by the user. These are returned in the same order as on the docs page: reverse chronological by the latest event relevant to the user (last viewed, edited, or shared). 

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class ListDocsExample
    {
        public void main()
        {

            var apiInstance = new DocsApi();
            var isOwner = true;  // bool? | Show only docs owned by the user. (optional) 
            var query = query_example;  // string | Search term used to filter down results. (optional) 
            var sourceDoc = sourceDoc_example;  // string | Show only docs copied from the specified doc ID. (optional) 
            var isStarred = true;  // bool? | If true, returns docs that are starred. If false, returns docs that are not starred. (optional) 
            var inGallery = true;  // bool? | Show only docs visible within the gallery. (optional) 
            var workspaceId = workspaceId_example;  // string | Show only docs belonging to the given workspace. (optional) 
            var folderId = folderId_example;  // string | Show only docs belonging to the given folder. (optional) 
            var limit = 56;  // int? | Maximum number of results to return in this query. (optional) 
            var pageToken = pageToken_example;  // string | An opaque token used to fetch the next page of results. (optional) 

            try
            {
                // List available docs
                DocList result = apiInstance.ListDocs(isOwner, query, sourceDoc, isStarred, inGallery, workspaceId, folderId, limit, pageToken);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling DocsApi.ListDocs: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **isOwner** | **bool?**| Show only docs owned by the user. | [optional] 
 **query** | **string**| Search term used to filter down results. | [optional] 
 **sourceDoc** | **string**| Show only docs copied from the specified doc ID. | [optional] 
 **isStarred** | **bool?**| If true, returns docs that are starred. If false, returns docs that are not starred. | [optional] 
 **inGallery** | **bool?**| Show only docs visible within the gallery. | [optional] 
 **workspaceId** | **string**| Show only docs belonging to the given workspace. | [optional] 
 **folderId** | **string**| Show only docs belonging to the given folder. | [optional] 
 **limit** | **int?**| Maximum number of results to return in this query. | [optional] 
 **pageToken** | **string**| An opaque token used to fetch the next page of results. | [optional] 

### Return type

[**DocList**](DocList.md)

### Authorization

[Bearer](../README.md#Bearer)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)
